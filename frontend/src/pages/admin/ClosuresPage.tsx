import { useEffect, useState, type FormEvent } from "react";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import AdminLayout from "../../components/admin/AdminLayout";
import {
  createClosure,
  deleteClosure,
  getClosures,
  getRooms,
  updateClosure,
  type Closure,
  type Room,
} from "../../api/admin";

interface ClosureFormState {
  reason: string;
  startDate: string;
  endDate: string;
  roomId: string; // "" = whole center
}

const emptyForm: ClosureFormState = {
  reason: "",
  startDate: "",
  endDate: "",
  roomId: "",
};

export default function ClosuresPage() {
  const { t, i18n } = useTranslation();
  const [closures, setClosures] = useState<Closure[]>([]);
  const [rooms, setRooms] = useState<Room[]>([]);
  const [form, setForm] = useState<ClosureFormState>(emptyForm);
  const [editing, setEditing] = useState<Closure | null>(null);
  const [error, setError] = useState<string | null>(null);

  async function reload() {
    setClosures(await getClosures());
  }

  useEffect(() => {
    let cancelled = false;
    void Promise.all([getClosures(), getRooms()]).then(([c, r]) => {
      if (!cancelled) {
        setClosures(c);
        setRooms(r);
      }
    });
    return () => {
      cancelled = true;
    };
  }, []);

  function handleApiError(err: unknown) {
    if (isAxiosError(err) && (err.response?.status === 400 || err.response?.status === 409)) {
      const detail = (err.response.data as { detail?: string } | undefined)?.detail;
      setError(detail ?? t("adminClosures.genericError"));
    } else {
      setError(t("adminClosures.genericError"));
    }
  }

  async function handleSubmit(event: FormEvent) {
    event.preventDefault();
    setError(null);
    const input = {
      reason: form.reason.trim(),
      startDate: form.startDate,
      endDate: form.endDate,
      roomId: form.roomId || null,
    };
    try {
      if (editing) {
        await updateClosure(editing.id, { ...input, rowVersion: editing.rowVersion });
      } else {
        await createClosure(input);
      }
      setForm(emptyForm);
      setEditing(null);
      await reload();
    } catch (err) {
      handleApiError(err);
    }
  }

  function startEdit(closure: Closure) {
    setEditing(closure);
    setForm({
      reason: closure.reason,
      startDate: closure.startDate,
      endDate: closure.endDate,
      roomId: closure.roomId ?? "",
    });
  }

  async function remove(id: string) {
    setError(null);
    try {
      await deleteClosure(id);
      await reload();
    } catch (err) {
      handleApiError(err);
    }
  }

  const dateFormatter = new Intl.DateTimeFormat(i18n.language === "en" ? "en-GB" : "pl-PL", {
    dateStyle: "medium",
  });
  const formatDate = (iso: string) => dateFormatter.format(new Date(iso));

  return (
    <AdminLayout>
      <h1>{t("adminClosures.title")}</h1>
      <p>{t("adminClosures.intro")}</p>

      <form className="admin-form" onSubmit={handleSubmit}>
        <label>
          {t("adminClosures.reason")}
          <input
            value={form.reason}
            onChange={(e) => setForm({ ...form, reason: e.target.value })}
            required
            maxLength={256}
          />
        </label>
        <label>
          {t("adminClosures.startDate")}
          <input
            type="date"
            value={form.startDate}
            onChange={(e) => setForm({ ...form, startDate: e.target.value })}
            required
          />
        </label>
        <label>
          {t("adminClosures.endDate")}
          <input
            type="date"
            value={form.endDate}
            onChange={(e) => setForm({ ...form, endDate: e.target.value })}
            required
          />
        </label>
        <label>
          {t("adminClosures.scope")}
          <select value={form.roomId} onChange={(e) => setForm({ ...form, roomId: e.target.value })}>
            <option value="">{t("adminClosures.wholeCenter")}</option>
            {rooms.map((r) => (
              <option key={r.id} value={r.id}>
                {t("adminClosures.roomOption", { number: r.number })}
              </option>
            ))}
          </select>
        </label>
        <button type="submit">
          {editing ? t("adminClosures.save") : t("adminClosures.add")}
        </button>
        {editing && (
          <button
            type="button"
            onClick={() => {
              setEditing(null);
              setForm(emptyForm);
            }}
          >
            {t("adminClosures.cancelEdit")}
          </button>
        )}
      </form>

      {error && <p role="alert">{error}</p>}

      <table className="admin-table">
        <thead>
          <tr>
            <th>{t("adminClosures.reason")}</th>
            <th>{t("adminClosures.dates")}</th>
            <th>{t("adminClosures.scope")}</th>
            <th />
          </tr>
        </thead>
        <tbody>
          {closures.map((closure) => (
            <tr key={closure.id}>
              <td>{closure.reason}</td>
              <td>
                {formatDate(closure.startDate)} – {formatDate(closure.endDate)}
              </td>
              <td>
                {closure.roomId
                  ? t("adminClosures.roomScope", { number: closure.roomNumber })
                  : t("adminClosures.wholeCenter")}
              </td>
              <td className="row-actions">
                <button type="button" onClick={() => startEdit(closure)}>
                  {t("adminClosures.edit")}
                </button>
                <button type="button" onClick={() => void remove(closure.id)}>
                  {t("adminClosures.delete")}
                </button>
              </td>
            </tr>
          ))}
          {closures.length === 0 && (
            <tr>
              <td colSpan={4}>{t("adminClosures.empty")}</td>
            </tr>
          )}
        </tbody>
      </table>
    </AdminLayout>
  );
}
