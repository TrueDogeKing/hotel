import { useTranslation } from "react-i18next";
import { useEffect, useLayoutEffect, useRef, useState, type CSSProperties } from "react";
import { MEAL_GAP_MINUTES, type ScheduleDay, type ScheduleEntry } from "../../api/admin";
import { formatDate, toTimeInput } from "../../utils/dates";
import { groupLabel } from "../../utils/groupLabel";

interface Props {
  day: ScheduleDay;
  onSelectGroup: (bookingId: string) => void;
  /** Step the shown day by whole days. Omitted when there is nowhere to step to. */
  onChangeDay?: (delta: number) => void;
  /**
   * Start an entry at this time ("HH:mm"). Given the hour that was clicked in the
   * grid, or the first free-looking hour when the button in the header is used.
   * Omitted for a reader who may not add anything.
   */
  onAddAt?: (startTime: string) => void;
  /**
   * The entry being written in the form below, as "HH:mm" times. Drawn in place on
   * the grid so its slot — and whatever it would sit next to — stays visible while
   * it is filled in, and it moves as the times in the form are edited.
   */
  pending?: { startTime: string; endTime: string } | null;
}

/** Height of one hour row, in px. Blocks are positioned against this; a one-hour
 *  entry has to fit its title, time, place and groups without clipping. */
const HOUR_HEIGHT = 84;
/** Floor for very short entries, so a 15-minute item stays readable. */
const MIN_BLOCK_HEIGHT = 30;
/** Line box of a block's text until the probe has measured the real one. Every
 *  vertical decision below is a multiple of that measurement rather than a pixel
 *  count, so blocks follow the reader's font size and browser zoom instead of
 *  assuming a 16px line. */
const CHIP_LINE_FALLBACK = 16;
/** A block's vertical padding, in line boxes (0.25rem top and bottom against a
 *  ~1rem line). Kept relative for the same reason. */
const CHIP_PADDING_LINES = 0.5;
/** Title and time+group: the two rows a block keeps as long as two rows fit at all. */
const CHIP_MIN_ROWS = 2;
/** Subpixel wobble in the measured line box is not worth a re-render. */
const MEASURE_TOLERANCE = 0.5;

/**
 * One chip in the timetable. Meals that several groups eat at the same time are
 * merged into a single chip ("Śniadanie 08:00–09:00 · 3 grupy") — that shared
 * sitting is exactly what the kitchen plans around.
 */
interface Chip {
  key: string;
  kind: ScheduleEntry["kind"];
  startTime: string;
  endTime: string;
  title: string;
  entries: ScheduleEntry[];
  startMin: number;
  endMin: number;
}

/** Why a block is flagged: the place is double-booked, or two groups eat at once. */
type ClashReason = "Location" | "Meal";

function placesOf(chip: Chip): string[] {
  return [...new Set(chip.entries.map((e) => e.location).filter((l): l is string => !!l))];
}

function groupsOf(chip: Chip): Set<string> {
  return new Set(chip.entries.map((e) => e.bookingId));
}

/**
 * Blocks that two different groups cannot both have. The same warning the entry form
 * gives before a save, shown here for entries already on the schedule — including the
 * ones that were saved past the warning.
 *
 * A merged meal block is itself a clash: it exists only when several groups sit down
 * at the same moment, and the centre seats one group at a time.
 */
