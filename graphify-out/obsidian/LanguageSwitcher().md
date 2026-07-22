---
source_file: "frontend/src/components/LanguageSwitcher.tsx"
type: "code"
community: "Frontend App Shell & i18n"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_App_Shell__i18n
---

# LanguageSwitcher()

## Context

_Source: `frontend/src/components/LanguageSwitcher.tsx` (defined near L5; showing L3–L31 of 31)._

```tsx

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
- [[AdminLayout.tsx]] - `imports` [EXTRACTED]
- [[BookingManagePage.tsx]] - `imports` [EXTRACTED]
- [[BookingWizardPage.tsx]] - `imports` [EXTRACTED]
- [[LanguageSwitcher.tsx]] - `contains` [EXTRACTED]
- [[LoginPage.tsx]] - `imports` [EXTRACTED]
- [[PaymentReturnPage.tsx]] - `imports` [EXTRACTED]
- [[PublicHomePage.tsx]] - `imports` [EXTRACTED]
- [[getStoredLanguage()]] - `calls` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_App_Shell__i18n