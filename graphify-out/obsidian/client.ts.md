---
source_file: "frontend/src/api/client.ts"
type: "code"
community: "Frontend Auth & API Client"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Auth__API_Client
---

# client.ts

## Context

_Source: `frontend/src/api/client.ts` (defined near L1; showing L1–L46 of 63)._

```typescript
import axios, { AxiosError, type InternalAxiosRequestConfig } from "axios";
import { clearAccessToken, getAccessToken, setAccessToken } from "./tokenStore";
import type { LoginResponse } from "./types";

// All requests go through the Vite dev proxy (/api -> backend), so cookies stay same-origin.
export const api = axios.create({
  baseURL: "/api",
  withCredentials: true,
});

// Attach the bearer token to every outgoing request.
api.interceptors.request.use((config) => {
  const token = getAccessToken();
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Exchanges the HttpOnly refresh cookie for a fresh access token. Concurrent callers share one
// in-flight request via refreshPromise. A raw axios call is used to bypass the interceptors below.
let refreshPromise: Promise<string | null> | null = null;

export function refreshAccessToken(): Promise<string | null> {
  refreshPromise ??= axios
    .post<LoginResponse>("/api/auth/refresh", null, { withCredentials: true })
    .then((response) => {
      setAccessToken(response.data.token);
      return response.data.token;
    })
    .catch(() => {
      clearAccessToken();
      return null;
    })
    .finally(() => {
      refreshPromise = null;
    });

  return refreshPromise;
}

type RetriableConfig = InternalAxiosRequestConfig & { _retry?: boolean };

// On 401, try a silent refresh once and replay the original request.
api.interceptors.response.use(
  (response) => response,
```

## Connections
- [[AuthContext.tsx]] - `imports_from` [EXTRACTED]
- [[LoginResponse]] - `imports` [EXTRACTED]
- [[RetriableConfig]] - `contains` [EXTRACTED]
- [[admin.ts]] - `imports_from` [EXTRACTED]
- [[api]] - `contains` [EXTRACTED]
- [[auth.ts]] - `imports_from` [EXTRACTED]
- [[clearAccessToken()]] - `imports` [EXTRACTED]
- [[getAccessToken()]] - `imports` [EXTRACTED]
- [[public.ts]] - `imports_from` [EXTRACTED]
- [[refreshAccessToken()]] - `contains` [EXTRACTED]
- [[setAccessToken()]] - `imports` [EXTRACTED]
- [[tokenStore.ts]] - `imports_from` [EXTRACTED]
- [[types.ts]] - `imports_from` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Auth__API_Client