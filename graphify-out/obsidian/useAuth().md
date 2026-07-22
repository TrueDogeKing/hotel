---
source_file: "frontend/src/auth/AuthContext.tsx"
type: "code"
community: "Frontend App Shell & i18n"
location: "L50"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_App_Shell__i18n
---

# useAuth()

## Context

_Source: `frontend/src/auth/AuthContext.tsx` (defined near L50; showing L48–L56 of 56)._

```tsx
// Hook colocated with its provider; the Fast Refresh rule only matters for dev DX.
// eslint-disable-next-line react-refresh/only-export-components
export function useAuth(): AuthContextValue {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider.");
  }
  return context;
}
```

## Connections
- [[AdminDashboardPage()]] - `calls` [EXTRACTED]
- [[AdminDashboardPage.tsx]] - `imports` [EXTRACTED]
- [[AdminLayout()]] - `calls` [EXTRACTED]
- [[AdminLayout.tsx]] - `imports` [EXTRACTED]
- [[AuthContext.tsx]] - `contains` [EXTRACTED]
- [[LoginPage()]] - `calls` [EXTRACTED]
- [[LoginPage.tsx]] - `imports` [EXTRACTED]
- [[ProtectedRoute()]] - `calls` [EXTRACTED]
- [[ProtectedRoute.tsx]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_App_Shell__i18n