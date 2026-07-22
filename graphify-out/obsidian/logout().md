---
source_file: "frontend/src/api/auth.ts"
type: "code"
community: "Frontend Auth & API Client"
location: "L12"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Auth__API_Client
---

# logout()

## Context

_Source: `frontend/src/api/auth.ts` (defined near L12; showing L10–L18 of 18)._

```typescript

// Logs out (revokes the refresh cookie) and clears the in-memory token regardless of the result.
export async function logout(): Promise<void> {
  try {
    await api.post("/auth/logout");
  } finally {
    clearAccessToken();
  }
}
```

## Connections
- [[AuthContext.tsx]] - `imports` [EXTRACTED]
- [[auth.ts]] - `contains` [EXTRACTED]
- [[clearAccessToken()]] - `calls` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Auth__API_Client