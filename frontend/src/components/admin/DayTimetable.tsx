import { useTranslation } from "react-i18next";
import { useEffect, useLayoutEffect, useRef, useState, type CSSProperties } from "react";
import { MEAL_GAP_MINUTES, type ScheduleDay, type ScheduleEntry } from "../../api/admin";
import { formatDate, toTimeInput } from "../../utils/dates";

interface Props {
  day: ScheduleDay;
  onSelectGroup: (bookingId: string) => void;
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

export default function DayTimetable({ day, onSelectGroup }: Props) {
  const { t, i18n } = useTranslation();
  const chips = buildChips(day.entries);
  const placed = placeChips(chips);
  const clashes = findClashes(chips);

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
  const hours = Array.from({ length: lastHour - firstHour + 1 }, (_, i) => firstHour + i);
  const dayStartMin = firstHour * 60;

  return (
    <section className="timetable-panel">
      <h2>{formatDate(day.date, i18n.language, "long")}</h2>

      {day.groups.length === 0 ? (
        <p>{t("schedule.noGroupsToday")}</p>
      ) : (
        <ul className="timetable-groups">
          {day.groups.map((group) => (
            <li key={group.bookingId}>
              <button type="button" onClick={() => onSelectGroup(group.bookingId)}>
                {group.organizationName}
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

        <div className="timetable-events">
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
            const groups =
              chip.entries.length > 1
                ? `${t("schedule.groupCount", { count: chip.entries.length })}: ${chip.entries
                    .map((e) => e.organizationName)
                    .join(", ")}`
                : chip.entries[0].organizationName;
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
                className={`timetable-chip ${chip.kind === "Meal" ? "meal" : "activity"} rows-${rows}${
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
