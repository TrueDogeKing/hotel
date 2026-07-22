---
source_file: "frontend/src/components/LanguageSwitcher.tsx"
type: "code"
community: "Frontend App Shell & i18n"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_App_Shell__i18n
---

# LanguageSwitcher.tsx

## Context

_Source: `frontend/src/components/LanguageSwitcher.tsx` (defined near L1; showing L1–L31 of 31)._

```tsx
import { useTranslation } from "react-i18next";
import { getStoredLanguage, setLanguage, type Language } from "../i18n";

// Two flag buttons (top corner of every layout): click a flag to switch the UI language.
export default function LanguageSwitcher() {
  // Subscribes this component to language changes so the active flag updates.
  useTranslation();
  const current = getStoredLanguage();

  function flagButton(language: Language, flag: string, label: string) {
    return (
      <button
        type="button"
        className={`lang-flag${current === language ? " active" : ""}`}
        onClick={() => setLanguage(language)}
        aria-label={label}
        aria-pressed={current === language}
        title={label}
      >
        {flag}
      </button>
    );
  }

  return (
    <div className="lang-switcher">
      {flagButton("pl", "🇵🇱", "Polski")}
      {flagButton("en", "🇬🇧", "English")}
    </div>
  );
}
```

## Connections
- [[AdminLayout.tsx]] - `imports_from` [EXTRACTED]
- [[BookingManagePage.tsx]] - `imports_from` [EXTRACTED]
- [[BookingWizardPage.tsx]] - `imports_from` [EXTRACTED]
- [[Language]] - `imports` [EXTRACTED]
- [[LanguageSwitcher()]] - `contains` [EXTRACTED]
- [[LoginPage.tsx]] - `imports_from` [EXTRACTED]
- [[PaymentReturnPage.tsx]] - `imports_from` [EXTRACTED]
- [[PublicHomePage.tsx]] - `imports_from` [EXTRACTED]
- [[getStoredLanguage()]] - `imports` [EXTRACTED]
- [[index.ts]] - `imports_from` [EXTRACTED]
- [[setLanguage()]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_App_Shell__i18n