import { useCallback, useEffect, useRef, useState } from "react";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import AdminLayout from "../../components/admin/AdminLayout";
import MonthCalendar, {
  type CalendarBar,
  type CalendarDayBadge,
} from "../../components/calendar/MonthCalendar";
import DayTimetable from "../../components/admin/DayTimetable";
import GroupSchedulePanel from "../../components/admin/GroupSchedulePanel";
import {
  getScheduleCalendar,
  getScheduleDay,
  type ScheduleCalendar,
  type ScheduleDay,
} from "../../api/admin";
import { addDaysIso, fromIsoDate, monthGrid, todayIso } from "../../utils/dates";
import { scrollPanelIntoView } from "../../utils/scroll";

const TODAY = todayIso();

// The camp schedule: a month of group stays, the hour-by-hour timetable for the
// selected day, and a side panel for editing one group's whole programme.
export default function SchedulePage() {
  const { t } = useTranslation();
  const now = new Date();
  const [year, setYear] = useState(now.getFullYear());
  const [month0, setMonth0] = useState(now.getMonth());
  const [calendar, setCalendar] = useState<ScheduleCalendar | null>(null);
  const [selectedDate, setSelectedDate] = useState<string | null>(TODAY);
  const [day, setDay] = useState<ScheduleDay | null>(null);
  const [selectedBookingId, setSelectedBookingId] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);
  const timetableRef = useRef<HTMLDivElement>(null);
  const groupPanelRef = useRef<HTMLDivElement>(null);
  // Set when the user picks a day, cleared once that day's timetable has been
  // scrolled to. A flag rather than scrolling straight from the click, because the
  // timetable only renders after its fetch lands — scrolling any earlier would
  // just move to the "pick a day" placeholder.
  const scrollToTimetable = useRef(false);

  // The visible grid always spans six weeks, so fetch exactly what it shows —
  // that way bars starting in the trailing days of the previous month render too.
  const grid = monthGrid(year, month0);
  const gridStart = grid[0];
  const gridEnd = grid[grid.length - 1];

  const loadCalendar = useCallback(async () => {
    try {
      setCalendar(await getScheduleCalendar(gridStart, gridEnd));
    } catch {
      setError(t("schedule.loadError"));
    }
  }, [gridStart, gridEnd, t]);

  useEffect(() => {
    let cancelled = false;
    void getScheduleCalendar(gridStart, gridEnd)
      .then((data) => {
        if (!cancelled) setCalendar(data);
      })
      .catch(() => {
        if (!cancelled) setError(t("schedule.loadError"));
      });
    return () => {
      cancelled = true;
    };
  }, [gridStart, gridEnd, t]);

  useEffect(() => {
    if (!selectedDate) return;
    let cancelled = false;
    void getScheduleDay(selectedDate)
      .then((data) => {
        if (!cancelled) setDay(data);
      })
      .catch(() => {
        if (!cancelled) setError(t("schedule.loadError"));
      });
    return () => {
      cancelled = true;
    };
  }, [selectedDate, t]);

  // The month grid is tall enough that the day's timetable lands below the fold, so
  // a click could look like it did nothing. Deliberately not run on first paint —
  // today is selected on mount and jumping the page down then would be jarring.
  useEffect(() => {
    if (!scrollToTimetable.current) return;
    if (!selectedDate || day?.date !== selectedDate) return;
    scrollToTimetable.current = false;
    scrollPanelIntoView(timetableRef.current);
  }, [day, selectedDate]);

  // Stepping a day from the timetable. The calendar follows across a month
  // boundary, so the grid above never highlights a day it is no longer showing —
  // and no scroll, because the timetable is already what the reader is looking at.
  function stepDay(delta: number) {
    if (!selectedDate) return;
    const next = addDaysIso(selectedDate, delta);
    const nextDate = fromIsoDate(next);
    if (nextDate.getFullYear() !== year || nextDate.getMonth() !== month0) {
      setYear(nextDate.getFullYear());
      setMonth0(nextDate.getMonth());
    }
    setSelectedDate(next);
  }

  async function refreshAll() {
    await loadCalendar();
    if (selectedDate) setDay(await getScheduleDay(selectedDate));
  }

  const bars: CalendarBar[] =
    calendar?.bookings.map((booking) => ({
      id: booking.bookingId,
      startIso: booking.startDate,
      // endDate is inclusive here: the group is present on its departure day.
      endIso: booking.endDate,
      label: `${booking.organizationName} · ${t("schedule.people", {
        count: booking.headcount,
      })}`,
      muted: booking.status === "PendingDeposit",
    })) ?? [];

  // The badge shows how many people are in the center that day — the occupancy
  // the kitchen and staff plan around, not how many groups it is split into.
  const dayBadges = new Map<string, CalendarDayBadge>(
    calendar?.days.map((d) => [
      d.date,
      { meals: d.mealCount, activities: d.activityCount, people: d.peopleCount },
    ]) ?? [],
  );

  return (
    <AdminLayout>
      <div className="schedule-toolbar">
        <div>
          <h1>{t("schedule.title")}</h1>
          <p>{t("schedule.intro")}</p>
        </div>
        <div className="row-actions">
          {/* A Link rather than a button — it navigates. Styled as one so it reads
              as the section's action. */}
          <Link className="toolbar-button" to="/admin/posilki">
            {t("schedule.mealTimesLink")}
          </Link>
        </div>
      </div>

      {error && <p role="alert">{error}</p>}

      <div className="schedule-layout">
        <div className="schedule-main">
          <MonthCalendar
            year={year}
            month0={month0}
            bars={bars}
            dayBadges={dayBadges}
            selectedIso={selectedDate}
            onSelectDay={(iso) => {
              // Clicking the selected day again clears it — nothing to scroll to.
              const next = iso === selectedDate ? null : iso;
              scrollToTimetable.current = next !== null;
              setSelectedDate(next);
            }}
            onSelectBar={setSelectedBookingId}
            onMonthChange={(y, m) => {
              setYear(y);
              setMonth0(m);
            }}
          />

          {/* Compare dates so a stale day never renders under a new selection. */}
          <div ref={timetableRef} className="schedule-day-anchor">
            {selectedDate && day?.date === selectedDate ? (
              <DayTimetable
                day={day}
                onSelectGroup={setSelectedBookingId}
                onChangeDay={stepDay}
              />
            ) : (
              <p className="schedule-hint">{t("schedule.pickDay")}</p>
            )}
          </div>
        </div>

        {selectedBookingId && (
          <div ref={groupPanelRef} className="schedule-group-anchor">
            <GroupSchedulePanel
              bookingId={selectedBookingId}
              onClose={() => setSelectedBookingId(null)}
              onChanged={() => void refreshAll()}
              onLoaded={() => scrollPanelIntoView(groupPanelRef.current)}
            />
          </div>
        )}
      </div>
    </AdminLayout>
  );
}
