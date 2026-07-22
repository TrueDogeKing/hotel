---
source_file: "frontend/src/api/jwt.ts"
type: "code"
community: "Frontend Auth & API Client"
location: "L35"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Auth__API_Client
---

# getUserLoginFromToken()

## Context

_Source: `frontend/src/api/jwt.ts` (defined near L35; showing L33–L44 of 44)._

```typescript

// The unique login ("preferred_username" claim).
export function getUserLoginFromToken(token: string | null): string | null {
  if (!token) return null;
  return decodeJWT(token)?.preferred_username ?? null;
}

// The user id ("sub" claim); used to tell own messages/participants from others'.
export function getUserIdFromToken(token: string | null): string | null {
  if (!token) return null;
  return decodeJWT(token)?.sub ?? null;
}
```

## Connections
- [[AuthContext.tsx]] - `imports` [EXTRACTED]
- [[AuthProvider()]] - `calls` [EXTRACTED]
- [[decodeJWT()]] - `calls` [EXTRACTED]
- [[jwt.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Auth__API_Client