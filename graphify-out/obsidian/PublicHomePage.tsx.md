---
source_file: "frontend/src/pages/PublicHomePage.tsx"
type: "code"
community: "Frontend App Shell & i18n"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_App_Shell__i18n
---

# PublicHomePage.tsx

## Context

_Source: `frontend/src/pages/PublicHomePage.tsx` (defined near L1; showing L1–L46 of 51)._

```tsx
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import LanguageSwitcher from "../components/LanguageSwitcher";

// Public landing page: hero, offer highlights and contact details.
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

      <section className="offer">
        <h2>{t("home.offerTitle")}</h2>
        <div className="offer-cards">
          {(["rooms", "meals", "grounds"] as const).map((key) => (
            <div key={key} className="offer-card">
              <h3>{t(`home.offer.${key}.title`)}</h3>
              <p>{t(`home.offer.${key}.body`)}</p>
            </div>
          ))}
        </div>
      </section>

      <section className="contact">
        <h2>{t("home.contactTitle")}</h2>
        <p>{t("home.contactAddress")}</p>
        <p>
          {t("home.contactPhone")} · <a href="mailto:rezerwacje@campcenter.pl">rezerwacje@campcenter.pl</a>
        </p>
      </section>

      <footer className="public-footer">
```

## Connections
- [[App.tsx]] - `imports_from` [EXTRACTED]
- [[LanguageSwitcher()]] - `imports` [EXTRACTED]
- [[LanguageSwitcher.tsx]] - `imports_from` [EXTRACTED]
- [[PublicHomePage()]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_App_Shell__i18n