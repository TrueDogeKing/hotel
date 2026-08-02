import { useState } from "react";
import { useTranslation } from "react-i18next";
import { formatMonth, monthGrid, shortWeekdays, todayIso } from "../../utils/dates";

interface Props {
  /** The chosen day, or "" while nothing is chosen. */
  value: string;
  onChange: (iso: string) => void;
}

/**
 * Picks one day, in the same grid the stay picker uses.
 *
 * Deliberately plainer than RangeCalendar: no availability is fetched and no band
 * is drawn, because the question here is "which day am I looking at", not "could
 * the centre take a group then". Sharing the markup keeps the two calendars
 * looking like one control rather than like two.
 */
export default function DayCalendar({ value, onChange }: Props) {
  const { t, i18n } = useTranslation();
  const anchor = value || todayIso();
  const [year, setYear] = useState(() => Number(anchor.slice(0, 4)));
  const [month0, setMonth0] = useState(() => Number(anchor.slice(5, 7)) - 1);

  function shiftMonth(delta: number) {
    const date = new Date(year, month0 + delta, 1);
    setYear(date.getFullYear());
    setMonth0(date.getMonth());
  }

  const weekdays = shortWeekdays(i18n.language);

  return (
    <div className="range-cal">
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

      <div className="range-cal-grid">
        {monthGrid(year, month0).map((iso) => {
          const outside = Number(iso.slice(5, 7)) - 1 !== month0;
          const classes = ["range-cal-day"];
          if (outside) classes.push("outside");
          // The same end-cap fill the range picker gives its arrival day.
          if (iso === value) classes.push("start");

          return (
            <button
              type="button"
              key={iso}
              className={classes.join(" ")}
              aria-label={iso}
              aria-pressed={iso === value}
              onClick={() => onChange(iso)}
            >
              <span className="range-cal-num">{Number(iso.slice(8, 10))}</span>
            </button>
          );
        })}
      </div>
    </div>
  );
}
