import { useEffect, useRef, useState } from "react";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import {
  bookingStatuses,
  checkScheduleConflicts,
  createScheduleEntry,
  deleteScheduleEntry,
  getBookingSchedule,
  getScheduleLocations,
  setBookingStatus,
  updateDietaryNotes,
  updateScheduleEntry,
  type BookingSchedule,
  type BookingStatus,
  type ScheduleConflict,
  type ScheduleEntry,
  type ScheduleEntryInput,
  type ScheduleLocations,
} from "../../api/admin";
import { formatDate, toTimeInput } from "../../utils/dates";
import { IconUtensils } from "../icons";
import ConfirmDialog from "../ConfirmDialog";
import ScheduleEntryForm from "./ScheduleEntryForm";
import GroupMealTimes from "./GroupMealTimes";
import GroupRooms from "./GroupRooms";
import { useAuth } from "../../auth/AuthContext";
import { scrollPanelIntoView } from "../../utils/scroll";

interface Props {
  bookingId: string;
  onClose: () => void;
  /** Lets the parent refresh the calendar after entries change. */
  onChanged?: () => void;
  /**
   * The day the panel was opened from, e.g. a click in the day timetable. When
   * set, the panel scrolls to that day's section instead of its own top once
   * loaded.
   */
  focusDate?: string | null;
  /**
   * Fired once this group's programme has arrived and the panel has its real
   * height. Parents scroll to the panel on this rather than on selection: while it
   * is still an empty loading box the page is too short to bring it to the top, so
   * the browser clamps the scroll and it lands halfway down.
   */
  onLoaded?: () => void;
}

/** Two entries on the same day whose times overlap — surfaced, not blocked. */
function overlaps(entry: ScheduleEntry, others: ScheduleEntry[]): boolean {
  return others.some(
    (other) =>
      other.id !== entry.id &&
      other.startTime < entry.endTime &&
      entry.startTime < other.endTime,
  );
}

