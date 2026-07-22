---
source_file: "frontend/src/i18n/index.ts"
type: "code"
community: "Frontend App Shell & i18n"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_App_Shell__i18n
---

# index.ts

## Context

_Source: `frontend/src/i18n/index.ts` (defined near L1; showing L1–L33 of 33)._

```typescript
import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import pl from "./pl.json";
import en from "./en.json";

// Language choice persists per browser; Polish is the default for new visitors.
const STORAGE_KEY = "language";

export type Language = "pl" | "en";

export function getStoredLanguage(): Language {
  return localStorage.getItem(STORAGE_KEY) === "en" ? "en" : "pl";
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
- [[Language]] - `contains` [EXTRACTED]
- [[LanguageSwitcher.tsx]] - `imports_from` [EXTRACTED]
- [[getStoredLanguage()]] - `contains` [EXTRACTED]
- [[main.tsx]] - `imports_from` [EXTRACTED]
- [[public.ts]] - `imports_from` [EXTRACTED]
- [[setLanguage()]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_App_Shell__i18n