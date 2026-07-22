---
source_file: "frontend/src/api/client.ts"
type: "code"
community: "Frontend Auth & API Client"
location: "L24"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Auth__API_Client
---

# refreshAccessToken()

## Context

_Source: `frontend/src/api/client.ts` (defined near L24; showing L22–L63 of 63)._

```typescript
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
  async (error: AxiosError) => {
    const original = error.config as RetriableConfig | undefined;
    const isAuthCall =
      original?.url?.includes("/auth/login") || original?.url?.includes("/auth/refresh");

    if (error.response?.status === 401 && original && !original._retry && !isAuthCall) {
      original._retry = true;
      const token = await refreshAccessToken();
      if (token) {
        original.headers.Authorization = `Bearer ${token}`;
        return api(original);
      }
    }

    return Promise.reject(error);
  },
);
```

## Connections
- [[AuthContext.tsx]] - `imports` [EXTRACTED]
- [[AuthProvider()]] - `calls` [EXTRACTED]
- [[clearAccessToken()]] - `calls` [EXTRACTED]
- [[client.ts]] - `contains` [EXTRACTED]
- [[setAccessToken()]] - `calls` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Auth__API_Client