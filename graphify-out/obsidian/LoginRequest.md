---
source_file: "frontend/src/api/types.ts"
type: "code"
community: "Frontend Auth & API Client"
location: "L3"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Auth__API_Client
---

# LoginRequest

## Context

_Source: `frontend/src/api/types.ts` (defined near L3; showing L1–L12 of 12)._

```typescript
// Shared API response/request shapes mirroring the backend DTOs.

export interface LoginRequest {
  login: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  expiresAtUtc: string;
  login: string;
}
```

## Connections
- [[AuthContext.tsx]] - `imports` [EXTRACTED]
- [[AuthContextValue]] - `references` [EXTRACTED]
- [[auth.ts]] - `imports` [EXTRACTED]
- [[types.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Auth__API_Client