function findClashes(chips: Chip[]): Map<string, ClashReason> {
  const flagged = new Map<string, ClashReason>();
  const places = new Map(chips.map((c) => [c.key, placesOf(c).map((p) => p.toLowerCase())]));
  const groups = new Map(chips.map((c) => [c.key, groupsOf(c)]));

  for (const chip of chips) {
    if (chip.kind === "Meal" && groups.get(chip.key)!.size > 1) {
      flagged.set(chip.key, "Meal");
    }
  }

  for (let i = 0; i < chips.length; i++) {
    for (let j = i + 1; j < chips.length; j++) {
      const a = chips[i];
      const b = chips[j];
      // One group being in two places at once is its own programme's problem and is
      // already marked in the group panel.
      if ([...groups.get(a.key)!].some((id) => groups.get(b.key)!.has(id))) continue;

      const samePlace = places.get(a.key)!.some((p) => places.get(b.key)!.includes(p));
      const bothMeals = a.kind === "Meal" && b.kind === "Meal";
      if (!samePlace && !bothMeals) continue;

      // Sittings need the changeover between them; anything else only has to not overlap.
      const gap = bothMeals ? MEAL_GAP_MINUTES : 0;
      if (a.startMin >= b.endMin + gap || b.startMin >= a.endMin + gap) continue;

      const reason: ClashReason = samePlace ? "Location" : "Meal";
      // A place clash is the more concrete complaint, so it may replace a meal one.
      for (const chip of [a, b]) {
        if (reason === "Location" || !flagged.has(chip.key)) flagged.set(chip.key, reason);
      }
    }
  }

  return flagged;
}

/** A chip with its slot in the day view: which of the side-by-side columns it
 *  takes, and how many columns its overlapping neighbours forced. */
interface PlacedChip extends Chip {
  column: number;
  columns: number;
}

function minutesOf(time: string): number {
  const [hours, mins] = time.split(":");
  return Number(hours) * 60 + Number(mins);
}

function buildChips(entries: ScheduleEntry[]): Chip[] {
  const chips = new Map<string, Chip>();

  for (const entry of entries) {
    // Meals group on (mealKind, start, end) — stable even if one group renames
    // its breakfast. Activities stay per-group: they are genuinely different.
    const key =
      entry.kind === "Meal"
        ? `meal|${entry.mealKind}|${entry.startTime}|${entry.endTime}`
        : `activity|${entry.id}`;

    const existing = chips.get(key);
    if (existing) {
      existing.entries.push(entry);
    } else {
      chips.set(key, {
        key,
        kind: entry.kind,
        startTime: entry.startTime,
        endTime: entry.endTime,
        title: entry.title,
        entries: [entry],
        startMin: minutesOf(entry.startTime),
        endMin: minutesOf(entry.endTime),
      });
    }
  }

  return [...chips.values()].sort(
    (a, b) => a.startMin - b.startMin || a.title.localeCompare(b.title),
  );
}

/**
 * Standard day-view packing. Chips that overlap in time are split into columns so
 * none is hidden behind another; chips that merely follow one another each get the
 * full width. Columns are counted per cluster of overlapping chips, so one busy
 * afternoon does not squeeze the rest of the day.
 */
function placeChips(chips: Chip[]): PlacedChip[] {
  const placed: PlacedChip[] = [];
  let cluster: Chip[] = [];
  let clusterEnd = -1;

  function flush() {
    if (cluster.length === 0) return;
    // Reuse a column as soon as the chip occupying it has ended.
    const columnEnds: number[] = [];
    const assigned = cluster.map((chip) => {
      let column = columnEnds.findIndex((end) => end <= chip.startMin);
      if (column === -1) column = columnEnds.length;
      columnEnds[column] = chip.endMin;
      return column;
    });

    cluster.forEach((chip, i) =>
      placed.push({ ...chip, column: assigned[i], columns: columnEnds.length }),
    );
    cluster = [];
    clusterEnd = -1;
  }

  for (const chip of chips) {
    if (cluster.length > 0 && chip.startMin >= clusterEnd) flush();
    cluster.push(chip);
    clusterEnd = Math.max(clusterEnd, chip.endMin);
  }
  flush();

  return placed;
}

/** Clicks land on whichever quarter-hour they are nearest — finer than that is
 *  noise at 84px an hour, and the form can still be typed into. */
const SNAP_MINUTES = 15;

/** How long a new entry runs until the form says otherwise. Exported so the
 *  preview drawn under the cursor and the form's own default cannot drift. */
export const NEW_ENTRY_MINUTES = 60;

function formatTime(totalMinutes: number): string {
  const clamped = Math.max(0, Math.min(23 * 60 + 45, totalMinutes));
  const hours = Math.floor(clamped / 60);
  const minutes = clamped % 60;
  return `${String(hours).padStart(2, "0")}:${String(minutes).padStart(2, "0")}`;
}

