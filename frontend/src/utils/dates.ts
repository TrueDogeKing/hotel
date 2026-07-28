/**
 * Date-only helpers.
 *
 * The API speaks `DateOnly` ("YYYY-MM-DD") and `TimeOnly` ("HH:mm:ss"). Both of
 * the obvious JS approaches are wrong for those:
 *
 *   - `d.toISOString().slice(0, 10)` converts to UTC first, so at 01:30 CEST on
 *     1 July it yields "2026-06-30".
 *   - `new Date("2026-07-01")` parses as UTC midnight, so it formats as 30 June
 *     for anyone west of UTC.
 *
 * Everything here works on the local calendar instead. These apply to date-ONLY
 * strings; genuine UTC timestamps (e.g. `holdExpiresAt`) are correctly handled by
 * plain `new Date(iso)` and must not be routed through here.
 */

const MS_PER_DAY = 86_400_000;

/** Local-calendar ISO date (YYYY-MM-DD). */
export function toIsoDate(d: Date): string {
  const pad = (n: number) => String(n).padStart(2, "0");
  return `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())}`;
}

export function todayIso(): string {
  return toIsoDate(new Date());
}

/** Parses an API date-only string into a *local* midnight Date. */
export function fromIsoDate(iso: string): Date {
  const [y, m, d] = iso.split("-").map(Number);
  return new Date(y, m - 1, d);
}

/** Epoch day number — integer arithmetic, immune to DST and time zones. */
export function dayNumber(iso: string): number {
  const [y, m, d] = iso.split("-").map(Number);
  return Math.round(Date.UTC(y, m - 1, d) / MS_PER_DAY);
}

/** Inverse of dayNumber. toISOString is safe here: built and read back in UTC. */
export function isoFromDayNumber(n: number): string {
  return new Date(n * MS_PER_DAY).toISOString().slice(0, 10);
}

export function addDaysIso(iso: string, days: number): string {
  return isoFromDayNumber(dayNumber(iso) + days);
}

/** Monday = 0 … Sunday = 6 (Polish/ISO week ordering). */
export function mondayIndex(iso: string): number {
  return (fromIsoDate(iso).getDay() + 6) % 7;
}

/**
 * 42 ISO dates: a fixed 6×7 Monday-first grid. Always six weeks so the calendar
 * never changes height between months.
 */
export function monthGrid(year: number, month0: number): string[] {
  const first = toIsoDate(new Date(year, month0, 1));
  const gridStart = dayNumber(first) - mondayIndex(first);
  return Array.from({ length: 42 }, (_, i) => isoFromDayNumber(gridStart + i));
}

const dateFormatters = new Map<string, Intl.DateTimeFormat>();

export function formatDate(iso: string, lang: string, style: "medium" | "long" = "medium"): string {
  const key = `${lang}|${style}`;
  let formatter = dateFormatters.get(key);
  if (!formatter) {
    formatter = new Intl.DateTimeFormat(lang === "en" ? "en-GB" : "pl-PL", { dateStyle: style });
    dateFormatters.set(key, formatter);
  }
  return formatter.format(fromIsoDate(iso));
}

const monthFormatters = new Map<string, Intl.DateTimeFormat>();

/** "lipiec 2026" / "July 2026" — the calendar header. */
export function formatMonth(year: number, month0: number, lang: string): string {
  let formatter = monthFormatters.get(lang);
  if (!formatter) {
    formatter = new Intl.DateTimeFormat(lang === "en" ? "en-GB" : "pl-PL", {
      month: "long",
      year: "numeric",
    });
    monthFormatters.set(lang, formatter);
  }
  return formatter.format(new Date(year, month0, 1));
}

const weekdayFormatters = new Map<string, Intl.DateTimeFormat>();

/** Short Monday-first weekday names for the calendar header row. */
export function shortWeekdays(lang: string): string[] {
  let formatter = weekdayFormatters.get(lang);
  if (!formatter) {
    formatter = new Intl.DateTimeFormat(lang === "en" ? "en-GB" : "pl-PL", { weekday: "short" });
    weekdayFormatters.set(lang, formatter);
  }
  // 2024-01-01 was a Monday.
  return Array.from({ length: 7 }, (_, i) => formatter.format(new Date(2024, 0, 1 + i)));
}

/** "08:00:00" (TimeOnly JSON) → "08:00", the format <input type="time"> expects. */
export function toTimeInput(time: string): string {
  return time.slice(0, 5);
}

/** "08:00" → "08:00:00" for the API. Already-full values pass through. */
export function fromTimeInput(time: string): string {
  return time.length === 5 ? `${time}:00` : time;
}

/** "08:00:00" + "09:00:00" → "08:00–09:00". */
export function formatTimeRange(start: string, end: string): string {
  return `${toTimeInput(start)}–${toTimeInput(end)}`;
}
