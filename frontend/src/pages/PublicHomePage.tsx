import type { ReactNode } from "react";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import PublicHeader from "../components/PublicHeader";
import PublicFooter from "../components/PublicFooter";
import {
  IconArrowRight,
  IconBed,
  IconMail,
  IconMapPin,
  IconPhone,
  IconUtensils,
  IconWaves,
} from "../components/icons";

// The keyless Google Maps embed endpoint: it takes a place query directly, no
// API key or billing account needed (unlike the JS/Embed API proper).
const MAP_QUERY = encodeURIComponent("Gdańska 47A, 82-110 Sztutowo");
const MAP_EMBED_SRC = `https://maps.google.com/maps?q=${MAP_QUERY}&z=15&output=embed`;

// Public landing page: sticky nav, hero, feature cards, how-it-works, contact,
// footer. Structure mirrors the stitch mockup.
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
      <PublicHeader />

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
          <div className="home-contact-map">
            <iframe
              title={t("home.contactAddress")}
              src={MAP_EMBED_SRC}
              loading="lazy"
              referrerPolicy="no-referrer-when-downgrade"
            />
          </div>
        </section>
      </main>

      <PublicFooter />
    </div>
  );
}
