---
source_file: "frontend/src/auth/ProtectedRoute.tsx"
type: "code"
community: "Frontend App Shell & i18n"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_App_Shell__i18n
---

# ProtectedRoute.tsx

## Context

_Source: `frontend/src/auth/ProtectedRoute.tsx` (defined near L1; showing L1–L19 of 19)._

```tsx
import { Navigate, Outlet, useLocation } from "react-router-dom";
import { useAuth } from "./AuthContext";

// Guards nested routes: waits for the initial silent refresh, then renders the routes
// when authenticated or redirects to /admin/logowanie (remembering where the user was headed).
export default function ProtectedRoute() {
  const { isAuthenticated, isBooting } = useAuth();
  const location = useLocation();

  if (isBooting) {
    return <p>Loading…</p>;
  }

  if (!isAuthenticated) {
    return <Navigate to="/admin/logowanie" replace state={{ from: location }} />;
  }

  return <Outlet />;
}
```

## Connections
- [[App.tsx]] - `imports_from` [EXTRACTED]
- [[AuthContext.tsx]] - `imports_from` [EXTRACTED]
- [[ProtectedRoute()]] - `contains` [EXTRACTED]
- [[useAuth()]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_App_Shell__i18n