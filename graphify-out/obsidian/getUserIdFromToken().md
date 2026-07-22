---
source_file: "frontend/src/api/jwt.ts"
type: "code"
community: "Frontend Auth & API Client"
location: "L41"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Auth__API_Client
---

# getUserIdFromToken()

## Context

_Source: `frontend/src/api/jwt.ts` (defined near L41; showing L39–L44 of 44)._

```typescript

// The user id ("sub" claim); used to tell own messages/participants from others'.
export function getUserIdFromToken(token: string | null): string | null {
  if (!token) return null;
  return decodeJWT(token)?.sub ?? null;
}
```

## Connections
- [[decodeJWT()]] - `calls` [EXTRACTED]
- [[jwt.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Auth__API_Client