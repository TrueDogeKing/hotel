import { useCallback, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import AdminLayout from "../../components/admin/AdminLayout";
import MonthCalendar, {
  type CalendarBar,
  type CalendarDayBadge,
} from "../../components/calendar/MonthCalendar";
import DayTimetable from "../../components/admin/DayTimetable";
import GroupSchedulePanel from "../../components/admin/GroupSchedulePanel";
import {
  generateMissingMeals,
  getScheduleCalendar,
  getScheduleDay,
  type ScheduleCalendar,
  type ScheduleDay,
} from "../../api/admin";
import { monthGrid, todayIso } from "../../utils/dates";

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
  const [notice, setNotice] = useState<string | null>(null);

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

  async function refreshAll() {
    await loadCalendar();
    if (selectedDate) setDay(await getScheduleDay(selectedDate));
  }

  async function handleBackfill() {
    setError(null);
    try {
      const { bookings, created } = await generateMissingMeals(gridStart, gridEnd);
      setNotice(t("schedule.backfillDone", { bookings, created }));
      await refreshAll();
    } catch (err) {
      if (isAxiosError(err) && err.response) {
        const detail = (err.response.data as { detail?: string } | undefined)?.detail;
        setError(detail ?? t("schedule.genericError"));
      } else {
        setError(t("schedule.genericError"));
      }
    }
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
          <button type="button" onClick={() => void handleBackfill()}>
            {t("schedule.backfill")}
          </button>
          <Link to="/admin/posilki">{t("schedule.mealTimesLink")}</Link>
        </div>
      </div>

      {error && <p role="alert">{error}</p>}
      {notice && <p className="group-panel-notice">{notice}</p>}

      <div className="schedule-layout">
        <div className="schedule-main">
          <MonthCalendar
            year={year}
            month0={month0}
            bars={bars}
            dayBadges={dayBadges}
            selectedIso={selectedDate}
            onSelectDay={(iso) => setSelectedDate(iso === selectedDate ? null : iso)}
            onSelectBar={setSelectedBookingId}
            onMonthChange={(y, m) => {
              setYear(y);
              setMonth0(m);
            }}
          />

          {/* Compare dates so a stale day never renders under a new selection. */}
          {selectedDate && day?.date === selectedDate ? (
            <DayTimetable day={day} onSelectGroup={setSelectedBookingId} />
          ) : (
            <p className="schedule-hint">{t("schedule.pickDay")}</p>
          )}
        </div>

        {selectedBookingId && (
          <GroupSchedulePanel
            bookingId={selectedBookingId}
            onClose={() => setSelectedBookingId(null)}
            onChanged={() => void refreshAll()}
          />
        )}
      </div>
    </AdminLayout>
  );
}
