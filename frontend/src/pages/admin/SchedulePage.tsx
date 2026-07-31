import { useCallback, useEffect, useRef, useState } from "react";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import AdminLayout from "../../components/admin/AdminLayout";
import MonthCalendar, {
  type CalendarBar,
  type CalendarDayBadge,
} from "../../components/calendar/MonthCalendar";
import DayTimetable, { NEW_ENTRY_MINUTES } from "../../components/admin/DayTimetable";
import GroupSchedulePanel from "../../components/admin/GroupSchedulePanel";
import ScheduleEntryForm from "../../components/admin/ScheduleEntryForm";
import ConfirmDialog from "../../components/ConfirmDialog";
import {
  checkScheduleConflicts,
  createScheduleEntry,
  getScheduleCalendar,
  getScheduleDay,
  getScheduleLocations,
  type ScheduleCalendar,
  type ScheduleConflict,
  type ScheduleDay,
  type ScheduleEntryInput,
  type ScheduleLocations,
} from "../../api/admin";
import { addDaysIso, fromIsoDate, monthGrid, todayIso, toTimeInput } from "../../utils/dates";
import { scrollPanelIntoView } from "../../utils/scroll";
import { useAuth } from "../../auth/AuthContext";

function addMinutes(time: string, minutes: number): string {
  const [hours, mins] = time.split(":").map(Number);
  const total = Math.min(23 * 60 + 59, hours * 60 + mins + minutes);
  return `${String(Math.floor(total / 60)).padStart(2, "0")}:${String(total % 60).padStart(2, "0")}`;
}

const TODAY = todayIso();

