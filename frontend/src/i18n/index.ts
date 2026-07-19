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
