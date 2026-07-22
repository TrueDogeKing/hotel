import { useTranslation } from "react-i18next";
import { getStoredLanguage, setLanguage, type Language } from "../i18n";

// Inline SVG flags — Windows does not render emoji regional-indicator flags
// (they fall back to the letters "PL"/"GB"), so we draw them instead.
function FlagPL() {
  return (
    <svg viewBox="0 0 24 16" aria-hidden="true" focusable="false">
      <rect width="24" height="16" fill="#fff" />
      <rect y="8" width="24" height="8" fill="#dc143c" />
    </svg>
  );
}

function FlagGB() {
  return (
    <svg viewBox="0 0 60 30" aria-hidden="true" focusable="false">
      <clipPath id="flag-gb-clip">
        <rect width="60" height="30" />
      </clipPath>
      <clipPath id="flag-gb-diag">
        <path d="M30,15 h30 v15 z v15 h-30 z h-30 v-15 z v-15 h30 z" />
      </clipPath>
      <g clipPath="url(#flag-gb-clip)">
        <rect width="60" height="30" fill="#012169" />
        <path d="M0,0 L60,30 M60,0 L0,30" stroke="#fff" strokeWidth="6" />
        <path
          d="M0,0 L60,30 M60,0 L0,30"
          clipPath="url(#flag-gb-diag)"
          stroke="#c8102e"
          strokeWidth="4"
        />
        <path d="M30,0 v30 M0,15 h60" stroke="#fff" strokeWidth="10" />
        <path d="M30,0 v30 M0,15 h60" stroke="#c8102e" strokeWidth="6" />
      </g>
    </svg>
  );
}

// Two flag buttons (top corner of every layout): click a flag to switch the UI language.
export default function LanguageSwitcher() {
  // Subscribes this component to language changes so the active flag updates.
  useTranslation();
  const current = getStoredLanguage();

  function flagButton(language: Language, flag: React.ReactNode, label: string) {
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
      {flagButton("pl", <FlagPL />, "Polski")}
      {flagButton("en", <FlagGB />, "English")}
    </div>
  );
}
