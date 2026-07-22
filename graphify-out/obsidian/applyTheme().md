---
source_file: "frontend/src/theme.ts"
type: "code"
community: "Frontend Theme Toggle"
location: "L20"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Theme_Toggle
---

# applyTheme()

## Context

_Source: `frontend/src/theme.ts` (defined near L20; showing L18–L36 of 36)._

```typescript
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
- [[toggleTheme()]] - `calls` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Theme_Toggle