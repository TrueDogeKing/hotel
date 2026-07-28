import { useMemo } from "react";
import { useTranslation } from "react-i18next";
import {
  dayNumber,
  formatMonth,
  monthGrid,
  shortWeekdays,
  todayIso,
} from "../../utils/dates";
import { packLanes, type LaneEvent } from "./lanes";
import CalendarTile from "./CalendarTile";

export interface CalendarBar {
  id: string;
  /** Both inclusive — the bar covers startIso..endIso. */
  startIso: string;
  endIso: string;
  label: string;
  /** Renders the bar in a muted fill, for stays that aren't confirmed yet. */
  muted?: boolean;
}

export interface CalendarDayBadge {
  meals: number;
  activities: number;
  /** Heads in the center that day — what the kitchen and staff actually plan for. */
  people: number;
}

interface Props {
  year: number;
  /** 0-11, matching Date.getMonth(). */
  month0: number;
  bars: CalendarBar[];
  dayBadges?: Map<string, CalendarDayBadge>;
  selectedIso: string | null;
  onSelectDay: (iso: string) => void;
  onSelectBar: (id: string) => void;
  onMonthChange: (year: number, month0: number) => void;
}

export default function MonthCalendar({
  year,
  month0,
  bars,
  dayBadges,
  selectedIso,
  onSelectDay,
  onSelectBar,
  onMonthChange,
}: Props) {
  const { t, i18n } = useTranslation();
  const days = useMemo(() => monthGrid(year, month0), [year, month0]);
  const weekdays = useMemo(() => shortWeekdays(i18n.language), [i18n.language]);

  const { lanes, laneCount } = useMemo(() => {
    const events: LaneEvent[] = bars.map((bar) => ({
      id: bar.id,
      start: dayNumber(bar.startIso),
      end: dayNumber(bar.endIso),
    }));
    const assigned = packLanes(events);
    const count = assigned.size === 0 ? 0 : Math.max(...assigned.values()) + 1;
    return { lanes: assigned, laneCount: count };
  }, [bars]);

  function shiftMonth(delta: number) {
    const shifted = new Date(year, month0 + delta, 1);
    onMonthChange(shifted.getFullYear(), shifted.getMonth());
  }

  function goToToday() {
    const now = new Date();
    onMonthChange(now.getFullYear(), now.getMonth());
    onSelectDay(todayIso());
  }

  return (
    <div className="cal">
      <div className="cal-header">
        <button
          type="button"
          className="cal-nav"
          aria-label={t("schedule.previousMonth")}
          onClick={() => shiftMonth(-1)}
        >
          ‹
        </button>
        <h2>{formatMonth(year, month0, i18n.language)}</h2>
        <button
          type="button"
          className="cal-nav"
          aria-label={t("schedule.nextMonth")}
          onClick={() => shiftMonth(1)}
        >
          ›
        </button>
        <button type="button" onClick={goToToday}>
          {t("schedule.today")}
        </button>
      </div>

      <div className="cal-weekdays" aria-hidden="true">
        {weekdays.map((day) => (
          <span key={day}>{day}</span>
        ))}
      </div>

      <div className="cal-grid">
        {days.map((iso) => (
          <CalendarTile
            key={iso}
            iso={iso}
            month0={month0}
            bars={bars}
            lanes={lanes}
            laneCount={laneCount}
            badge={dayBadges?.get(iso)}
            isSelected={iso === selectedIso}
            onSelectDay={onSelectDay}
            onSelectBar={onSelectBar}
          />
        ))}
      </div>
    </div>
  );
}
