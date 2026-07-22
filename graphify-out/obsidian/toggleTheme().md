---
source_file: "frontend/src/theme.ts"
type: "code"
community: "Frontend Theme Toggle"
location: "L24"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Theme_Toggle
---

# toggleTheme()

## Context

_Source: `frontend/src/theme.ts` (defined near L24; showing L22–L36 of 36)._

```typescript
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
- [[applyTheme()]] - `calls` [EXTRACTED]
- [[getTheme()]] - `calls` [EXTRACTED]
- [[theme.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Theme_Toggle