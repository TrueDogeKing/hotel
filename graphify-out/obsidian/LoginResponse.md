---
source_file: "frontend/src/api/types.ts"
type: "code"
community: "Frontend Auth & API Client"
location: "L8"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Auth__API_Client
---

# LoginResponse

## Context

_Source: `frontend/src/api/types.ts` (defined near L8; showing L6–L12 of 12)._

```typescript
}

export interface LoginResponse {
  token: string;
  expiresAtUtc: string;
  login: string;
}
```

## Connections
- [[auth.ts]] - `imports` [EXTRACTED]
- [[client.ts]] - `imports` [EXTRACTED]
- [[types.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Auth__API_Client