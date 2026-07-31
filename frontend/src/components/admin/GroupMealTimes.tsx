import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import {
  deleteBookingMeals,
  getBookingMealTimes,
  resetBookingMealTime,
  setBookingMealTime,
  MEAL_GAP_MINUTES,
  type BookingMealTime,
  type NeighbourSitting,
} from "../../api/admin";
import { fromTimeInput, toTimeInput } from "../../utils/dates";
import ConfirmDialog from "../ConfirmDialog";
import { useAuth } from "../../auth/AuthContext";

function minutesOf(time: string): number {
  const [hours, mins] = time.split(":");
  return Number(hours) * 60 + Number(mins);
}

/**
 * Groups sharing the centre that this proposed sitting would crowd. Two sittings
 * clash when either starts before the other has finished *and* had its changeover —
 * the kitchen cannot seat a second group while the first is still at the tables.
 */
function clashingNeighbours(
  start: string,
  end: string,
  neighbours: NeighbourSitting[],
): NeighbourSitting[] {
  const from = minutesOf(start);
  const to = minutesOf(end);
  return neighbours.filter((n) => {
    const nFrom = minutesOf(n.startTime);
    const nTo = minutesOf(n.endTime);
    return from < nTo + MEAL_GAP_MINUTES && nFrom < to + MEAL_GAP_MINUTES;
  });
}

interface Props {
  bookingId: string;
  /** Refresh the day list after a re-time moves entries. */
  onChanged: () => void;
}

/**
 * This group's own meal times. Changing one re-times that meal across the whole
 * stay in a single action — so groups can be staggered without editing every day —
 * while days moved individually for a one-off reason keep their times.
 */
