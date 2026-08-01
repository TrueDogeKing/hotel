import { useEffect, useState, type FormEvent } from "react";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import AdminLayout from "../../components/admin/AdminLayout";
import ConfirmDialog from "../../components/ConfirmDialog";
import {
  createUser,
  deleteUser,
  getUsers,
  setUserPassword,
  setUserRole,
  userRoles,
  type AdminUser,
} from "../../api/admin";
import type { UserRole } from "../../api/jwt";
import { formatDate } from "../../utils/dates";
import { useAuth } from "../../auth/AuthContext";

interface FormState {
  login: string;
  password: string;
  role: UserRole;
}

const emptyForm: FormState = { login: "", password: "", role: "Worker" };

/**
 * Panel accounts. A worker reads the list like any other section; adding,
 * re-roling and deleting are administrator-only, and the API says so regardless
 * of what this page draws.
 *
 * Two rules the server enforces and this page explains rather than hides: an
 * account cannot delete itself, and the last administrator can be neither deleted
 * nor demoted. Both are shown as disabled controls with the reason on hover, so
 * the constraint is visible before it is hit.
 */
export default function UsersPage() {
  const { t, i18n } = useTranslation();
  // A worker may see who has an account; making, re-roling and deleting them are
  // writes, which the API allows only to an administrator.
  const { canEdit } = useAuth();
  const [users, setUsers] = useState<AdminUser[] | null>(null);
  const [form, setForm] = useState<FormState>(emptyForm);
  const [busyId, setBusyId] = useState<string | null>(null);
  const [deleteTarget, setDeleteTarget] = useState<AdminUser | null>(null);
  const [passwordTarget, setPasswordTarget] = useState<AdminUser | null>(null);
  const [passwordDraft, setPasswordDraft] = useState("");
  const [passwordError, setPasswordError] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [notice, setNotice] = useState<string | null>(null);

  useEffect(() => {
    let cancelled = false;
    void getUsers()
      .then((data) => {
        if (!cancelled) setUsers(data);
      })
      .catch(() => {
        if (!cancelled) setError(t("adminUsers.loadError"));
      });
    return () => {
      cancelled = true;
    };
  }, [t]);

  async function reload() {
    setUsers(await getUsers());
  }

  // The server owns the rules — a taken login, the last administrator — so show
  // what it said rather than second-guessing it.
  function handleApiError(err: unknown, fallback: string) {
    const detail = isAxiosError(err)
      ? (err.response?.data as { detail?: string } | undefined)?.detail
      : undefined;
    setError(detail ?? fallback);
  }

  async function handleCreate(event: FormEvent) {
    event.preventDefault();
    setError(null);
    setNotice(null);
    try {
      const created = await createUser({
        login: form.login.trim(),
        password: form.password,
        role: form.role,
      });
      setForm(emptyForm);
      setNotice(t("adminUsers.addedNotice", { login: created.login }));
      await reload();
    } catch (err) {
      handleApiError(err, t("adminUsers.createError"));
    }
  }

  async function handleRoleChange(user: AdminUser, role: UserRole) {
    setBusyId(user.id);
    setError(null);
    setNotice(null);
    try {
      await setUserRole(user.id, role);
      setNotice(t("adminUsers.roleChanged", { login: user.login, role: t(`roles.${role}`) }));
      await reload();
    } catch (err) {
      handleApiError(err, t("adminUsers.roleError"));
    } finally {
      setBusyId(null);
    }
  }

  useEffect(() => {
    if (!passwordTarget) return;
    function onKeyDown(event: KeyboardEvent) {
      if (event.key === "Escape" && busyId !== passwordTarget?.id) setPasswordTarget(null);
    }
    document.addEventListener("keydown", onKeyDown);
    return () => document.removeEventListener("keydown", onKeyDown);
  }, [passwordTarget, busyId]);

  function openPasswordDialog(user: AdminUser) {
    setPasswordTarget(user);
    setPasswordDraft("");
    setPasswordError(null);
  }

  async function handleSetPassword(event: FormEvent) {
    event.preventDefault();
    if (!passwordTarget) return;
    setBusyId(passwordTarget.id);
    setPasswordError(null);
    try {
      await setUserPassword(passwordTarget.id, passwordDraft);
      setNotice(t("adminUsers.passwordChanged", { login: passwordTarget.login }));
      setPasswordTarget(null);
      setPasswordDraft("");
      // The target's own sessions just ended; if it was this browser's account,
      // the next request finds that out and sends it back to the login page —
      // nothing to special-case here.
      await reload();
    } catch (err) {
      const detail = isAxiosError(err)
        ? (err.response?.data as { detail?: string } | undefined)?.detail
        : undefined;
      setPasswordError(detail ?? t("adminUsers.passwordError"));
    } finally {
      setBusyId(null);
    }
  }

  async function handleDelete(user: AdminUser) {
    setBusyId(user.id);
    setError(null);
    setNotice(null);
    try {
      await deleteUser(user.id);
      setDeleteTarget(null);
      setNotice(t("adminUsers.deleted", { login: user.login }));
      await reload();
    } catch (err) {
      handleApiError(err, t("adminUsers.deleteError"));
    } finally {
      setBusyId(null);
    }
  }

  const administrators = users?.filter((u) => u.role === "Administrator").length ?? 0;

  return (
    <AdminLayout>
      <h1>{t("adminUsers.title")}</h1>
      <p>{t("adminUsers.intro")}</p>

      {canEdit && (
      <form className="admin-form" onSubmit={(e) => void handleCreate(e)}>
        <label>
          {t("adminUsers.login")}
          <input
            value={form.login}
            onChange={(e) => setForm({ ...form, login: e.target.value })}
            maxLength={32}
            autoComplete="off"
            required
          />
        </label>
        <label>
          {t("adminUsers.password")}
          <input
            type="password"
            value={form.password}
            onChange={(e) => setForm({ ...form, password: e.target.value })}
            autoComplete="new-password"
            required
          />
        </label>
        <label>
          {t("adminUsers.role")}
          <select
            value={form.role}
            onChange={(e) => setForm({ ...form, role: e.target.value as UserRole })}
          >
            {userRoles.map((role) => (
              <option key={role} value={role}>
                {t(`roles.${role}`)}
              </option>
            ))}
          </select>
        </label>
        <button type="submit">{t("adminUsers.add")}</button>
      </form>
      )}
      {canEdit && <p className="hk-section-hint">{t("adminUsers.passwordHint")}</p>}

      {error && <p role="alert">{error}</p>}
      {notice && <p className="group-panel-notice">{notice}</p>}

      {!users ? (
        <p>{t("common.loading")}</p>
      ) : (
        <table className="admin-table">
          <thead>
            <tr>
              <th>{t("adminUsers.login")}</th>
              <th>{t("adminUsers.role")}</th>
              <th>{t("adminUsers.created")}</th>
              {canEdit && <th />}
            </tr>
          </thead>
          <tbody>
            {users.map((user) => {
              // Demoting or deleting the only administrator left would lock
              // everyone out of the accounts page, including this one.
              const isLastAdministrator = user.role === "Administrator" && administrators <= 1;
              const roleLocked = isLastAdministrator || busyId === user.id;
              const deleteLocked = user.isSelf || isLastAdministrator || busyId === user.id;
              const reason = user.isSelf
                ? t("adminUsers.cannotDeleteSelf")
                : isLastAdministrator
                  ? t("adminUsers.lastAdministrator")
                  : undefined;

              return (
                <tr key={user.id}>
                  <td>
                    {user.login}
                    {user.isSelf && (
                      <span className="hk-room-beds"> · {t("adminUsers.you")}</span>
                    )}
                  </td>
                  <td>
                    {canEdit ? (
                      <select
                        value={user.role}
                        disabled={roleLocked}
                        title={isLastAdministrator ? t("adminUsers.lastAdministrator") : undefined}
                        aria-label={t("adminUsers.role")}
                        onChange={(e) => void handleRoleChange(user, e.target.value as UserRole)}
                      >
                        {userRoles.map((role) => (
                          <option key={role} value={role}>
                            {t(`roles.${role}`)}
                          </option>
                        ))}
                      </select>
                    ) : (
                      t(`roles.${user.role}`)
                    )}
                  </td>
                  <td>{formatDate(user.createdAt.slice(0, 10), i18n.language)}</td>
                  {canEdit && (
                    <td className="row-actions">
                      <button
                        type="button"
                        disabled={busyId === user.id}
                        onClick={() => openPasswordDialog(user)}
                      >
                        {t("adminUsers.changePassword")}
                      </button>
                      <button
                        type="button"
                        disabled={deleteLocked}
                        title={reason}
                        onClick={() => setDeleteTarget(user)}
                      >
                        {t("adminUsers.delete")}
                      </button>
                    </td>
                  )}
                </tr>
              );
            })}
          </tbody>
        </table>
      )}

      {deleteTarget && (
        <ConfirmDialog
          title={t("adminUsers.deleteTitle")}
          message={t("adminUsers.deleteMessage", { login: deleteTarget.login })}
          confirmLabel={t("adminUsers.delete")}
          cancelLabel={t("adminUsers.cancel")}
          onConfirm={() => void handleDelete(deleteTarget)}
          onCancel={() => setDeleteTarget(null)}
        />
      )}

      {passwordTarget && (
        <div
          className="modal-overlay"
          role="presentation"
          onClick={() => busyId !== passwordTarget.id && setPasswordTarget(null)}
        >
          <form
            className="modal"
            role="dialog"
            aria-modal="true"
            aria-labelledby="password-title"
            onClick={(e) => e.stopPropagation()}
            onSubmit={(e) => void handleSetPassword(e)}
          >
            <h2 id="password-title">
              {t("adminUsers.changePasswordTitle", { login: passwordTarget.login })}
            </h2>
            <label>
              {t("adminUsers.password")}
              <input
                type="password"
                value={passwordDraft}
                onChange={(e) => setPasswordDraft(e.target.value)}
                autoComplete="new-password"
                autoFocus
                required
              />
            </label>
            <p className="hk-section-hint">{t("adminUsers.passwordHint")}</p>
            {passwordError && <p role="alert">{passwordError}</p>}
            <div className="modal-actions">
              <button
                type="button"
                className="secondary"
                disabled={busyId === passwordTarget.id}
                onClick={() => setPasswordTarget(null)}
              >
                {t("adminUsers.cancel")}
              </button>
              <button type="submit" disabled={busyId === passwordTarget.id}>
                {busyId === passwordTarget.id
                  ? t("adminUsers.saving")
                  : t("adminUsers.changePassword")}
              </button>
            </div>
          </form>
        </div>
      )}
    </AdminLayout>
  );
}
