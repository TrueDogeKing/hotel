import { useEffect, useState, type FormEvent } from "react";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import AdminLayout from "../../components/admin/AdminLayout";
import { useAuth } from "../../auth/AuthContext";
import {
  createMealTime,
  deleteMealTime,
  getMealTimes,
  mealKinds,
  updateMealTime,
  type MealKind,
  type MealTimeDefault,
} from "../../api/admin";
import { fromTimeInput, toTimeInput } from "../../utils/dates";

interface FormState {
  mealKind: MealKind;
  label: string;
  startTime: string;
  endTime: string;
  durationMinutes: string;
  sortOrder: string;
}

const emptyForm: FormState = {
  mealKind: "Breakfast",
  label: "",
  startTime: "08:00",
  endTime: "09:00",
  durationMinutes: "30",
  sortOrder: "1",
};

// The center's default meal slots. Confirmed bookings get one meal per active
// slot per day of their stay; editing a slot affects future generation only.
export default function MealTimesPage() {
  const { t } = useTranslation();
  // Centre-wide settings: a worker reads them, an administrator changes them.
  const { canEdit } = useAuth();
  const [mealTimes, setMealTimes] = useState<MealTimeDefault[]>([]);
  const [form, setForm] = useState<FormState>(emptyForm);
  const [editing, setEditing] = useState<MealTimeDefault | null>(null);
  const [error, setError] = useState<string | null>(null);

  async function reload() {
    setMealTimes(await getMealTimes());
  }

  useEffect(() => {
    let cancelled = false;
    void getMealTimes()
      .then((data) => {
        if (!cancelled) setMealTimes(data);
      })
      .catch(() => {
        if (!cancelled) setError(t("mealTimes.genericError"));
      });
    return () => {
      cancelled = true;
    };
  }, [t]);

  function handleApiError(err: unknown) {
    if (isAxiosError(err) && err.response) {
      const detail = (err.response.data as { detail?: string } | undefined)?.detail;
      setError(detail ?? t("mealTimes.genericError"));
    } else {
      setError(t("mealTimes.genericError"));
    }
  }

  async function handleSubmit(event: FormEvent) {
    event.preventDefault();
    setError(null);
    const input = {
      mealKind: form.mealKind,
      label: form.label.trim(),
      startTime: fromTimeInput(form.startTime),
      endTime: fromTimeInput(form.endTime),
      durationMinutes: Number(form.durationMinutes) || 0,
      sortOrder: Number(form.sortOrder) || 0,
    };
    try {
      if (editing) {
        await updateMealTime(editing.id, {
          ...input,
          isActive: editing.isActive,
          rowVersion: editing.rowVersion,
        });
      } else {
        await createMealTime(input);
      }
      setForm(emptyForm);
      setEditing(null);
      await reload();
    } catch (err) {
      handleApiError(err);
    }
  }

  function startEdit(mealTime: MealTimeDefault) {
    setEditing(mealTime);
    setForm({
      mealKind: mealTime.mealKind,
      label: mealTime.label,
      startTime: toTimeInput(mealTime.startTime),
      endTime: toTimeInput(mealTime.endTime),
      durationMinutes: String(mealTime.durationMinutes),
      sortOrder: String(mealTime.sortOrder),
    });
  }

  async function toggleActive(mealTime: MealTimeDefault) {
    setError(null);
    try {
      await updateMealTime(mealTime.id, {
        mealKind: mealTime.mealKind,
        label: mealTime.label,
        startTime: mealTime.startTime,
        endTime: mealTime.endTime,
        durationMinutes: mealTime.durationMinutes,
        sortOrder: mealTime.sortOrder,
        isActive: !mealTime.isActive,
        rowVersion: mealTime.rowVersion,
      });
      await reload();
    } catch (err) {
      handleApiError(err);
    }
  }

  async function remove(id: string) {
    setError(null);
    try {
      await deleteMealTime(id);
      await reload();
    } catch (err) {
      handleApiError(err);
    }
  }

  return (
    <AdminLayout>
      <h1>{t("mealTimes.title")}</h1>
      <p>{t("mealTimes.intro")}</p>
      <p>
        <Link to="/admin/harmonogram">{t("mealTimes.backToSchedule")}</Link>
      </p>

      {canEdit && (
        <form className="admin-form" onSubmit={handleSubmit}>
          <label>
            {t("mealTimes.mealKind")}
            <select
              value={form.mealKind}
              onChange={(e) => setForm({ ...form, mealKind: e.target.value as MealKind })}
            >
              {mealKinds.map((m) => (
                <option key={m} value={m}>
                  {t(`schedule.mealKinds.${m}`)}
                </option>
              ))}
            </select>
          </label>
          <label>
            {t("mealTimes.label")}
            <input
              value={form.label}
              onChange={(e) => setForm({ ...form, label: e.target.value })}
              required
              maxLength={128}
            />
          </label>
          <label>
            {t("mealTimes.startTime")}
            <input
              type="time"
              value={form.startTime}
              onChange={(e) => setForm({ ...form, startTime: e.target.value })}
              required
            />
          </label>
          <label>
            {t("mealTimes.endTime")}
            <input
              type="time"
              value={form.endTime}
              onChange={(e) => setForm({ ...form, endTime: e.target.value })}
              required
            />
          </label>
          <label title={t("mealTimes.durationHint")}>
            {t("mealTimes.duration")}
            <input
              type="number"
              min={5}
              max={480}
              step={5}
              value={form.durationMinutes}
              onChange={(e) => setForm({ ...form, durationMinutes: e.target.value })}
              required
            />
          </label>
          <label>
            {t("mealTimes.sortOrder")}
            <input
              type="number"
              min={0}
              value={form.sortOrder}
              onChange={(e) => setForm({ ...form, sortOrder: e.target.value })}
            />
          </label>
          <button type="submit">{editing ? t("mealTimes.save") : t("mealTimes.add")}</button>
          {editing && (
            <button
              type="button"
              onClick={() => {
                setEditing(null);
                setForm(emptyForm);
              }}
            >
              {t("mealTimes.cancelEdit")}
            </button>
          )}
        </form>
      )}

      {error && <p role="alert">{error}</p>}

      <table className="admin-table">
        <thead>
          <tr>
            <th>{t("mealTimes.label")}</th>
            <th>{t("mealTimes.mealKind")}</th>
            <th>{t("mealTimes.time")}</th>
            <th>{t("mealTimes.duration")}</th>
            <th>{t("mealTimes.status")}</th>
            {canEdit && <th />}
          </tr>
        </thead>
        <tbody>
          {mealTimes.map((mealTime) => (
            <tr key={mealTime.id}>
              <td>{mealTime.label}</td>
              <td>{t(`schedule.mealKinds.${mealTime.mealKind}`)}</td>
              <td>
                {toTimeInput(mealTime.startTime)}–{toTimeInput(mealTime.endTime)}
              </td>
              <td>{t("mealTimes.minutes", { count: mealTime.durationMinutes })}</td>
              <td>{mealTime.isActive ? t("mealTimes.active") : t("mealTimes.inactive")}</td>
              {canEdit && (
                <td className="row-actions">
                  <button type="button" onClick={() => startEdit(mealTime)}>
                    {t("mealTimes.edit")}
                  </button>
                  <button type="button" onClick={() => void toggleActive(mealTime)}>
                    {mealTime.isActive ? t("mealTimes.deactivate") : t("mealTimes.activate")}
                  </button>
                  <button type="button" onClick={() => void remove(mealTime.id)}>
                    {t("mealTimes.delete")}
                  </button>
                </td>
              )}
            </tr>
          ))}
          {mealTimes.length === 0 && (
            <tr>
              <td colSpan={5}>{t("mealTimes.empty")}</td>
            </tr>
          )}
        </tbody>
      </table>
    </AdminLayout>
  );
}