export default function GroupMealTimes({ bookingId, onChanged }: Props) {
  const { t } = useTranslation();
  // Sittings are readable by both roles; re-timing them is a write.
  const { canEdit } = useAuth();
  const [mealTimes, setMealTimes] = useState<BookingMealTime[] | null>(null);
  const [drafts, setDrafts] = useState<Record<string, { start: string; end: string }>>({});
  const [busyId, setBusyId] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [notice, setNotice] = useState<string | null>(null);
  /** A save held back because it would crowd another group; confirming lets it through. */
  const [clashPrompt, setClashPrompt] = useState<{
    mealTime: BookingMealTime;
    groups: string;
  } | null>(null);
  const [deleteTarget, setDeleteTarget] = useState<BookingMealTime | null>(null);

  function applyToState(list: BookingMealTime[]) {
    setMealTimes(list);
    setDrafts(
      Object.fromEntries(
        list.map((m) => [
          m.mealTimeDefaultId,
          { start: toTimeInput(m.startTime), end: toTimeInput(m.endTime) },
        ]),
      ),
    );
  }

  useEffect(() => {
    let cancelled = false;
    void getBookingMealTimes(bookingId)
      .then((list) => {
        if (!cancelled) applyToState(list);
      })
      .catch(() => {
        if (!cancelled) setError(t("schedule.mealTimes.loadError"));
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

  function reportApplied(updated: number, skipped: number, created: number) {
    const applied =
      skipped > 0
        ? t("schedule.mealTimes.appliedWithExceptions", { updated, skipped })
        : t("schedule.mealTimes.applied", { updated });
    // Applying a time also seeds whatever the stay was missing, so say so rather
    // than leaving the new meals to appear unannounced.
    setNotice(
      created > 0 ? `${applied} ${t("schedule.mealTimes.alsoSeeded", { count: created })}` : applied,
    );
  }

  /** Warn first when the new time would crowd another group — then save anyway if
   *  the admin confirms. The server never blocks this; the check is advisory. */
  function requestSave(mealTime: BookingMealTime) {
    const draft = drafts[mealTime.mealTimeDefaultId];
    if (!draft) return;

    const clashes = clashingNeighbours(draft.start, draft.end, mealTime.neighbours);
    if (clashes.length > 0) {
      setClashPrompt({
        mealTime,
        groups: clashes
          .map((c) => `${c.organizationName} (${toTimeInput(c.startTime)}–${toTimeInput(c.endTime)})`)
          .join(", "),
      });
      return;
    }

    void save(mealTime);
  }

  async function save(mealTime: BookingMealTime) {
    const draft = drafts[mealTime.mealTimeDefaultId];
    if (!draft) return;
    setError(null);
    setNotice(null);
    setBusyId(mealTime.mealTimeDefaultId);
    try {
      const result = await setBookingMealTime(bookingId, mealTime.mealTimeDefaultId, {
        startTime: fromTimeInput(draft.start),
        endTime: fromTimeInput(draft.end),
        applyToExisting: true,
        rowVersion: mealTime.rowVersion,
      });
      applyToState(await getBookingMealTimes(bookingId));
      reportApplied(result.updated, result.skippedCustomized, result.created);
      onChanged();
    } catch (err) {
      handleApiError(err);
    } finally {
      setBusyId(null);
    }
  }

  async function reset(mealTime: BookingMealTime) {
    setError(null);
    setNotice(null);
    setBusyId(mealTime.mealTimeDefaultId);
    try {
      const result = await resetBookingMealTime(bookingId, mealTime.mealTimeDefaultId, true);
      applyToState(await getBookingMealTimes(bookingId));
      reportApplied(result.updated, result.skippedCustomized, result.created);
      onChanged();
    } catch (err) {
      handleApiError(err);
    } finally {
      setBusyId(null);
    }
  }

  async function removeAll(mealTime: BookingMealTime) {
    setError(null);
    setNotice(null);
    setBusyId(mealTime.mealTimeDefaultId);
    try {
      const { deleted } = await deleteBookingMeals(bookingId, mealTime.mealTimeDefaultId);
      setNotice(t("schedule.mealTimes.deletedAll", { count: deleted, label: mealTime.label }));
      onChanged();
    } catch (err) {
      handleApiError(err);
    } finally {
      setBusyId(null);
      setDeleteTarget(null);
    }
  }

  if (!mealTimes) {
    return null;
  }

  return (
    <section className="group-meal-times">
      <h3>{t("schedule.mealTimes.title")}</h3>
      <p className="group-panel-meta">{t("schedule.mealTimes.intro")}</p>

      {error && <p role="alert">{error}</p>}
      {notice && <p className="group-panel-notice">{notice}</p>}

      {mealTimes.length === 0 && <p>{t("schedule.mealTimes.empty")}</p>}

      <ul className="group-meal-times-list">
        {mealTimes.map((mealTime) => {
          const draft = drafts[mealTime.mealTimeDefaultId];
          const changed =
            draft &&
            (draft.start !== toTimeInput(mealTime.startTime) ||
              draft.end !== toTimeInput(mealTime.endTime));
          return (
            <li key={mealTime.mealTimeDefaultId}>
              <span className="gmt-label">
                {mealTime.label}
                {mealTime.isOverridden && (
                  <em className="gmt-badge" title={t("schedule.mealTimes.overriddenHint")}>
                    {t("schedule.mealTimes.overridden")}
                  </em>
                )}
                {/* Seated past the window's close — more groups than it holds. */}
                {mealTime.exceedsWindow && (
                  <em
                    className="gmt-badge gmt-badge-warn"
                    title={t("schedule.mealTimes.exceedsHint", {
                      end: toTimeInput(mealTime.defaultEndTime),
                    })}
                  >
                    {t("schedule.mealTimes.exceeds")}
                  </em>
                )}
              </span>

              <span className="gmt-times">
                <input
                  type="time"
                  aria-label={t("schedule.form.startTime")}
                  readOnly={!canEdit}
                  value={draft?.start ?? ""}
                  onChange={(e) =>
                    setDrafts({
                      ...drafts,
                      [mealTime.mealTimeDefaultId]: {
                        start: e.target.value,
                        end: draft?.end ?? "",
                      },
                    })
                  }
                />
                <input
                  type="time"
                  aria-label={t("schedule.form.endTime")}
                  readOnly={!canEdit}
                  value={draft?.end ?? ""}
                  onChange={(e) =>
                    setDrafts({
                      ...drafts,
                      [mealTime.mealTimeDefaultId]: {
                        start: draft?.start ?? "",
                        end: e.target.value,
                      },
                    })
                  }
                />
              </span>

              {canEdit && (
              <span className="row-actions">
                <button
                  type="button"
                  disabled={!changed || busyId === mealTime.mealTimeDefaultId}
                  onClick={() => requestSave(mealTime)}
                >
                  {t("schedule.mealTimes.apply")}
                </button>
                <button
                  type="button"
                  disabled={busyId === mealTime.mealTimeDefaultId}
                  onClick={() => setDeleteTarget(mealTime)}
                  title={t("schedule.mealTimes.deleteAllHint", { label: mealTime.label })}
                >
                  {t("schedule.mealTimes.deleteAll")}
                </button>
                {mealTime.isOverridden && (
                  <button
                    type="button"
                    disabled={busyId === mealTime.mealTimeDefaultId}
                    onClick={() => void reset(mealTime)}
                    title={t("schedule.mealTimes.resetHint", {
                      start: toTimeInput(mealTime.defaultStartTime),
                      end: toTimeInput(mealTime.defaultEndTime),
                    })}
                  >
                    {t("schedule.mealTimes.reset")}
                  </button>
                )}
              </span>
              )}
            </li>
          );
        })}
      </ul>

      {clashPrompt && (
        <ConfirmDialog
          title={t("schedule.mealTimes.clashTitle")}
          message={t("schedule.mealTimes.clashMessage", {
            groups: clashPrompt.groups,
            gap: MEAL_GAP_MINUTES,
          })}
          confirmLabel={t("schedule.mealTimes.clashConfirm")}
          cancelLabel={t("schedule.mealTimes.clashCancel")}
          onConfirm={() => {
            const { mealTime } = clashPrompt;
            setClashPrompt(null);
            void save(mealTime);
          }}
          onCancel={() => setClashPrompt(null)}
        />
      )}

      {deleteTarget && (
        <ConfirmDialog
          title={t("schedule.mealTimes.deleteAllTitle", { label: deleteTarget.label })}
          message={t("schedule.mealTimes.deleteAllMessage", { label: deleteTarget.label })}
          confirmLabel={t("schedule.mealTimes.deleteAllConfirm")}
          cancelLabel={t("schedule.mealTimes.clashCancel")}
          onConfirm={() => void removeAll(deleteTarget)}
          onCancel={() => setDeleteTarget(null)}
        />
      )}
    </section>
  );
}
