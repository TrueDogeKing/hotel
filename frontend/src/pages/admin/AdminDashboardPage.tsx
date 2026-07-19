import { useTranslation } from "react-i18next";
import { useAuth } from "../../auth/AuthContext";
import LanguageSwitcher from "../../components/LanguageSwitcher";

// Admin panel shell. Dashboard aggregates (occupancy, pending deposits, tasks)
// arrive in phase 4; phase 1 proves auth + layout + i18n.
export default function AdminDashboardPage() {
  const { t } = useTranslation();
  const { userLogin, logout } = useAuth();

  return (
    <main className="admin-page">
      <header className="public-header">
        <div className="auth-brand">
          <span className="mark">C</span> {t("admin.dashboardTitle")}
        </div>
        <div className="admin-header-actions">
          <LanguageSwitcher />
          <button type="button" onClick={() => void logout()}>
            {t("admin.logout")}
          </button>
        </div>
      </header>

      <section className="admin-content">
        <p>{t("admin.welcome", { login: userLogin ?? "" })}</p>
      </section>
    </main>
  );
}
