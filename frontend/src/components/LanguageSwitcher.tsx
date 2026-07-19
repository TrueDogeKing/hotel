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
