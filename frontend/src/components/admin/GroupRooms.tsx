import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import {
  getAdminBooking,
  getAssignableRooms,
  reassignBooking,
  type AdminBooking,
  type AssignableRoom,
} from "../../api/admin";

interface Props {
  bookingId: string;
  /** Rooms drive housekeeping and the occupancy grid, so a move has to refresh them. */
  onChanged: () => void;
}

/** One row being edited: which room, and how many of the group sleep in it. */
interface Draft {
  roomId: string;
  peopleCount: number;
}

/**
 * The rooms a group is in, and the way to move it between them.
 *
 * Kept editable rather than read-only because rooms change after the booking is
 * made for reasons no availability check predicts — a broken heater, a child who
 * cannot manage stairs, two groups that should not share a corridor. The whole
 * set is saved at once, since the server takes the assignments wholesale and
 * validates them as a set: every person housed, no room twice.
 */
export default function GroupRooms({ bookingId, onChanged }: Props) {
  const { t } = useTranslation();
  const [booking, setBooking] = useState<AdminBooking | null>(null);
  const [rooms, setRooms] = useState<AssignableRoom[]>([]);
  const [drafts, setDrafts] = useState<Draft[] | null>(null);
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [notice, setNotice] = useState<string | null>(null);

  useEffect(() => {
    let cancelled = false;
    setDrafts(null);
    setNotice(null);
    void Promise.all([getAdminBooking(bookingId), getAssignableRooms(bookingId)])
      .then(([bookingData, roomData]) => {
        if (cancelled) return;
        setBooking(bookingData);
        setRooms(roomData);
        setError(null);
      })
      .catch(() => {
        if (!cancelled) setError(t("groupRooms.loadError"));
      });
    return () => {
      cancelled = true;
    };
  }, [bookingId, t]);

  function startEditing() {
    if (!booking) return;
    setNotice(null);
    setError(null);
    setDrafts(
      booking.assignments.map((a) => ({ roomId: a.roomId, peopleCount: a.peopleCount })),
    );
  }

  function updateDraft(index: number, patch: Partial<Draft>) {
    setDrafts((current) =>
      current?.map((draft, i) => (i === index ? { ...draft, ...patch } : draft)) ?? null,
    );
  }

  /** The first room not already in the draft — what an added row starts on. */
  function firstUnusedRoom(current: Draft[]): AssignableRoom | undefined {
    return rooms.find((room) => !current.some((draft) => draft.roomId === room.roomId));
  }

  function addRow() {
    setDrafts((current) => {
      if (!current) return current;
      const room = firstUnusedRoom(current);
      return room ? [...current, { roomId: room.roomId, peopleCount: 1 }] : current;
    });
  }

  function removeRow(index: number) {
    setDrafts((current) => current?.filter((_, i) => i !== index) ?? null);
  }

  async function save() {
    if (!drafts || !booking) return;
    setSaving(true);
    setError(null);
    try {
      await reassignBooking(bookingId, drafts);
      const [bookingData, roomData] = await Promise.all([
        getAdminBooking(bookingId),
        getAssignableRooms(bookingId),
      ]);
      setBooking(bookingData);
      setRooms(roomData);
      setDrafts(null);
      setNotice(t("groupRooms.saved"));
      onChanged();
    } catch (err) {
      // The server owns the rules here — a room taken in the meantime, a count that
      // does not add up — so show what it said rather than guessing at it.
      const detail = isAxiosError(err)
        ? (err.response?.data as { detail?: string } | undefined)?.detail
        : undefined;
      setError(detail ?? t("groupRooms.saveError"));
    } finally {
      setSaving(false);
    }
  }

  if (!booking) {
    return (
      <section className="group-rooms">
        <h3>{t("groupRooms.title")}</h3>
        <p>{error ?? t("common.loading")}</p>
      </section>
    );
  }

  const placed = drafts?.reduce((sum, draft) => sum + (draft.peopleCount || 0), 0) ?? 0;
  const balanced = placed === booking.headcount;
  const duplicated = drafts ? new Set(drafts.map((d) => d.roomId)).size !== drafts.length : false;
  const canSave = !!drafts && drafts.length > 0 && balanced && !duplicated && !saving;
  const roomsLeftToAdd = drafts ? !!firstUnusedRoom(drafts) : false;
  // Rooms that were free when the panel loaded, so a stale list is visible as such
  // rather than only surfacing when the save is rejected.
  const editable = booking.status !== "Cancelled" && booking.status !== "Completed";

  return (
    <section className="group-rooms">
      <h3>{t("groupRooms.title")}</h3>

      {!drafts ? (
        <>
          <ul className="group-rooms-list">
            {booking.assignments.map((assignment) => (
              <li key={assignment.id}>
                <strong className="group-rooms-number">{assignment.roomNumber}</strong>
                <span className="group-rooms-people">
                  {t("groupRooms.people", { count: assignment.peopleCount })}
                </span>
                <span className="group-rooms-capacity">
                  {t("groupRooms.capacity", { count: assignment.capacity })}
                  {/* An admin may overfill a room on purpose (an extra bed); say so
                      rather than hiding it, because housekeeping has to set it up. */}
                  {assignment.peopleCount > assignment.capacity && (
                    <em className="group-rooms-over" title={t("groupRooms.overCapacityHint")}>
                      {t("groupRooms.overCapacity")}
                    </em>
                  )}
                </span>
              </li>
            ))}
            {booking.assignments.length === 0 && (
              <li className="group-rooms-empty">{t("groupRooms.none")}</li>
            )}
          </ul>

          {editable ? (
            <div className="row-actions">
              <button type="button" onClick={startEditing}>
                {t("groupRooms.change")}
              </button>
            </div>
          ) : (
            <p className="group-rooms-hint">{t("groupRooms.notEditable")}</p>
          )}
        </>
      ) : (
        <>
          <ul className="group-rooms-edit">
            {drafts.map((draft, index) => {
              const taken = drafts.some((other, i) => i !== index && other.roomId === draft.roomId);
              return (
                <li key={index} className={taken ? "duplicate" : undefined}>
                  <select
                    value={draft.roomId}
                    aria-label={t("groupRooms.room")}
                    onChange={(e) => updateDraft(index, { roomId: e.target.value })}
                  >
                    {rooms.map((room) => (
                      <option key={room.roomId} value={room.roomId}>
                        {t("groupRooms.roomOption", {
                          number: room.roomNumber,
                          count: room.capacity,
                        })}
                      </option>
                    ))}
                    {/* A room the group holds that is no longer offered (deactivated
                        mid-stay) would otherwise silently become another room. */}
                    {!rooms.some((room) => room.roomId === draft.roomId) && (
                      <option value={draft.roomId}>
                        {booking.assignments.find((a) => a.roomId === draft.roomId)?.roomNumber ??
                          t("groupRooms.unknownRoom")}
                      </option>
                    )}
                  </select>
                  <input
                    type="number"
                    min={1}
                    max={999}
                    value={draft.peopleCount}
                    aria-label={t("groupRooms.peopleLabel")}
                    onChange={(e) =>
                      updateDraft(index, { peopleCount: Number(e.target.value) || 0 })
                    }
                  />
                  <button
                    type="button"
                    disabled={drafts.length === 1}
                    title={t("groupRooms.removeHint")}
                    onClick={() => removeRow(index)}
                  >
                    {t("groupRooms.remove")}
                  </button>
                </li>
              );
            })}
          </ul>

          {/* The running total is the thing that makes a move safe: people are moved
              between rooms one field at a time, and this says when everyone is housed. */}
          <p className={`group-rooms-total${balanced ? " balanced" : " off"}`}>
            {t("groupRooms.placed", { placed, headcount: booking.headcount })}
            {!balanced && ` · ${t("groupRooms.mustMatch")}`}
            {duplicated && ` · ${t("groupRooms.duplicate")}`}
          </p>

          <div className="row-actions">
            <button type="button" disabled={!roomsLeftToAdd} onClick={addRow}>
              {t("groupRooms.addRoom")}
            </button>
            <button type="button" disabled={!canSave} onClick={() => void save()}>
              {saving ? t("groupRooms.saving") : t("groupRooms.save")}
            </button>
            <button type="button" onClick={() => setDrafts(null)}>
              {t("groupRooms.cancel")}
            </button>
          </div>
        </>
      )}

      {error && <p role="alert">{error}</p>}
      {notice && <p className="group-panel-notice">{notice}</p>}
    </section>
  );
}
