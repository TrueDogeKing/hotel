import { type ReactNode } from "react";
import { Link, NavLink } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { useAuth } from "../../auth/AuthContext";
import LanguageSwitcher from "../LanguageSwitcher";
import ThemeToggle from "../ThemeToggle";
import { IconSunSea } from "../icons";

// Shared admin shell: brand, section nav, language flags, logout.
export default function AdminLayout({ children }: { children: ReactNode }) {
  const { t } = useTranslation();
  const { logout } = useAuth();

  return (
    <main className="admin-page">
      <header className="public-header">
        <div className="admin-header-left">
          {/* Sections are chosen from the dashboard's tiles, not from here — so the
              brand is the way back to them from wherever you are. */}
          <Link to="/admin" className="auth-brand admin-brand">
            <span className="mark">
              <IconSunSea />
            </span>
            <span className="admin-brand-text">{t("admin.dashboardTitle")}</span>
          </Link>
        </div>
        <div className="admin-header-actions">
          <ThemeToggle />
          <LanguageSwitcher />
          <button type="button" onClick={() => void logout()}>
            {t("admin.logout")}
          </button>
        </div>
      </header>

      <section className="admin-content">{children}</section>

      <footer className="home-footer">
        <span className="home-footer-brand">{t("common.appName")}</span>
        <nav className="admin-footer-nav">
          <NavLink to="/admin" end>
            {t("admin.nav.dashboard")}
          </NavLink>
          <NavLink to="/admin/harmonogram">{t("admin.nav.schedule")}</NavLink>
          <NavLink to="/admin/sprzatanie">{t("admin.nav.housekeeping")}</NavLink>
          <NavLink to="/admin/oblozenie">{t("admin.nav.occupancy")}</NavLink>
          <NavLink to="/admin/rezerwacje">{t("admin.nav.bookings")}</NavLink>
          <NavLink to="/admin/zadania">{t("admin.nav.tasks")}</NavLink>
          <Link to="/">{t("admin.viewSite")}</Link>
        </nav>
        <span className="home-footer-rights">{t("home.rights")}</span>
      </footer>
    </main>
  );
}