// The camp schedule: a month of group stays, the hour-by-hour timetable for the
// selected day, and a side panel for editing one group's whole programme.
export default function SchedulePage() {
  const { t } = useTranslation();
  // The one admin page a worker may open, so it has to draw itself read-only.
  const { canEdit } = useAuth();
  const now = new Date();
  const [year, setYear] = useState(now.getFullYear());
  const [month0, setMonth0] = useState(now.getMonth());
  const [calendar, setCalendar] = useState<ScheduleCalendar | null>(null);
  const [selectedDate, setSelectedDate] = useState<string | null>(TODAY);
  const [day, setDay] = useState<ScheduleDay | null>(null);
  const [selectedBookingId, setSelectedBookingId] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);
  // The entry being added straight from the timetable — its times as the form
  // currently has them, so the grid can draw the slot it would take. Null when no
  // such entry is being written.
  const [pending, setPending] = useState<{ startTime: string; endTime: string } | null>(null);
  const [locations, setLocations] = useState<ScheduleLocations>({
    locations: [],
    mealLocation: null,
  });
  /** A save held back because another group already has that place or sitting.
   *  `run` is the save itself, so confirming lets exactly that write through. */
  const [conflictPrompt, setConflictPrompt] = useState<{
    details: string;
    run: () => Promise<void>;
  } | null>(null);
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

  // Place suggestions for the add form. Loaded once; a failure leaves the field a
  // plain text input rather than blocking the page.
  useEffect(() => {
    let cancelled = false;
    void getScheduleLocations().then(
      (data) => {
        if (!cancelled) setLocations(data);
      },
      () => {},
    );
    return () => {
      cancelled = true;
    };
  }, []);

  // The month grid is tall enough that the day's timetable lands below the fold, so
  // a click could look like it did nothing. Deliberately not run on first paint —
  // today is selected on mount and jumping the page down then would be jarring.
  useEffect(() => {
    if (!scrollToTimetable.current) return;
    if (!selectedDate || day?.date !== selectedDate) return;
    scrollToTimetable.current = false;
    scrollPanelIntoView(timetableRef.current);
  }, [day, selectedDate]);

  /** The one way the shown day changes. Closes any half-written entry with it: a
   *  form prefilled from yesterday's grid would silently write to the wrong day. */
  function selectDate(next: string | null) {
    setSelectedDate(next);
    setPending(null);
  }

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
    selectDate(next);
  }

  async function refreshAll() {
    await loadCalendar();
    if (selectedDate) setDay(await getScheduleDay(selectedDate));
  }

  function describeConflict(conflict: ScheduleConflict, gap: number): string {
    const when = `${toTimeInput(conflict.startTime)}–${toTimeInput(conflict.endTime)}`;
    return conflict.reason === "Location"
      ? t("schedule.conflict.location", {
          location: conflict.location,
          group: conflict.organizationName,
          when,
          title: conflict.title,
        })
      : t("schedule.conflict.meal", {
          group: conflict.organizationName,
          when,
          title: conflict.title,
          gap,
        });
  }

  /**
   * Adding an entry from the timetable itself: the group is a field in the form
   * rather than something to be selected first.
   *
   * Same advisory clash check the group panel does — this form is opened over a
   * day where every group's programme is visible at once, which is exactly where
   * double-booking a place is easiest.
   */
  async function handleAdd(input: ScheduleEntryInput, bookingId?: string) {
    if (!bookingId) return;
    setError(null);

    const save = async () => {
      try {
        await createScheduleEntry({ ...input, bookingId });
        setPending(null);
        await refreshAll();
      } catch {
        setError(t("schedule.genericError"));
      }
    };

    try {
      const { conflicts, mealGapMinutes } = await checkScheduleConflicts({
        bookingId,
        kind: input.kind,
        date: input.date,
        startTime: input.startTime,
        endTime: input.endTime,
        location: input.location,
      });
      if (conflicts.length > 0) {
        setConflictPrompt({
          details: conflicts.map((c) => describeConflict(c, mealGapMinutes)).join(" "),
          run: save,
        });
        return;
      }
    } catch {
      // Nothing to warn about that we can prove — fall through to the save.
    }

    await save();
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
        {/* Meal-time defaults are a centre-wide setting, so the link is only
            drawn for the role that may open that page. */}
        {canEdit && (
          <div className="row-actions">
            {/* A Link rather than a button — it navigates. Styled as one so it
                reads as the section's action. */}
            <Link className="toolbar-button" to="/admin/posilki">
              {t("schedule.mealTimesLink")}
            </Link>
          </div>
        )}
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
              selectDate(next);
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
              <>
                <DayTimetable
                  day={day}
                  onSelectGroup={setSelectedBookingId}
                  onChangeDay={stepDay}
                  onAddAt={
                    canEdit
                      ? (time) =>
                          setPending({
                            startTime: time,
                            endTime: addMinutes(time, NEW_ENTRY_MINUTES),
                          })
                      : undefined
                  }
                  pending={pending}
                />

                {/* Under the grid rather than over it: the timetable is what makes
                    the time and any clash obvious, so it stays readable — and keeps
                    drawing this entry's slot — while the entry is filled in. */}
                {pending && (
                  <div className="timetable-add-form">
                    <h3>{t("schedule.addAt", { time: pending.startTime })}</h3>
                    <ScheduleEntryForm
                      date={selectedDate}
                      defaultStartTime={pending.startTime}
                      defaultEndTime={pending.endTime}
                      groups={day.groups.map((group) => ({
                        bookingId: group.bookingId,
                        organizationName: group.organizationName,
                        headcount: group.headcount,
                      }))}
                      locations={locations.locations}
                      mealLocation={locations.mealLocation}
                      onSubmit={handleAdd}
                      onTimesChange={setPending}
                      onCancel={() => setPending(null)}
                    />
                  </div>
                )}
              </>
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

      {conflictPrompt && (
        <ConfirmDialog
          title={t("schedule.conflict.title")}
          message={t("schedule.conflict.message", { details: conflictPrompt.details })}
          confirmLabel={t("schedule.conflict.confirm")}
          cancelLabel={t("schedule.conflict.cancel")}
          onConfirm={() => {
            const { run } = conflictPrompt;
            setConflictPrompt(null);
            void run();
          }}
          onCancel={() => setConflictPrompt(null)}
        />
      )}
    </AdminLayout>
  );
}
