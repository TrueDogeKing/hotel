// Light (Lakeside warm sand) is the product default; dark (charcoal) is an
// opt-in stored per browser. The initial theme is applied by a blocking snippet
// in index.html (no flash); this module owns runtime toggling.

export type Theme = "dark" | "light";

const STORAGE_KEY = "theme";

// Two toggle surfaces exist (the rail button and Settings > Appearance); both read through
// this subscription so neither shows a stale icon after the other one flips the theme.
type Listener = () => void;
const listeners = new Set<Listener>();

export function getTheme(): Theme {
  const current = document.documentElement.dataset.theme;
  if (current === "light" || current === "dark") return current;
  return localStorage.getItem(STORAGE_KEY) === "dark" ? "dark" : "light";
}

export function applyTheme(theme: Theme): void {
  document.documentElement.dataset.theme = theme;
}

export function toggleTheme(): Theme {
  const next: Theme = getTheme() === "dark" ? "light" : "dark";
  localStorage.setItem(STORAGE_KEY, next);
  applyTheme(next);
  listeners.forEach((listener) => listener());
  return next;
}

// Subscribe to theme changes (used with useSyncExternalStore). Returns an unsubscribe function.
export function subscribeTheme(listener: Listener): () => void {
  listeners.add(listener);
  return () => listeners.delete(listener);
}
