import { useId, useState, type FormEvent } from "react";
import { useTranslation } from "react-i18next";
import {
  mealKinds,
  type MealKind,
  type ScheduleEntry,
  type ScheduleEntryInput,
  type ScheduleEntryKind,
} from "../../api/admin";
import { fromTimeInput, toTimeInput } from "../../utils/dates";

interface Props {
  date: string;
  /** Set when editing; omitted when adding. */
  entry?: ScheduleEntry;
  defaultKind?: ScheduleEntryKind;
  /** The group's full headcount — the default participant count for a meal or
   * activity entry, and the threshold past which the form warns rather than blocks. */
  groupHeadcount: number;
  /** Places already used elsewhere in the schedule, offered as suggestions: clash
   *  detection compares places as text, so "Sala A" and "sala a " are two rooms. */
  locations?: string[];
  /** Where meals are served. Filled in for a meal that has no place yet. */
  mealLocation?: string | null;
  onSubmit: (input: ScheduleEntryInput) => Promise<void>;
  onCancel: () => void;
}

export default function ScheduleEntryForm({
  date,
  entry,
  defaultKind = "Activity",
  groupHeadcount,
  locations = [],
  mealLocation = null,
  onSubmit,
  onCancel,
}: Props) {
  const { t } = useTranslation();
  // Several of these forms can be open at once (one per day), so the suggestion list
  // needs an id of its own rather than a shared constant.
  const locationListId = useId();
  const [kind, setKind] = useState<ScheduleEntryKind>(entry?.kind ?? defaultKind);
  const [mealKind, setMealKind] = useState<MealKind>(entry?.mealKind ?? "Breakfast");
  // TimeOnly arrives as "08:00:00" but <input type="time"> wants "08:00".
  const [startTime, setStartTime] = useState(toTimeInput(entry?.startTime ?? "09:00:00"));
  const [endTime, setEndTime] = useState(toTimeInput(entry?.endTime ?? "10:00:00"));
  const [title, setTitle] = useState(entry?.title ?? "");
  const [menu, setMenu] = useState(entry?.menu ?? "");
  const [prepNotes, setPrepNotes] = useState(entry?.prepNotes ?? "");
  const [location, setLocation] = useState(
    entry?.location ?? (kind === "Meal" ? mealLocation ?? "" : ""),
  );
  const [participantCount, setParticipantCount] = useState(
    entry?.participantCount ?? groupHeadcount,
  );
  const [busy, setBusy] = useState(false);

  /** Switching to a meal fills in the canteen; switching away drops it again, so the
   *  place follows the kind unless the admin typed something of their own. */
  function changeKind(next: ScheduleEntryKind) {
    setKind(next);
    if (!mealLocation) return;
    if (next === "Meal" && location.trim() === "") setLocation(mealLocation);
    if (next === "Activity" && location === mealLocation) setLocation("");
  }

  async function handleSubmit(event: FormEvent) {
    event.preventDefault();
    setBusy(true);
    try {
      await onSubmit({
        kind,
        mealKind: kind === "Meal" ? mealKind : null,
        date,
        startTime: fromTimeInput(startTime),
        endTime: fromTimeInput(endTime),
        title: title.trim(),
        menu: kind === "Meal" && menu.trim() ? menu.trim() : null,
        prepNotes: prepNotes.trim() || null,
        location: location.trim() || null,
        participantCount,
      });
    } finally {
      setBusy(false);
    }
  }

  return (
    <form className="group-panel-form" onSubmit={handleSubmit}>
      <label>
        {t("schedule.form.kind")}
        <select value={kind} onChange={(e) => changeKind(e.target.value as ScheduleEntryKind)}>
          <option value="Activity">{t("schedule.kinds.Activity")}</option>
          <option value="Meal">{t("schedule.kinds.Meal")}</option>
        </select>
      </label>

      {kind === "Meal" && (
        <label>
          {t("schedule.form.mealKind")}
          <select value={mealKind} onChange={(e) => setMealKind(e.target.value as MealKind)}>
            {mealKinds.map((m) => (
              <option key={m} value={m}>
                {t(`schedule.mealKinds.${m}`)}
              </option>
            ))}
          </select>
        </label>
      )}

      <label>
        {t("schedule.form.participantCount")}
        <input
          type="number"
          min={1}
          value={participantCount}
          onChange={(e) => setParticipantCount(Math.max(1, Number(e.target.value) || 1))}
          required
        />
      </label>

      <label>
        {t("schedule.form.startTime")}
        <input
          type="time"
          value={startTime}
          onChange={(e) => setStartTime(e.target.value)}
          required
        />
      </label>
      <label>
        {t("schedule.form.endTime")}
        <input type="time" value={endTime} onChange={(e) => setEndTime(e.target.value)} required />
      </label>

      <label className="wide">
        {t("schedule.form.title")}
        <input value={title} onChange={(e) => setTitle(e.target.value)} required maxLength={200} />
      </label>

      {kind === "Meal" && (
        <label className="wide">
          {t("schedule.form.menu")}
          <textarea
            value={menu}
            onChange={(e) => setMenu(e.target.value)}
            rows={2}
            maxLength={2000}
            placeholder={t("schedule.form.menuPlaceholder")}
          />
        </label>
      )}

      <label className="wide">
        {t("schedule.form.prepNotes")}
        <textarea
          value={prepNotes}
          onChange={(e) => setPrepNotes(e.target.value)}
          rows={2}
          maxLength={2000}
          placeholder={t("schedule.form.prepNotesPlaceholder")}
        />
      </label>

      <label className="wide">
        {t("schedule.form.location")}
        <input
          value={location}
          onChange={(e) => setLocation(e.target.value)}
          maxLength={200}
          list={locations.length > 0 ? locationListId : undefined}
          placeholder={t("schedule.form.locationPlaceholder")}
        />
        {locations.length > 0 && (
          <datalist id={locationListId}>
            {locations.map((known) => (
              <option key={known} value={known} />
            ))}
          </datalist>
        )}
      </label>

      {participantCount > groupHeadcount && (
        <p className="wide" role="alert">
          {t("schedule.form.participantCountWarning", { count: groupHeadcount })}
        </p>
      )}

      <div className="wide row-actions">
        <button type="submit" disabled={busy}>
          {entry ? t("schedule.form.save") : t("schedule.form.add")}
        </button>
        <button type="button" onClick={onCancel}>
          {t("schedule.form.cancel")}
        </button>
      </div>
    </form>
  );
}
