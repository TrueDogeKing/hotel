import { useCallback, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import AdminLayout from "../../components/admin/AdminLayout";
import {
  getHousekeepingDay,
  getHousekeepingRange,
  roomCleaningStatuses,
  setRoomCleaning,
  type HousekeepingDay,
  type HousekeepingRange,
  type HousekeepingRoom,
  type RoomCleaningStatus,
} from "../../api/admin";
import { addDaysIso, formatDate, todayIso } from "../../utils/dates";

/** Days either side of the chosen one in the strip: yesterday for anything left over,
 *  and a week ahead, which is as far as "what do we prepare next" usefully reaches. */
const STRIP_BEFORE = 1;
const STRIP_AFTER = 7;
/** How far the arrows either side of the strip jump. A whole strip at a time, so
 *  looking a month out is four clicks rather than thirty. */
const STRIP_PAGE = STRIP_BEFORE + STRIP_AFTER + 1;

/** Rooms are grouped under these headings, in this order — a turnaround has to be
 *  emptied and remade before the next group walks in, so it leads. */
const kindOrder = ["Turnaround", "Departure", "Arrival"] as const;

/**
 * The housekeeping round. One day at a time, because that is how the work happens: the
 * rooms a group has just vacated (check and clean), the rooms the next group moves into
 * today (have them ready), and the tight ones that are both.
 *
 * The list is derived from the room assignments server-side, so a reassignment or a
 * cancellation shows up here immediately; only each room's progress is stored.
 */
export default function HousekeepingPage() {
  const { t, i18n } = useTranslation();
  const [date, setDate] = useState(todayIso);
  const [day, setDay] = useState<HousekeepingDay | null>(null);
  const [strip, setStrip] = useState<HousekeepingRange | null>(null);
  // Days the strip is shifted from the chosen day, so the coming weeks can be
  // scanned without moving the round itself off the day being worked on.
  const [stripOffset, setStripOffset] = useState(0);
  const [busyRoomId, setBusyRoomId] = useState<string | null>(null);
  const [noteFor, setNoteFor] = useState<string | null>(null);
  const [noteDraft, setNoteDraft] = useState("");
  const [error, setError] = useState<string | null>(null);

  /** The day and its strip come together: marking a room done changes both the list and
   *  that day's counter, and fetching them separately would show the two disagreeing. */
  const fetchDay = useCallback(
    (day: string, offset: number) =>
      Promise.all([
        getHousekeepingDay(day),
        getHousekeepingRange(
          addDaysIso(day, offset - STRIP_BEFORE),
          addDaysIso(day, offset + STRIP_AFTER),
        ),
      ]),
    [],
  );

  const reload = useCallback(async () => {
    const [dayData, stripData] = await fetchDay(date, stripOffset);
    setDay(dayData);
    setStrip(stripData);
  }, [date, stripOffset, fetchDay]);

  /** Choosing a day re-centres the strip on it: the window you browse with is not
   *  meant to outlive the jump it was used to make. */
  function selectDate(iso: string) {
    setDate(iso);
    setStripOffset(0);
  }

  useEffect(() => {
    let cancelled = false;
    void fetchDay(date, stripOffset)
      .then(([dayData, stripData]) => {
        if (cancelled) return;
        setDay(dayData);
        setStrip(stripData);
        setError(null);
      })
      .catch(() => {
        if (!cancelled) setError(t("housekeeping.loadError"));
      });
    return () => {
      cancelled = true;
    };
  }, [date, stripOffset, fetchDay, t]);

  async function apply(room: HousekeepingRoom, status: RoomCleaningStatus, note?: string | null) {
    setBusyRoomId(room.roomId);
    setError(null);
    try {
      await setRoomCleaning(date, room.roomId, {
        status,
        // `undefined` means "leave the note as it is"; the API always takes the note it
        // is given, so pass the current one back.
        note: note === undefined ? room.note : note,
      });
      setNoteFor(null);
      await reload();
    } catch (err) {
      const detail = isAxiosError(err)
        ? (err.response?.data as { detail?: string } | undefined)?.detail
        : undefined;
      setError(detail ?? t("housekeeping.saveError"));
    } finally {
      setBusyRoomId(null);
    }
  }

  /** Pending → InProgress → Done → Pending: one control for the whole round, which is
   *  what someone working through rooms on a phone actually wants. */
  function nextStatus(status: RoomCleaningStatus): RoomCleaningStatus {
    const index = roomCleaningStatuses.indexOf(status);
    return roomCleaningStatuses[(index + 1) % roomCleaningStatuses.length];
  }

  const outstanding = day ? day.rooms.length - day.doneCount : 0;

  return (
    <AdminLayout>
      <h1>{t("housekeeping.title")}</h1>
      <p>{t("housekeeping.intro")}</p>

      <div className="hk-toolbar">
        <button type="button" onClick={() => selectDate(addDaysIso(date, -1))}>
          {t("housekeeping.previousDay")}
        </button>
        <input
          type="date"
          value={date}
          onChange={(e) => selectDate(e.target.value || todayIso())}
        />
        <button type="button" onClick={() => selectDate(addDaysIso(date, 1))}>
          {t("housekeeping.nextDay")}
        </button>
        <button type="button" onClick={() => selectDate(todayIso())}>
          {t("housekeeping.today")}
        </button>
      </div>

      {/* Which of the coming days still have rooms outstanding — the reason to look
          ahead at all is to see a heavy changeover before the morning it lands. */}
      {strip && (
        <div className="hk-strip-row">
          <button
            type="button"
            className="hk-strip-nav"
            aria-label={t("housekeeping.stripBack")}
            title={t("housekeeping.stripBack")}
            onClick={() => setStripOffset(stripOffset - STRIP_PAGE)}
          >
            ‹
          </button>

          <ul className="hk-strip">
            {strip.days.map((summary) => {
              const left = summary.roomCount - summary.doneCount;
              return (
                <li key={summary.date}>
                  <button
                    type="button"
                    className={`hk-strip-day${summary.date === date ? " selected" : ""}${
                      summary.roomCount === 0 ? " empty" : left === 0 ? " clear" : ""
                    }`}
                    aria-current={summary.date === date}
                    onClick={() => selectDate(summary.date)}
                  >
                    <span className="hk-strip-date">
                      {formatDate(summary.date, i18n.language)}
                    </span>
                    <span className="hk-strip-count">
                      {summary.roomCount === 0
                        ? t("housekeeping.stripNone")
                        : t("housekeeping.stripCount", {
                            done: summary.doneCount,
                            total: summary.roomCount,
                          })}
                    </span>
                  </button>
                </li>
              );
            })}
          </ul>

          <button
            type="button"
            className="hk-strip-nav"
            aria-label={t("housekeeping.stripForward")}
            title={t("housekeeping.stripForward")}
            onClick={() => setStripOffset(stripOffset + STRIP_PAGE)}
          >
            ›
          </button>
        </div>
      )}

      {/* Paged away from the day being worked on, the strip no longer contains it —
          say so, and offer the way back. */}
      {stripOffset !== 0 && (
        <p className="hk-strip-note">
          {t("housekeeping.stripAway", { date: formatDate(date, i18n.language) })}{" "}
          <button type="button" className="hk-strip-reset" onClick={() => setStripOffset(0)}>
            {t("housekeeping.stripBackToDay")}
          </button>
        </p>
      )}

      {error && <p role="alert">{error}</p>}

      {!day ? (
        <p>{t("common.loading")}</p>
      ) : day.rooms.length === 0 ? (
        <p className="hk-empty">{t("housekeeping.noRooms")}</p>
      ) : (
        <>
          <p className="hk-summary">
            {t("housekeeping.summary", { done: day.doneCount, total: day.rooms.length })}
            {outstanding > 0 && ` · ${t("housekeeping.outstanding", { count: outstanding })}`}
          </p>

          {kindOrder.map((kind) => {
            const rooms = day.rooms.filter((room) => room.kind === kind);
            if (rooms.length === 0) return null;

            return (
              <section className="hk-section" key={kind}>
                <h2>
                  {t(`housekeeping.kinds.${kind}`)} <span className="hk-count">{rooms.length}</span>
                </h2>
                <p className="hk-section-hint">{t(`housekeeping.kindHints.${kind}`)}</p>

                <ul className="hk-rooms">
                  {rooms.map((room) => (
                    <li
                      key={room.roomId}
                      className={`hk-room ${kind.toLowerCase()} status-${room.status.toLowerCase()}`}
                    >
                      <span className="hk-room-head">
                        <strong className="hk-room-number">{room.roomNumber}</strong>
                        <span className="hk-room-beds">
                          {t("housekeeping.beds", { count: room.capacity })}
                        </span>
                        {room.openTaskCount > 0 && (
                          <span
                            className="hk-room-tasks"
                            title={t("housekeeping.openTasksHint", { count: room.openTaskCount })}
                          >
                            {t("housekeeping.openTasks", { count: room.openTaskCount })}
                          </span>
                        )}
                        {room.closed && (
                          <span className="hk-room-closed" title={room.closureReason ?? undefined}>
                            {t("housekeeping.closed")}
                          </span>
                        )}
                      </span>

                      <span className="hk-room-groups">
                        {room.outgoingOrganizationName && (
                          <span className="hk-out">
                            {t("housekeeping.outgoing", {
                              group: room.outgoingOrganizationName,
                              count: room.outgoingPeopleCount ?? 0,
                            })}
                          </span>
                        )}
                        {room.incomingOrganizationName && (
                          <span className="hk-in">
                            {t("housekeeping.incoming", {
                              group: room.incomingOrganizationName,
                              count: room.incomingPeopleCount ?? 0,
                            })}
                          </span>
                        )}
                      </span>

                      {room.note && <span className="hk-room-note">{room.note}</span>}

                      <span className="hk-room-actions">
                        <button
                          type="button"
                          className={`hk-status hk-status-${room.status.toLowerCase()}`}
                          disabled={busyRoomId === room.roomId}
                          onClick={() => void apply(room, nextStatus(room.status))}
                          title={t("housekeeping.advanceHint", {
                            next: t(`housekeeping.statuses.${nextStatus(room.status)}`),
                          })}
                        >
                          {t(`housekeeping.statuses.${room.status}`)}
                        </button>
                        <button
                          type="button"
                          className="hk-note-toggle"
                          onClick={() => {
                            setNoteFor(noteFor === room.roomId ? null : room.roomId);
                            setNoteDraft(room.note ?? "");
                          }}
                        >
                          {room.note ? t("housekeeping.editNote") : t("housekeeping.addNote")}
                        </button>
                      </span>

                      {noteFor === room.roomId && (
                        <span className="hk-note-form">
                          <input
                            value={noteDraft}
                            onChange={(e) => setNoteDraft(e.target.value)}
                            maxLength={1000}
                            placeholder={t("housekeeping.notePlaceholder")}
                            aria-label={t("housekeeping.note")}
                          />
                          <button
                            type="button"
                            disabled={busyRoomId === room.roomId}
                            onClick={() => void apply(room, room.status, noteDraft.trim() || null)}
                          >
                            {t("housekeeping.saveNote")}
                          </button>
                          <button type="button" onClick={() => setNoteFor(null)}>
                            {t("housekeeping.cancelNote")}
                          </button>
                        </span>
                      )}
                    </li>
                  ))}
                </ul>
              </section>
            );
          })}
        </>
      )}
    </AdminLayout>
  );
}
