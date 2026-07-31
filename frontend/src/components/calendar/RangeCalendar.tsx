import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { getAvailabilityCalendar, type AvailabilityDay } from "../../api/public";
import {
  addDaysIso,
  dayNumber,
  formatMonth,
  monthGrid,
  shortWeekdays,
  todayIso,
} from "../../utils/dates";

interface Props {
  /** Arrival, or "" while nothing is chosen. */
  startDate: string;
  /** Departure, or "" while only the arrival is chosen. */
  endDate: string;
  /** Greys out the nights that cannot hold a group this size. 0 means "not asked
   *  yet" — then only closed and fully booked nights are greyed. */
  headcount: number;
  onChange: (range: { startDate: string; endDate: string }) => void;
  /** Lets a past date be picked as the arrival. Off for the public wizard, where a
   *  stay can only start today or later; on for the admin form, which backfills
   *  groups that already arrived. Availability still applies — a past night is
   *  only offered if the centre genuinely had room for it. */
  allowPast?: boolean;
}

/**
 * Picks a stay as a range rather than as two dates.
 *
 * The whole span between arrival and departure is drawn as one band, and the
 * span the cursor would produce is previewed before it is committed — the two
 * `<input type="date">` fields this replaces made a booker hold the range in
 * their head and gave no hint that a date was unbookable until they submitted.
 *
 * Nights the centre could not take the group are greyed: closed for maintenance
 * or a winter break, fully booked, or simply short of beds for this headcount.
 * Availability comes per night from the API, so what is greyed is exactly what
 * the booking endpoint would refuse.
 */
