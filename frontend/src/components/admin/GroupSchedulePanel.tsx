import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import {
  bookingStatuses,
  createScheduleEntry,
  deleteScheduleEntry,
  getBookingSchedule,
  setBookingStatus,
  updateDietaryNotes,
  updateScheduleEntry,
  type BookingSchedule,
  type BookingStatus,
  type ScheduleEntry,
  type ScheduleEntryInput,
} from "../../api/admin";
import { formatDate, toTimeInput } from "../../utils/dates";
import { IconUtensils } from "../icons";
import ConfirmDialog from "../ConfirmDialog";
import ScheduleEntryForm from "./ScheduleEntryForm";
import GroupMealTimes from "./GroupMealTimes";

interface Props {
  bookingId: string;
  onClose: () => void;
  /** Lets the parent refresh the calendar after entries change. */
  onChanged?: () => void;
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

export default function GroupSchedulePanel({ bookingId, onClose, onChanged }: Props) {
  const { t, i18n } = useTranslation();
  const [schedule, setSchedule] = useState<BookingSchedule | null>(null);
  const [dietaryNotes, setDietaryNotes] = useState("");
  const [addingOn, setAddingOn] = useState<string | null>(null);
  const [editing, setEditing] = useState<ScheduleEntry | null>(null);
  const [deleteTarget, setDeleteTarget] = useState<ScheduleEntry | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [notice, setNotice] = useState<string | null>(null);

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
          created and again whenever its meal times are applied below. The schedule
          page keeps a range-wide backfill for groups that predate that. */}
      <div className="row-actions">
        <button type="button" onClick={() => void handleSaveDietaryNotes()}>
          {t("schedule.saveDietary")}
        </button>
      </div>

      {error && <p role="alert">{error}</p>}
      {notice && <p className="group-panel-notice">{notice}</p>}

      <GroupMealTimes bookingId={bookingId} onChanged={() => void afterMutation()} />

      {schedule.days.map((day) => (
        <section className="group-panel-day" key={day.date}>
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
                    onSubmit={(input) => handleUpdate(entry, input)}
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
                  </span>
                  <span className="row-actions">
                    <button type="button" onClick={() => setEditing(entry)}>
                      {t("schedule.edit")}
                    </button>
                    <button type="button" onClick={() => setDeleteTarget(entry)}>
                      {t("schedule.delete")}
                    </button>
                  </span>
                </li>
              ),
            )}

            {day.entries.length === 0 && addingOn !== day.date && (
              <li className="group-panel-empty">{t("schedule.noEntries")}</li>
            )}
          </ul>

          {addingOn === day.date ? (
            <ScheduleEntryForm
              date={day.date}
              onSubmit={handleAdd}
              onCancel={() => setAddingOn(null)}
            />
          ) : (
            <button type="button" className="group-panel-add" onClick={() => setAddingOn(day.date)}>
              {t("schedule.addEntry")}
            </button>
          )}
        </section>
      ))}

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
