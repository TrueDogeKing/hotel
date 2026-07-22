---
source_file: "frontend/src/theme.ts"
type: "code"
community: "Frontend Theme Toggle"
location: "L33"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Theme_Toggle
---

# subscribeTheme()

## Context

_Source: `frontend/src/theme.ts` (defined near L33; showing L31–L36 of 36)._

```typescript

// Subscribe to theme changes (used with useSyncExternalStore). Returns an unsubscribe function.
export function subscribeTheme(listener: Listener): () => void {
  listeners.add(listener);
  return () => listeners.delete(listener);
}
```

## Connections
- [[theme.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Theme_Toggle