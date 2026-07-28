import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import {
  getBookingMealTimes,
  resetBookingMealTime,
  setBookingMealTime,
  type BookingMealTime,
} from "../../api/admin";
import { fromTimeInput, toTimeInput } from "../../utils/dates";

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
  const [mealTimes, setMealTimes] = useState<BookingMealTime[] | null>(null);
  const [drafts, setDrafts] = useState<Record<string, { start: string; end: string }>>({});
  const [busyId, setBusyId] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [notice, setNotice] = useState<string | null>(null);

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

  function reportApplied(updated: number, skipped: number) {
    setNotice(
      skipped > 0
        ? t("schedule.mealTimes.appliedWithExceptions", { updated, skipped })
        : t("schedule.mealTimes.applied", { updated }),
    );
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
      reportApplied(result.updated, result.skippedCustomized);
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
      reportApplied(result.updated, result.skippedCustomized);
      onChanged();
    } catch (err) {
      handleApiError(err);
    } finally {
      setBusyId(null);
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
              </span>

              <span className="gmt-times">
                <input
                  type="time"
                  aria-label={t("schedule.form.startTime")}
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

              <span className="row-actions">
                <button
                  type="button"
                  disabled={!changed || busyId === mealTime.mealTimeDefaultId}
                  onClick={() => void save(mealTime)}
                >
                  {t("schedule.mealTimes.apply")}
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
            </li>
          );
        })}
      </ul>
    </section>
  );
}
