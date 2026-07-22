---
source_file: "frontend/src/api/jwt.ts"
type: "code"
community: "Frontend Auth & API Client"
location: "L25"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Auth__API_Client
---

# getUserNameFromToken()

## Context

_Source: `frontend/src/api/jwt.ts` (defined near L25; showing L23–L44 of 44)._

```typescript
}

export function getUserNameFromToken(token: string | null): string | null {
  if (!token) return null;
  const payload = decodeJWT(token);
  const firstName = payload?.given_name || "";
  const lastName = payload?.family_name || "";
  const fullName = `${firstName} ${lastName}`.trim();
  return fullName || null;
}

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