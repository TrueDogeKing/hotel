import { useTranslation } from "react-i18next";
import type { CSSProperties } from "react";
import type { ScheduleDay, ScheduleEntry } from "../../api/admin";
import { formatDate, toTimeInput } from "../../utils/dates";

interface Props {
  day: ScheduleDay;
  onSelectGroup: (bookingId: string) => void;
}

/** Height of one hour row, in px. Blocks are positioned against this. */
const HOUR_HEIGHT = 56;
/** Floor for very short entries, so a 15-minute item stays readable. */
const MIN_BLOCK_HEIGHT = 26;

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
            const people = chip.entries.reduce((sum, entry) => sum + entry.headcount, 0);
            const top = ((chip.startMin - dayStartMin) / 60) * HOUR_HEIGHT;
            const height = Math.max(
              ((chip.endMin - chip.startMin) / 60) * HOUR_HEIGHT,
              MIN_BLOCK_HEIGHT,
            );

            return (
              <button
                type="button"
                key={chip.key}
                className={`timetable-chip ${chip.kind === "Meal" ? "meal" : "activity"}`}
                style={{
                  top: `${top}px`,
                  height: `${height}px`,
                  left: `calc(${(chip.column / chip.columns) * 100}% + 2px)`,
                  width: `calc(${100 / chip.columns}% - 6px)`,
                }}
                onClick={() => onSelectGroup(chip.entries[0].bookingId)}
                title={`${toTimeInput(chip.startTime)}–${toTimeInput(chip.endTime)} · ${chip.title}`}
              >
                <span className="chip-head">
                  <strong>{chip.title}</strong>
                  {/* Heads across every group at this sitting — what the kitchen
                      and the activity leader actually need to cater for. */}
                  <span className="chip-people">{t("schedule.people", { count: people })}</span>
                </span>
                <span className="chip-time">
                  {toTimeInput(chip.startTime)}–{toTimeInput(chip.endTime)}
                </span>
                <span className="chip-groups">
                  {chip.entries.length > 1
                    ? `${t("schedule.groupCount", { count: chip.entries.length })}: ${chip.entries
                        .map((e) => e.organizationName)
                        .join(", ")}`
                    : chip.entries[0].organizationName}
                </span>
                {chip.entries.some((e) => e.menu) && (
                  <span className="chip-menu">
                    {chip.entries
                      .filter((e) => e.menu)
                      .map((e) => e.menu)
                      .join(" · ")}
                  </span>
                )}
              </button>
            );
          })}
        </div>
      </div>
    </section>
  );
}
