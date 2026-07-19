import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import LanguageSwitcher from "../components/LanguageSwitcher";

// Public landing page. Marketing content (offer, gallery, contact) lands here in phase 3;
// for now it carries the hero and navigation so routing and i18n are in place.
export default function PublicHomePage() {
  const { t } = useTranslation();

  return (
    <main className="public-page">
      <header className="public-header">
        <div className="auth-brand">
          <span className="mark">C</span> {t("common.appName")}
        </div>
        <LanguageSwitcher />
      </header>

      <section className="hero">
        <h1>{t("home.heroTitle")}</h1>
        <p>{t("home.heroSubtitle")}</p>
        <Link className="cta" to="/rezerwacja">
          {t("home.bookCta")}
        </Link>
      </section>

      <footer className="public-footer">
        <Link to="/admin">{t("home.adminLink")}</Link>
      </footer>
    </main>
  );
}
