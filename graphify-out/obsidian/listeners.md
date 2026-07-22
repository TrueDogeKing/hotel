---
source_file: "frontend/src/api/tokenStore.ts"
type: "code"
community: "Frontend Auth & API Client"
location: "L7"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Auth__API_Client
---

# listeners

## Context

_Source: `frontend/src/api/tokenStore.ts` (defined near L7; showing L5–L26 of 26)._

```typescript

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
- [[tokenStore.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Auth__API_Client