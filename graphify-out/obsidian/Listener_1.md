---
source_file: "frontend/src/theme.ts"
type: "code"
community: "Frontend Theme Toggle"
location: "L11"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Theme_Toggle
---

# Listener

## Context

_Source: `frontend/src/theme.ts` (defined near L11; showing L9–L36 of 36)._

```typescript
// Two toggle surfaces exist (the rail button and Settings > Appearance); both read through
// this subscription so neither shows a stale icon after the other one flips the theme.
type Listener = () => void;
const listeners = new Set<Listener>();

export function getTheme(): Theme {
  const current = document.documentElement.dataset.theme;
  if (current === "light" || current === "dark") return current;
  return localStorage.getItem(STORAGE_KEY) === "light" ? "light" : "dark";
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
```

## Connections
- [[theme.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Theme_Toggle