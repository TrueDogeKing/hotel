import { useEffect, useState, type FormEvent } from "react";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import AdminLayout from "../../components/admin/AdminLayout";
import {
  archiveSession,
  createSession,
  deleteSession,
  formatZl,
  getSessions,
  groszeToZl,
  publishSession,
  updateSession,
  zlToGrosze,
  type CampSession,
} from "../../api/admin";

interface SessionFormState {
  name: string;
  startDate: string;
  endDate: string;
  priceZl: string;
  depositZl: string;
}

const emptyForm: SessionFormState = {
  name: "",
  startDate: "",
  endDate: "",
  priceZl: "",
  depositZl: "",
};

export default function SessionsPage() {
  const { t, i18n } = useTranslation();
  const [sessions, setSessions] = useState<CampSession[]>([]);
  const [form, setForm] = useState<SessionFormState>(emptyForm);
  const [editing, setEditing] = useState<CampSession | null>(null);
  const [error, setError] = useState<string | null>(null);

  async function reload() {
    setSessions(await getSessions());
  }

  useEffect(() => {
    let cancelled = false;
    void getSessions().then((data) => {
      if (!cancelled) setSessions(data);
    });
    return () => {
      cancelled = true;
    };
  }, []);

  function handleApiError(err: unknown) {
    if (isAxiosError(err) && err.response?.status === 400) {
      // Business-rule violations (overlap, frozen dates) come back as ProblemDetails.
      const detail = (err.response.data as { detail?: string } | undefined)?.detail;
      setError(detail ?? t("adminSessions.genericError"));
    } else if (isAxiosError(err) && err.response?.status === 409) {
      setError(t("adminSessions.conflict"));
    } else {
      setError(t("adminSessions.genericError"));
    }
  }

  async function handleSubmit(event: FormEvent) {
    event.preventDefault();
    setError(null);
    const input = {
      name: form.name.trim(),
      startDate: form.startDate,
      endDate: form.endDate,
      pricePerPersonGrosze: zlToGrosze(form.priceZl),
      depositPerPersonGrosze: zlToGrosze(form.depositZl),
    };
    try {
      if (editing) {
        await updateSession(editing.id, { ...input, rowVersion: editing.rowVersion });
      } else {
        await createSession(input);
      }
      setForm(emptyForm);
      setEditing(null);
      await reload();
    } catch (err) {
      handleApiError(err);
    }
  }

  function startEdit(session: CampSession) {
    setEditing(session);
    setForm({
      name: session.name,
      startDate: session.startDate,
      endDate: session.endDate,
      priceZl: groszeToZl(session.pricePerPersonGrosze),
      depositZl: groszeToZl(session.depositPerPersonGrosze),
    });
  }

  async function run(action: () => Promise<unknown>) {
    setError(null);
    try {
      await action();
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
      <h1>{t("adminSessions.title")}</h1>

      <form className="admin-form" onSubmit={handleSubmit}>
        <label>
          {t("adminSessions.name")}
          <input
            value={form.name}
            onChange={(e) => setForm({ ...form, name: e.target.value })}
            required
            maxLength={128}
          />
        </label>
        <label>
          {t("adminSessions.startDate")}
          <input
            type="date"
            value={form.startDate}
            onChange={(e) => setForm({ ...form, startDate: e.target.value })}
            required
          />
        </label>
        <label>
          {t("adminSessions.endDate")}
          <input
            type="date"
            value={form.endDate}
            onChange={(e) => setForm({ ...form, endDate: e.target.value })}
            required
          />
        </label>
        <label>
          {t("adminSessions.pricePerPerson")}
          <input
            type="number"
            step="0.01"
            min="0.01"
            value={form.priceZl}
            onChange={(e) => setForm({ ...form, priceZl: e.target.value })}
            required
          />
        </label>
        <label>
          {t("adminSessions.depositPerPerson")}
          <input
            type="number"
            step="0.01"
            min="0.01"
            value={form.depositZl}
            onChange={(e) => setForm({ ...form, depositZl: e.target.value })}
            required
          />
        </label>
        <button type="submit">{editing ? t("adminSessions.save") : t("adminSessions.add")}</button>
        {editing && (
          <button
            type="button"
            onClick={() => {
              setEditing(null);
              setForm(emptyForm);
            }}
          >
            {t("adminSessions.cancelEdit")}
          </button>
        )}
      </form>

      {error && <p role="alert">{error}</p>}

      <table className="admin-table">
        <thead>
          <tr>
            <th>{t("adminSessions.name")}</th>
            <th>{t("adminSessions.dates")}</th>
            <th>{t("adminSessions.pricePerPerson")}</th>
            <th>{t("adminSessions.depositPerPerson")}</th>
            <th>{t("adminSessions.status")}</th>
            <th />
          </tr>
        </thead>
        <tbody>
          {sessions.map((session) => (
            <tr key={session.id}>
              <td>
                <Link to={`/admin/turnusy/${session.id}`}>{session.name}</Link>
              </td>
              <td>
                {formatDate(session.startDate)} – {formatDate(session.endDate)}
              </td>
              <td>{formatZl(session.pricePerPersonGrosze)}</td>
              <td>{formatZl(session.depositPerPersonGrosze)}</td>
              <td>{t(`adminSessions.statuses.${session.status}`)}</td>
              <td className="row-actions">
                <button type="button" onClick={() => startEdit(session)}>
                  {t("adminSessions.edit")}
                </button>
                {session.status !== "Published" && (
                  <button type="button" onClick={() => void run(() => publishSession(session.id))}>
                    {t("adminSessions.publish")}
                  </button>
                )}
                {session.status !== "Archived" && (
                  <button type="button" onClick={() => void run(() => archiveSession(session.id))}>
                    {t("adminSessions.archive")}
                  </button>
                )}
                <button type="button" onClick={() => void run(() => deleteSession(session.id))}>
                  {t("adminSessions.delete")}
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </AdminLayout>
  );
}
