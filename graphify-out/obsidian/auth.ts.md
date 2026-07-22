---
source_file: "frontend/src/api/auth.ts"
type: "code"
community: "Frontend Auth & API Client"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Auth__API_Client
---

# auth.ts

## Context

_Source: `frontend/src/api/auth.ts` (defined near L1; showing L1–L18 of 18)._

```typescript
import { api } from "./client";
import { clearAccessToken, setAccessToken } from "./tokenStore";
import type { LoginRequest, LoginResponse } from "./types";

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
- [[AuthContext.tsx]] - `imports_from` [EXTRACTED]
- [[LoginRequest]] - `imports` [EXTRACTED]
- [[LoginResponse]] - `imports` [EXTRACTED]
- [[api]] - `imports` [EXTRACTED]
- [[clearAccessToken()]] - `imports` [EXTRACTED]
- [[client.ts]] - `imports_from` [EXTRACTED]
- [[login()]] - `contains` [EXTRACTED]
- [[logout()]] - `contains` [EXTRACTED]
- [[setAccessToken()]] - `imports` [EXTRACTED]
- [[tokenStore.ts]] - `imports_from` [EXTRACTED]
- [[types.ts]] - `imports_from` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Auth__API_Client