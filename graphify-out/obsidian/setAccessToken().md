---
source_file: "frontend/src/api/tokenStore.ts"
type: "code"
community: "Frontend Auth & API Client"
location: "L13"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Auth__API_Client
---

# setAccessToken()

## Context

_Source: `frontend/src/api/tokenStore.ts` (defined near L13; showing L11–L26 of 26)._

```typescript
}

export function setAccessToken(token: string | null): void {
  accessToken = token;
  listeners.forEach((listener) => listener(token));
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
- [[clearAccessToken()]] - `calls` [EXTRACTED]
- [[client.ts]] - `imports` [EXTRACTED]
- [[login()]] - `calls` [EXTRACTED]
- [[refreshAccessToken()]] - `calls` [EXTRACTED]
- [[tokenStore.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Auth__API_Client