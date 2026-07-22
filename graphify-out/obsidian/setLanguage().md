---
source_file: "frontend/src/i18n/index.ts"
type: "code"
community: "Frontend App Shell & i18n"
location: "L15"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_App_Shell__i18n
---

# setLanguage()

## Context

_Source: `frontend/src/i18n/index.ts` (defined near L15; showing L13–L33 of 33)._

```typescript
}

export function setLanguage(language: Language): void {
  localStorage.setItem(STORAGE_KEY, language);
  void i18n.changeLanguage(language);
  document.documentElement.lang = language;
}

void i18n.use(initReactI18next).init({
  resources: {
    pl: { translation: pl },
    en: { translation: en },
  },
  lng: getStoredLanguage(),
  fallbackLng: "pl",
  interpolation: { escapeValue: false },
});

document.documentElement.lang = getStoredLanguage();

export default i18n;
```

## Connections
- [[LanguageSwitcher.tsx]] - `imports` [EXTRACTED]
- [[index.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_App_Shell__i18n