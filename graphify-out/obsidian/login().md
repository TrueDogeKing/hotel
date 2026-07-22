---
source_file: "frontend/src/api/auth.ts"
type: "code"
community: "Frontend Auth & API Client"
location: "L6"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Auth__API_Client
---

# login()

## Context

_Source: `frontend/src/api/auth.ts` (defined near L6; showing L4–L18 of 18)._

```typescript

// Logs in and stores the access token in memory. The refresh token is set as an HttpOnly cookie.
export async function login(credentials: LoginRequest): Promise<void> {
  const { data } = await api.post<LoginResponse>("/auth/login", credentials);
  setAccessToken(data.token);
}

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
- [[setAccessToken()]] - `calls` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Auth__API_Client