export default function RangeCalendar({
  startDate,
  endDate,
  headcount,
  onChange,
  allowPast = false,
}: Props) {
  const { t, i18n } = useTranslation();
  const today = todayIso();
  const anchor = startDate || today;
  const [year, setYear] = useState(() => Number(anchor.slice(0, 4)));
  const [month0, setMonth0] = useState(() => Number(anchor.slice(5, 7)) - 1);
  /** The day under the cursor while a departure is being chosen. */
  const [hover, setHover] = useState<string | null>(null);

  const grid = monthGrid(year, month0);
  const gridStart = grid[0];
  const gridEnd = grid[grid.length - 1];
  // What the grid is currently asking the API. Headcount is part of it: how many
  // beds a night must have free is part of the question.
  const query = `${gridStart}|${gridEnd}|${headcount}`;

  /** The answer, tagged with the question it answers. Written only from the
   *  fetch's callbacks — "loading" is then simply "the answer on hand is for a
   *  different question", with no second state to keep in step. */
  const [answer, setAnswer] = useState<{
    query: string;
    nights: Map<string, AvailabilityDay>;
    failed: boolean;
  } | null>(null);

  const loading = answer?.query !== query;
  const nights = answer?.nights ?? new Map<string, AvailabilityDay>();
  const failed = answer?.query === query && answer.failed;

  useEffect(() => {
    let cancelled = false;
    void getAvailabilityCalendar(gridStart, gridEnd, headcount || undefined)
      .then((calendar) => {
        if (cancelled) return;
        setAnswer({
          query,
          nights: new Map(calendar.days.map((day) => [day.date, day])),
          failed: false,
        });
      })
      .catch(() => {
        if (!cancelled) setAnswer({ query, nights: new Map(), failed: true });
      });
    return () => {
      cancelled = true;
    };
  }, [gridStart, gridEnd, headcount, query]);

  /** A night the centre could take this group. Unknown nights (a failed fetch)
   *  count as available: the API refuses a bad range anyway, and a calendar that
   *  greys everything out because a request failed is worse than one that lets
   *  the booker try. */
  function isFree(iso: string): boolean {
    const night = nights.get(iso);
    return night ? night.fits : true;
  }

  /** "Too early to pick" — not simply "in the past" once `allowPast` is on, which
   *  is why every other check goes through this rather than a raw date compare. */
  function isBlocked(iso: string): boolean {
    return !allowPast && iso < today;
  }

  /** Every night of a stay has to be free — the departure day itself is not one. */
  function rangeIsFree(from: string, to: string): boolean {
    for (let date = from; date < to; date = addDaysIso(date, 1)) {
      if (!isFree(date) || isBlocked(date)) return false;
    }
    return true;
  }

  function handleClick(iso: string) {
    // Restart from here when there is no arrival yet, when the range is already
    // complete, or when the click is at or before the arrival.
    const startingOver = !startDate || endDate !== "" || iso <= startDate;
    if (startingOver) {
      if (isBlocked(iso) || !isFree(iso)) return;
      onChange({ startDate: iso, endDate: "" });
      return;
    }

    // Otherwise this is the departure: allowed as long as every night up to it is
    // free. The departure day itself may be closed — the group leaves that morning.
    if (!rangeIsFree(startDate, iso)) return;
    onChange({ startDate, endDate: iso });
  }

  /** The range as it currently reads, including the one being hovered. */
  const previewEnd = endDate || (startDate && hover && hover > startDate ? hover : "");
  const previewValid = previewEnd === "" || rangeIsFree(startDate, previewEnd);

  function shiftMonth(delta: number) {
    const date = new Date(year, month0 + delta, 1);
    setYear(date.getFullYear());
    setMonth0(date.getMonth());
  }

  const weekdays = shortWeekdays(i18n.language);
  const selectedNights =
    startDate && endDate ? dayNumber(endDate) - dayNumber(startDate) : 0;

  return (
    <div className={`range-cal${loading ? " loading" : ""}`}>
      <div className="range-cal-header">
        <button
          type="button"
          className="cal-nav"
          aria-label={t("wizard.calendar.previousMonth")}
          onClick={() => shiftMonth(-1)}
        >
          ‹
        </button>
        <h2>{formatMonth(year, month0, i18n.language)}</h2>
        <button
          type="button"
          className="cal-nav"
          aria-label={t("wizard.calendar.nextMonth")}
          onClick={() => shiftMonth(1)}
        >
          ›
        </button>
      </div>

      <div className="cal-weekdays" aria-hidden="true">
        {weekdays.map((day) => (
          <span key={day}>{day}</span>
        ))}
      </div>

      <div className="range-cal-grid" onMouseLeave={() => setHover(null)}>
        {grid.map((iso) => {
          const night = nights.get(iso);
          const outside = Number(iso.slice(5, 7)) - 1 !== month0;
          const blocked = isBlocked(iso);
          const free = isFree(iso);

          const isStart = iso === startDate;
          const isEnd = iso === endDate;
          // The band covers the nights slept, so the departure day is drawn as an
          // end cap rather than as part of the run.
          const inRange =
            startDate !== "" && previewEnd !== "" && iso > startDate && iso < previewEnd;

          const classes = ["range-cal-day"];
          if (outside) classes.push("outside");
          if (blocked) classes.push("past");
          if (!free && !blocked) classes.push("unavailable");
          if (isStart) classes.push("start");
          if (isEnd || (previewEnd !== "" && iso === previewEnd)) classes.push("end");
          if (inRange) classes.push("in-range");
          if ((isStart || inRange || iso === previewEnd) && !previewValid) {
            classes.push("invalid");
          }

          // Why a day cannot be picked, on hover — greying without a reason is the
          // part of a booking calendar people complain about.
          const reason = blocked
            ? t("wizard.calendar.pastHint")
            : night?.closed
              ? night.closureReason || t("wizard.calendar.closedHint")
              : !free
                ? t("wizard.calendar.fullHint", { beds: night?.freeBeds ?? 0 })
                : t("wizard.calendar.freeHint", { beds: night?.freeBeds ?? 0 });

          return (
            <button
              type="button"
              key={iso}
              className={classes.join(" ")}
              title={reason}
              aria-label={`${iso} — ${reason}`}
              aria-pressed={isStart || isEnd}
              // Never disabled: a past or full day still has to explain itself on
              // hover, and a disabled button reports nothing to a pointer.
              onMouseEnter={() => setHover(iso)}
              onClick={() => handleClick(iso)}
            >
              <span className="range-cal-num">{Number(iso.slice(8, 10))}</span>
              {/* Beds left, so "nearly full" is visible before it becomes "full". */}
              {!blocked && !outside && night && !night.closed && (
                <span className="range-cal-beds">{night.freeBeds}</span>
              )}
            </button>
          );
        })}
      </div>

      <p className="range-cal-status" role="status">
        {failed
          ? t("wizard.calendar.loadError")
          : selectedNights > 0
            ? t("wizard.calendar.selected", {
                nights: selectedNights,
                count: selectedNights,
              })
            : startDate
              ? t("wizard.calendar.pickDeparture")
              : t("wizard.calendar.pickArrival")}
      </p>

      <ul className="range-cal-legend" aria-hidden="true">
        <li>
          <span className="swatch free" /> {t("wizard.calendar.legendFree")}
        </li>
        <li>
          <span className="swatch unavailable" /> {t("wizard.calendar.legendUnavailable")}
        </li>
        <li>
          <span className="swatch selected" /> {t("wizard.calendar.legendSelected")}
        </li>
      </ul>
    </div>
  );
}
