---
source_file: "frontend/src/api/client.ts"
type: "code"
community: "Frontend Auth & API Client"
location: "L42"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Auth__API_Client
---

# RetriableConfig

## Context

_Source: `frontend/src/api/client.ts` (defined near L42; showing L40–L63 of 63)._

```typescript
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
- [[client.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Auth__API_Client