import type { ReactNode } from "react";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import LanguageSwitcher from "../components/LanguageSwitcher";
import ThemeToggle from "../components/ThemeToggle";
import {
  IconArrowRight,
  IconBed,
  IconLandscape,
  IconMail,
  IconMap,
  IconMapPin,
  IconPhone,
  IconUtensils,
  IconWaves,
} from "../components/icons";

// Public landing page — Lakeside look: sticky nav, hero, feature cards,
// how-it-works, contact, footer. Structure mirrors the stitch mockup.
const OFFER: { key: string; icon: ReactNode }[] = [
  { key: "rooms", icon: <IconBed /> },
  { key: "meals", icon: <IconUtensils /> },
  { key: "grounds", icon: <IconWaves /> },
];

const STEPS = ["step1", "step2", "step3"] as const;

export default function PublicHomePage() {
  const { t } = useTranslation();

  return (
    <div className="home">
      <header className="home-header">
        <a className="home-brand" href="#top">
          <IconLandscape />
          {t("common.appName")}
        </a>
        <nav className="home-nav">
          <a href="#offer">{t("home.navOffer")}</a>
          <a href="#how">{t("home.navHow")}</a>
          <a href="#contact">{t("home.navContact")}</a>
        </nav>
        <div className="home-header-actions">
          <ThemeToggle />
          <LanguageSwitcher />
          <Link className="home-book-btn" to="/rezerwacja">
            {t("home.bookNow")}
          </Link>
        </div>
      </header>

      <main id="top">
        <section className="home-hero">
          <div className="home-hero-overlay" />
          <div className="home-hero-content">
            <h1>{t("home.heroTitle")}</h1>
            <p>{t("home.heroSubtitle")}</p>
            <Link className="cta-amber" to="/rezerwacja">
              {t("home.bookCta")}
              <IconArrowRight />
            </Link>
          </div>
        </section>

        <section id="offer" className="home-features">
          <div className="home-section-head">
            <h2>{t("home.offerTitle")}</h2>
            <p>{t("home.offerSubtitle")}</p>
          </div>
          <div className="feature-cards">
            {OFFER.map(({ key, icon }) => (
              <article key={key} className="feature-card">
                <span className="feature-icon">{icon}</span>
                <h3>{t(`home.offer.${key}.title`)}</h3>
                <p>{t(`home.offer.${key}.body`)}</p>
              </article>
            ))}
          </div>
        </section>

        <section id="how" className="home-how">
          <h2>{t("home.how.title")}</h2>
          <ol className="how-steps">
            {STEPS.map((step, i) => (
              <li key={step} className="how-step">
                <span className="how-step-num">{i + 1}</span>
                <h4>{t(`home.how.${step}.title`)}</h4>
                <p>{t(`home.how.${step}.body`)}</p>
              </li>
            ))}
          </ol>
        </section>

        <section id="contact" className="home-contact">
          <div className="home-contact-text">
            <h2>{t("home.contactTitle")}</h2>
            <p className="home-contact-lead">{t("home.contactLead")}</p>
            <ul className="contact-list">
              <li>
                <IconMapPin />
                {t("home.contactAddress")}
              </li>
              <li>
                <IconPhone />
                {t("home.contactPhone")}
              </li>
              <li>
                <IconMail />
                <a href={`mailto:${t("home.contactEmail")}`}>{t("home.contactEmail")}</a>
              </li>
            </ul>
          </div>
          <div className="home-contact-map" aria-hidden="true">
            <IconMap />
          </div>
        </section>
      </main>

      <footer className="home-footer">
        <span className="home-footer-brand">{t("common.appName")}</span>
        <Link to="/admin">{t("home.adminLink")}</Link>
        <span className="home-footer-rights">{t("home.rights")}</span>
      </footer>
    </div>
  );
}
