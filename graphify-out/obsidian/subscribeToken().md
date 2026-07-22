---
source_file: "frontend/src/api/tokenStore.ts"
type: "code"
community: "Frontend Auth & API Client"
location: "L23"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Auth__API_Client
---

# subscribeToken()

## Context

_Source: `frontend/src/api/tokenStore.ts` (defined near L23; showing L21–L26 of 26)._

```typescript

// Subscribe to token changes (used by the auth state hook). Returns an unsubscribe function.
export function subscribeToken(listener: Listener): () => void {
  listeners.add(listener);
  return () => listeners.delete(listener);
}
```

## Connections
- [[AuthContext.tsx]] - `imports` [EXTRACTED]
- [[AuthProvider()]] - `indirect_call` [INFERRED]
- [[tokenStore.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Auth__API_Client