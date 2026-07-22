import { useSyncExternalStore } from "react";
import { getTheme, subscribeTheme, toggleTheme } from "../theme";
import { IconMoon, IconSun } from "./icons";

// Light/dark switch. Reads through the shared theme store so every mounted
// toggle (public header, admin header) stays in sync when one flips it.
export default function ThemeToggle() {
  const theme = useSyncExternalStore(subscribeTheme, getTheme, () => "light" as const);
  const isDark = theme === "dark";

  return (
    <button
      type="button"
      className="theme-toggle-btn"
      onClick={() => toggleTheme()}
      aria-label={isDark ? "Switch to light theme" : "Switch to dark theme"}
      title={isDark ? "Light theme" : "Dark theme"}
    >
      {isDark ? <IconSun /> : <IconMoon />}
    </button>
  );
}
