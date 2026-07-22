---
source_file: "frontend/src/api/tokenStore.ts"
type: "code"
community: "Frontend Auth & API Client"
location: "L18"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Auth__API_Client
---

# clearAccessToken()

## Context

_Source: `frontend/src/api/tokenStore.ts` (defined near L18; showing L16–L26 of 26)._

```typescript
}

export function clearAccessToken(): void {
  setAccessToken(null);
}

// Subscribe to token changes (used by the auth state hook). Returns an unsubscribe function.
export function subscribeToken(listener: Listener): () => void {
  listeners.add(listener);
  return () => listeners.delete(listener);
}
```

## Connections
- [[auth.ts]] - `imports` [EXTRACTED]
- [[client.ts]] - `imports` [EXTRACTED]
- [[logout()]] - `calls` [EXTRACTED]
- [[refreshAccessToken()]] - `calls` [EXTRACTED]
- [[setAccessToken()]] - `calls` [EXTRACTED]
- [[tokenStore.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Auth__API_Client