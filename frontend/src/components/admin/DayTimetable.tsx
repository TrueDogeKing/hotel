import { useTranslation } from "react-i18next";
import type { ScheduleDay, ScheduleEntry } from "../../api/admin";
import { formatDate, toTimeInput } from "../../utils/dates";

interface Props {
  day: ScheduleDay;
  onSelectGroup: (bookingId: string) => void;
}

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
      });
    }
  }

  return [...chips.values()].sort(
    (a, b) => a.startTime.localeCompare(b.startTime) || a.title.localeCompare(b.title),
  );
}

export default function DayTimetable({ day, onSelectGroup }: Props) {
  const { t, i18n } = useTranslation();
  const chips = buildChips(day.entries);

  // 06:00–23:00 by default, widened to cover anything scheduled outside it.
  let firstHour = 6;
  let lastHour = 22;
  for (const entry of day.entries) {
    firstHour = Math.min(firstHour, Number(entry.startTime.slice(0, 2)));
    lastHour = Math.max(lastHour, Number(entry.startTime.slice(0, 2)));
  }
  const hours = Array.from({ length: lastHour - firstHour + 1 }, (_, i) => firstHour + i);

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

      <div className="timetable">
        {hours.map((hour) => {
          const label = `${String(hour).padStart(2, "0")}:00`;
          const inHour = chips.filter((chip) => Number(chip.startTime.slice(0, 2)) === hour);
          return (
            <div className="timetable-row" key={hour}>
              <div className="timetable-hour">{label}</div>
              <div className="timetable-slot">
                {inHour.map((chip) => (
                  <button
                    type="button"
                    key={chip.key}
                    className={`timetable-chip ${chip.kind === "Meal" ? "meal" : "activity"}`}
                    onClick={() => onSelectGroup(chip.entries[0].bookingId)}
                  >
                    <span className="chip-time">
                      {toTimeInput(chip.startTime)}–{toTimeInput(chip.endTime)}
                    </span>
                    <strong>{chip.title}</strong>
                    <span className="chip-groups">
                      {chip.entries.length > 1
                        ? t("schedule.groupCount", { count: chip.entries.length })
                        : chip.entries[0].organizationName}
                    </span>
                    {chip.entries.length > 1 && (
                      <span className="chip-groups">
                        {chip.entries.map((e) => e.organizationName).join(", ")}
                      </span>
                    )}
                    {chip.entries.some((e) => e.menu) && (
                      <span className="chip-menu">
                        {chip.entries
                          .filter((e) => e.menu)
                          .map((e) => e.menu)
                          .join(" · ")}
                      </span>
                    )}
                  </button>
                ))}
              </div>
            </div>
          );
        })}
      </div>
    </section>
  );
}
