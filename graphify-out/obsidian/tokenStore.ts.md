---
source_file: "frontend/src/api/tokenStore.ts"
type: "code"
community: "Frontend Auth & API Client"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Auth__API_Client
---

# tokenStore.ts

## Context

_Source: `frontend/src/api/tokenStore.ts` (defined near L1; showing L1–L26 of 26)._

```typescript
// In-memory store for the JWT access token. Kept out of localStorage on purpose
// (XSS-resistant); the token is re-acquired via the HttpOnly refresh cookie on boot and on 401.

type Listener = (token: string | null) => void;

let accessToken: string | null = null;
const listeners = new Set<Listener>();

export function getAccessToken(): string | null {
  return accessToken;
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
- [[AuthContext.tsx]] - `imports_from` [EXTRACTED]
- [[Listener]] - `contains` [EXTRACTED]
- [[auth.ts]] - `imports_from` [EXTRACTED]
- [[clearAccessToken()]] - `contains` [EXTRACTED]
- [[client.ts]] - `imports_from` [EXTRACTED]
- [[getAccessToken()]] - `contains` [EXTRACTED]
- [[listeners]] - `contains` [EXTRACTED]
- [[setAccessToken()]] - `contains` [EXTRACTED]
- [[subscribeToken()]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Auth__API_Client