export default function GroupSchedulePanel({
  bookingId,
  onClose,
  onChanged,
  focusDate,
  onLoaded,
}: Props) {
  const { t, i18n } = useTranslation();
  // A worker reads this panel and changes nothing in it. The API refuses the
  // writes either way; hiding them keeps the panel honest about what it offers.
  const { canEdit } = useAuth();
  const [schedule, setSchedule] = useState<BookingSchedule | null>(null);
  const [dietaryNotes, setDietaryNotes] = useState("");
  const [addingOn, setAddingOn] = useState<string | null>(null);
  const [editing, setEditing] = useState<ScheduleEntry | null>(null);
  const [deleteTarget, setDeleteTarget] = useState<ScheduleEntry | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [notice, setNotice] = useState<string | null>(null);
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
  // Held in a ref so an inline arrow from the parent cannot change the effects'
  // identity and re-trigger the fetch.
  const onLoadedRef = useRef(onLoaded);
  useEffect(() => {
    onLoadedRef.current = onLoaded;
  });
  const focusDateRef = useRef(focusDate);
  useEffect(() => {
    focusDateRef.current = focusDate;
  });
  const dayRefs = useRef(new Map<string, HTMLElement>());

  // Announced from an effect, not from the fetch callback: the state set there has
  // not been committed yet, so the panel is still an empty box and a parent that
  // scrolls to it would measure the wrong height. Once per group, so re-loading
  // after an edit does not yank the page back.
  const announcedFor = useRef<string | null>(null);
  useEffect(() => {
    if (!schedule || announcedFor.current === schedule.bookingId) return;
    announcedFor.current = schedule.bookingId;
    const target = focusDateRef.current
      ? dayRefs.current.get(focusDateRef.current)
      : null;
    if (target) {
      scrollPanelIntoView(target);
    } else {
      onLoadedRef.current?.();
    }
  }, [schedule]);

  async function reload() {
    const data = await getBookingSchedule(bookingId);
    setSchedule(data);
    setDietaryNotes(data.dietaryNotes ?? "");
  }

  useEffect(() => {
    let cancelled = false;
    void getBookingSchedule(bookingId)
      .then((data) => {
        if (!cancelled) {
          setSchedule(data);
          setDietaryNotes(data.dietaryNotes ?? "");
        }
      })
      .catch(() => {
        if (!cancelled) setError(t("schedule.loadError"));
      });
    return () => {
      cancelled = true;
    };
  }, [bookingId, t]);

  // Suggestions for the place field. Loaded once per group; a failure leaves the
  // field a plain text input rather than blocking the panel.
  useEffect(() => {
    let cancelled = false;
    void getScheduleLocations().then((data) => {
      if (!cancelled) setLocations(data);
    }, () => {});
    return () => {
      cancelled = true;
    };
  }, [bookingId]);

  function handleApiError(err: unknown) {
    if (isAxiosError(err) && err.response) {
      const detail = (err.response.data as { detail?: string } | undefined)?.detail;
      setError(detail ?? t("schedule.genericError"));
    } else {
      setError(t("schedule.genericError"));
    }
  }

  async function afterMutation() {
    await reload();
    onChanged?.();
  }

  async function handleStatusChange(status: BookingStatus) {
    setError(null);
    setNotice(null);
    try {
      await setBookingStatus(bookingId, status);
      setNotice(t("schedule.statusChanged", { status: t(`adminBookings.statuses.${status}`) }));
      await afterMutation();
    } catch (err) {
      handleApiError(err);
    }
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
   * Warn before a save that would put this group where another one already is, or
   * seat it while another is still eating — then save anyway if the admin confirms.
   * Advisory by design: the server does not enforce it, and a failed check must never
   * be what stops a legitimate save.
   */
  async function requestSave(input: ScheduleEntryInput, entry?: ScheduleEntry) {
    const save = () => (entry ? handleUpdate(entry, input) : handleAdd(input));
    setError(null);

    try {
      const { conflicts, mealGapMinutes } = await checkScheduleConflicts({
        bookingId,
        entryId: entry?.id,
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

  // The date travels inside `input` — ScheduleEntryForm sets it from the day
  // section the "+ add" button belongs to.
  async function handleAdd(input: ScheduleEntryInput) {
    setError(null);
    try {
      await createScheduleEntry({ ...input, bookingId });
      setAddingOn(null);
      await afterMutation();
    } catch (err) {
      handleApiError(err);
    }
  }

  async function handleUpdate(entry: ScheduleEntry, input: ScheduleEntryInput) {
    setError(null);
    try {
      await updateScheduleEntry(entry.id, { ...input, rowVersion: entry.rowVersion });
      setEditing(null);
      await afterMutation();
    } catch (err) {
      handleApiError(err);
    }
  }

  async function handleDelete(entry: ScheduleEntry) {
    setError(null);
    try {
      await deleteScheduleEntry(entry.id);
      setDeleteTarget(null);
      await afterMutation();
    } catch (err) {
      handleApiError(err);
    }
  }

  async function handleSaveDietaryNotes() {
    if (!schedule) return;
    setError(null);
    try {
      await updateDietaryNotes(bookingId, {
        dietaryNotes: dietaryNotes.trim() || null,
        rowVersion: schedule.bookingRowVersion,
      });
      setNotice(t("schedule.dietarySaved"));
      await reload();
    } catch (err) {
      handleApiError(err);
    }
  }

  if (!schedule) {
    return (
      <aside className="group-panel">
        <p>{error ?? t("common.loading")}</p>
      </aside>
    );
  }

  return (
    <aside className="group-panel">
      <div className="group-panel-header">
        <div>
          <h2>{schedule.organizationName}</h2>
          {/* The status is editable here rather than only on the dashboard, so it
              can also be changed from the calendar, which opens this same panel. */}
          {canEdit ? (
            <select
              className={`group-panel-status status-${schedule.status.toLowerCase()}`}
              value={schedule.status}
              aria-label={t("schedule.status")}
              onChange={(e) => void handleStatusChange(e.target.value as BookingStatus)}
            >
              {bookingStatuses.map((status) => (
                <option key={status} value={status}>
                  {t(`adminBookings.statuses.${status}`)}
                </option>
              ))}
            </select>
          ) : (
            <p className={`group-panel-status status-${schedule.status.toLowerCase()}`}>
              {t(`adminBookings.statuses.${schedule.status}`)}
            </p>
          )}
          <p className="group-panel-meta">
            {formatDate(schedule.startDate, i18n.language)} –{" "}
            {formatDate(schedule.endDate, i18n.language)} ·{" "}
            {t("schedule.people", { count: schedule.headcount })} ·{" "}
            {t("schedule.nights", { count: schedule.nights })}
          </p>
        </div>
        <button type="button" onClick={onClose} aria-label={t("schedule.close")}>
          ×
        </button>
      </div>

      {schedule.notes && (
        <p className="group-panel-note">
          <strong>{t("schedule.bookerNotes")}:</strong> {schedule.notes}
        </p>
      )}

      {canEdit ? (
        <>
          <label className="group-panel-dietary">
            {t("schedule.dietaryNotes")}
            <textarea
              value={dietaryNotes}
              onChange={(e) => setDietaryNotes(e.target.value)}
              rows={2}
              maxLength={2000}
              placeholder={t("schedule.dietaryPlaceholder")}
            />
          </label>
          {/* No "generate meals" button here: a stay is seeded when the group is
              created and again whenever its meal times are applied below. The
              schedule page keeps a range-wide backfill for groups that predate
              that. */}
          <div className="row-actions">
            <button type="button" onClick={() => void handleSaveDietaryNotes()}>
              {t("schedule.saveDietary")}
            </button>
          </div>
        </>
      ) : (
        schedule.dietaryNotes && (
          <p className="group-panel-note">
            <strong>{t("schedule.dietaryNotes")}:</strong> {schedule.dietaryNotes}
          </p>
        )
      )}

      {error && <p role="alert">{error}</p>}
      {notice && <p className="group-panel-notice">{notice}</p>}

      {/* Above the programme: where the group sleeps is the first thing asked when
          something has to be moved, and a room change feeds housekeeping too. Both
          render for a worker as well — read-only, like the rest of the panel. */}
      {/* Keyed by group so switching to another one starts both sub-panels clean
          instead of carrying over a half-finished edit — and keyed *distinctly*,
          because keys have to be unique among siblings: two children sharing one
          key is a reconciliation bug, not just a warning. */}
      <GroupRooms
        key={`rooms-${bookingId}`}
        bookingId={bookingId}
        onChanged={() => void afterMutation()}
      />

      <GroupMealTimes
        key={`meal-times-${bookingId}`}
        bookingId={bookingId}
        onChanged={() => void afterMutation()}
      />

      {schedule.days.map((day) => (
        <section
          className="group-panel-day"
          key={day.date}
          ref={(el) => {
            if (el) dayRefs.current.set(day.date, el);
            else dayRefs.current.delete(day.date);
          }}
        >
          <h3>
            {formatDate(day.date, i18n.language)}
            {day.isArrivalDay && ` · ${t("schedule.arrives")}`}
            {day.isDepartureDay && ` · ${t("schedule.departs")}`}
          </h3>

          <ul className="group-panel-entries">
            {day.entries.map((entry) =>
              editing?.id === entry.id ? (
                <li key={entry.id}>
                  <ScheduleEntryForm
                    date={day.date}
                    entry={entry}
                    groupHeadcount={schedule.headcount}
                    locations={locations.locations}
                    mealLocation={locations.mealLocation}
                    onSubmit={(input) => requestSave(input, entry)}
                    onCancel={() => setEditing(null)}
                  />
                </li>
              ) : (
                <li
                  key={entry.id}
                  className={`group-panel-entry${
                    overlaps(entry, day.entries) ? " overlapping" : ""
                  }`}
                >
                  <span className="entry-time">
                    {toTimeInput(entry.startTime)}–{toTimeInput(entry.endTime)}
                    {entry.timesCustomized && (
                      <em className="entry-custom" title={t("schedule.customTimeHint")}>
                        {t("schedule.customTime")}
                      </em>
                    )}
                  </span>
                  <span className="entry-meta">
                    <strong>
                      {entry.kind === "Meal" && <IconUtensils aria-hidden="true" />} {entry.title}
                    </strong>
                    {entry.location && <span className="entry-location">{entry.location}</span>}
                    {entry.menu && <span className="entry-menu">{entry.menu}</span>}
                    {entry.prepNotes && <span className="entry-prep">{entry.prepNotes}</span>}
                    {entry.participantCount != null && (
                      <span className="entry-participants">
                        {t("schedule.people", { count: entry.participantCount })}
                      </span>
                    )}
                  </span>
                  {canEdit && (
                    <span className="row-actions">
                      <button type="button" onClick={() => setEditing(entry)}>
                        {t("schedule.edit")}
                      </button>
                      <button type="button" onClick={() => setDeleteTarget(entry)}>
                        {t("schedule.delete")}
                      </button>
                    </span>
                  )}
                </li>
              ),
            )}

            {day.entries.length === 0 && addingOn !== day.date && (
              <li className="group-panel-empty">{t("schedule.noEntries")}</li>
            )}
          </ul>

          {canEdit &&
            (addingOn === day.date ? (
              <ScheduleEntryForm
                date={day.date}
                groupHeadcount={schedule.headcount}
                locations={locations.locations}
                mealLocation={locations.mealLocation}
                onSubmit={(input) => requestSave(input)}
                onCancel={() => setAddingOn(null)}
              />
            ) : (
              <button
                type="button"
                className="group-panel-add"
                onClick={() => setAddingOn(day.date)}
              >
                {t("schedule.addEntry")}
              </button>
            ))}
        </section>
      ))}

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

      {deleteTarget && (
        <ConfirmDialog
          title={t("schedule.deleteTitle")}
          message={t("schedule.deleteMessage", { title: deleteTarget.title })}
          confirmLabel={t("schedule.delete")}
          cancelLabel={t("schedule.form.cancel")}
          onConfirm={() => void handleDelete(deleteTarget)}
          onCancel={() => setDeleteTarget(null)}
        />
      )}
    </aside>
  );
}