export default function DayTimetable({
  day,
  onSelectGroup,
  onChangeDay,
  onAddAt,
  pending = null,
}: Props) {
  const { t, i18n } = useTranslation();
  const supervisorsShort = (count: number) => t("schedule.supervisorsShort", { count });
  const chips = buildChips(day.entries);
  const placed = placeChips(chips);
  const clashes = findClashes(chips);
  // Quarter-hour under the cursor while the grid is being pointed at, so the slot
  // a click would take is shown before the click rather than after it.
  const [hoverMin, setHoverMin] = useState<number | null>(null);

  // How tall one row of a block's text actually is, measured from a hidden line
  // styled exactly like a block rather than assumed. Font size, zoom and a late
  // webfont all change it, and each would otherwise leave blocks either clipping
  // their text or dropping rows they had room for.
  const probeRef = useRef<HTMLSpanElement>(null);
  const [lineHeight, setLineHeight] = useState(CHIP_LINE_FALLBACK);

  // Read back on mount before the browser paints: the first render has only the
  // fallback, and settling a frame later would show every block briefly at the wrong
  // row count. Re-running on the value it just set converges in one extra pass, because
  // a measurement that has not really changed is not written back.
  useLayoutEffect(() => {
    const probe = probeRef.current;
    if (!probe) return;
    const measured = probe.getBoundingClientRect().height;
    if (measured > 0 && Math.abs(measured - lineHeight) > MEASURE_TOLERANCE) {
      setLineHeight(measured);
    }
  }, [lineHeight]);

  // Zoom, a change of root font size and a webfont arriving late all change the line
  // box without anything re-rendering. Three signals rather than one because no single
  // one covers all three: zoom arrives as a resize, a font as fonts.ready, and a root
  // font-size change only as a resize of the probe itself.
  useEffect(() => {
    const probe = probeRef.current;
    if (!probe) return;

    const remeasure = () => {
      const measured = probe.getBoundingClientRect().height;
      if (measured <= 0) return;
      setLineHeight((current) =>
        Math.abs(measured - current) > MEASURE_TOLERANCE ? measured : current,
      );
    };

    window.addEventListener("resize", remeasure);
    void document.fonts?.ready.then(remeasure);
    const observer = new ResizeObserver(remeasure);
    observer.observe(probe);

    return () => {
      window.removeEventListener("resize", remeasure);
      observer.disconnect();
    };
  }, []);

  // 06:00–22:00 by default, widened to cover anything scheduled outside it. The
  // end times matter as much as the starts now: a block running to 20:00 needs the
  // 20:00 line to reach down to.
  let firstHour = 6;
  let lastHour = 22;
  for (const chip of chips) {
    firstHour = Math.min(firstHour, Math.floor(chip.startMin / 60));
    lastHour = Math.max(lastHour, Math.ceil(chip.endMin / 60));
  }
  // The entry being written counts too, or a 23:00 start typed into the form would
  // draw its block past the bottom of a ruler that stops at 22:00.
  if (pending) {
    firstHour = Math.min(firstHour, Math.floor(minutesOf(pending.startTime) / 60));
    lastHour = Math.max(lastHour, Math.ceil(minutesOf(pending.endTime) / 60));
  }
  const hours = Array.from({ length: lastHour - firstHour + 1 }, (_, i) => firstHour + i);
  const dayStartMin = firstHour * 60;

  return (
    <section className="timetable-panel">
      {/* Stepping a day belongs here as well as on the calendar: the month grid is
          tall enough that walking a week from the timetable would mean scrolling
          back up to it for every single day. */}
      <header className="timetable-head">
        {onChangeDay && (
          <button
            type="button"
            className="timetable-nav"
            aria-label={t("schedule.previousDay")}
            title={t("schedule.previousDay")}
            onClick={() => onChangeDay(-1)}
          >
            ‹
          </button>
        )}
        <h2>{formatDate(day.date, i18n.language, "long")}</h2>
        {onChangeDay && (
          <button
            type="button"
            className="timetable-nav"
            aria-label={t("schedule.nextDay")}
            title={t("schedule.nextDay")}
            onClick={() => onChangeDay(1)}
          >
            ›
          </button>
        )}
        {/* Clicking the grid is the quick way in, but it is mouse-only; this is the
            same action for anyone on a keyboard, and it names the affordance. */}
        {onAddAt && day.groups.length > 0 && (
          <button
            type="button"
            className="timetable-add"
            onClick={() => onAddAt(formatTime(Math.max(dayStartMin, 9 * 60)))}
          >
            {t("schedule.addHere")}
          </button>
        )}
      </header>

      {day.groups.length === 0 ? (
        <p>{t("schedule.noGroupsToday")}</p>
      ) : (
        <ul className="timetable-groups">
          {day.groups.map((group) => (
            <li key={group.bookingId}>
              <button type="button" onClick={() => onSelectGroup(group.bookingId)}>
                {groupLabel(group.organizationName, group.supervisorCount, supervisorsShort)}
              </button>
              <span className="timetable-group-meta">
                {t("schedule.people", { count: group.headcount })}
                {group.isArrivalDay && ` · ${t("schedule.arrives")}`}
                {group.isDepartureDay && ` · ${t("schedule.departs")}`}
              </span>
            </li>
          ))}
        </ul>
      )}

      <div className="timetable" style={{ "--tt-hour": `${HOUR_HEIGHT}px` } as CSSProperties}>
        {/* One line of a block's text, hidden, measured. Never read by anyone. */}
        <span className="timetable-probe" ref={probeRef} aria-hidden="true">
          0
        </span>

        {/* Hour rows are only the ruler; the entries live in the layer below so
            each one can span its real duration instead of sitting in its start hour. */}
        <div className="timetable-ruler">
          {hours.map((hour) => (
            <div className="timetable-row" key={hour}>
              <div className="timetable-hour">{`${String(hour).padStart(2, "0")}:00`}</div>
              <div className="timetable-line" />
            </div>
          ))}
        </div>

        {/* The grid itself starts an entry: click an empty stretch and the form
            opens on that quarter-hour. Behind the blocks, so a click on one still
            opens that group rather than adding on top of it. */}
        {onAddAt && day.groups.length > 0 && (
          <div
            className="timetable-canvas"
            title={t("schedule.addHereHint")}
            onMouseMove={(event) => {
              const top = event.currentTarget.getBoundingClientRect().top;
              const minutes = dayStartMin + ((event.clientY - top) / HOUR_HEIGHT) * 60;
              const snapped = Math.round(minutes / SNAP_MINUTES) * SNAP_MINUTES;
              // Only on a real change: this fires on every pixel of movement, and
              // a re-render per pixel to redraw the same block is waste.
              setHoverMin((current) => (current === snapped ? current : snapped));
            }}
            onMouseLeave={() => setHoverMin(null)}
            onClick={(event) => {
              // Computed from the click itself rather than from the hover state:
              // a tap arrives without a mousemove before it, and would otherwise
              // land on whatever the last hovered slot was — or on nothing.
              const top = event.currentTarget.getBoundingClientRect().top;
              const minutes = dayStartMin + ((event.clientY - top) / HOUR_HEIGHT) * 60;
              onAddAt(formatTime(Math.round(minutes / SNAP_MINUTES) * SNAP_MINUTES));
            }}
          />
        )}

        <div className="timetable-events">
          {/* The slot the new entry would take, drawn where it would land: solid
              once the form is open and following the times typed into it, dashed
              while it is only the hover under the cursor. Never a hit target — it
              sits over the grid that opens the form. */}
          {(() => {
            const preview = pending
              ? { startMin: minutesOf(pending.startTime), endMin: minutesOf(pending.endTime) }
              : hoverMin !== null
                ? { startMin: hoverMin, endMin: hoverMin + NEW_ENTRY_MINUTES }
                : null;
            if (!preview) return null;

            const top = ((preview.startMin - dayStartMin) / 60) * HOUR_HEIGHT;
            const height = Math.max(
              ((preview.endMin - preview.startMin) / 60) * HOUR_HEIGHT,
              MIN_BLOCK_HEIGHT,
            );
            return (
              <div
                className={`timetable-preview${pending ? " pending" : ""}`}
                style={{ top: `${top}px`, height: `${height}px` }}
                aria-hidden="true"
              >
                <span className="timetable-preview-time">
                  {formatTime(preview.startMin)}–{formatTime(preview.endMin)}
                </span>
              </div>
            );
          })()}

          {placed.map((chip) => {
            const people = chip.entries.reduce(
              (sum, entry) => sum + (entry.participantCount ?? entry.headcount),
              0,
            );
            const top = ((chip.startMin - dayStartMin) / 60) * HOUR_HEIGHT;
            const height = Math.max(
              ((chip.endMin - chip.startMin) / 60) * HOUR_HEIGHT,
              MIN_BLOCK_HEIGHT,
            );
            const clash = clashes.get(chip.key);
            const time = `${toTimeInput(chip.startTime)}–${toTimeInput(chip.endTime)}`;
            const places = placesOf(chip).join(" · ");
            // Meals and activities both draw as chips, so this is where the kadra
            // count shows up beside the group: "Karatecy (3 op.)".
            const groups =
              chip.entries.length > 1
                ? `${t("schedule.groupCount", { count: chip.entries.length })}: ${chip.entries
                    .map((e) => groupLabel(e.organizationName, e.supervisorCount, supervisorsShort))
                    .join(", ")}`
                : groupLabel(
                    chip.entries[0].organizationName,
                    chip.entries[0].supervisorCount,
                    supervisorsShort,
                  );
            const menu = chip.entries
              .filter((e) => e.menu)
              .map((e) => e.menu)
              .join(" · ");

            // Rows this block has to say, against rows it has room for. The lower of
            // the two decides how much CSS shows; one row is always shown, however
            // short the entry, and hover opens the rest.
            const wanted = CHIP_MIN_ROWS + (places ? 1 : 0) + (menu ? 1 : 0);
            const room = Math.floor(height / lineHeight - CHIP_PADDING_LINES);
            const rows = Math.max(1, Math.min(wanted, room));

            return (
              <button
                type="button"
                key={chip.key}
                className={`timetable-chip ${chip.kind === "Meal" ? "meal" : chip.kind === "Outing" ? "away" : "activity"} rows-${rows}${
                  clash ? " clashing" : ""
                }`}
                style={
                  {
                    top: `${top}px`,
                    height: `${height}px`,
                    // Hovering grows the block to fit its text; --chip-h keeps it from
                    // shrinking below the duration it represents while it is open.
                    "--chip-h": `${height}px`,
                    left: `calc(${(chip.column / chip.columns) * 100}% + 2px)`,
                    width: `calc(${100 / chip.columns}% - 6px)`,
                  } as CSSProperties
                }
                onClick={() => onSelectGroup(chip.entries[0].bookingId)}
                /* A short block shows only its first lines, so the tooltip carries the
                   whole thing for anyone who cannot hover it open. */
                title={[
                  `${time} · ${chip.title}`,
                  places,
                  groups,
                  menu,
                  clash && t(`schedule.conflict.badge${clash}`),
                ]
                  .filter(Boolean)
                  .join("\n")}
              >
                <span className="chip-head">
                  <strong>{chip.title}</strong>
                  {/* Heads across every group at this sitting — what the kitchen
                      and the activity leader actually need to cater for. */}
                  <span className="chip-people">{t("schedule.people", { count: people })}</span>
                </span>
                {/* Time and group share a line: whose block this is has to survive down
                    to the shortest entry, and on its own line it was the first thing a
                    half-hour block dropped. */}
                <span className="chip-meta">
                  <span className="chip-time">{time}</span>
                  <span className="chip-groups">{groups}</span>
                  {/* Outside the time, which a very narrow block drops — a clash has to
                      stay visible at every width. */}
                  {clash && (
                    <em className="chip-clash" title={t(`schedule.conflict.badge${clash}`)}>
                      {t("schedule.conflict.badge")}
                    </em>
                  )}
                </span>
                {places && <span className="chip-where">{places}</span>}
                {menu && <span className="chip-menu">{menu}</span>}
              </button>
            );
          })}
        </div>
      </div>
    </section>
  );
}
