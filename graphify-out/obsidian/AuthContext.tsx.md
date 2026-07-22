---
source_file: "frontend/src/auth/AuthContext.tsx"
type: "code"
community: "Frontend Auth & API Client"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Auth__API_Client
---

# AuthContext.tsx

## Context

_Source: `frontend/src/auth/AuthContext.tsx` (defined near L1; showing L1–L46 of 56)._

```tsx
import {
  createContext,
  useContext,
  useEffect,
  useState,
  useSyncExternalStore,
  type ReactNode,
} from "react";
import { getAccessToken, subscribeToken } from "../api/tokenStore";
import { getUserLoginFromToken, getUserNameFromToken } from "../api/jwt";
import { refreshAccessToken } from "../api/client";
import { login as apiLogin, logout as apiLogout } from "../api/auth";
import type { LoginRequest } from "../api/types";

interface AuthContextValue {
  isAuthenticated: boolean;
  userName: string | null;
  userLogin: string | null;
  // True until the initial silent refresh resolves; used to avoid premature redirects.
  isBooting: boolean;
  login: (credentials: LoginRequest) => Promise<void>;
  logout: () => Promise<void>;
}

const AuthContext = createContext<AuthContextValue | undefined>(undefined);

export function AuthProvider({ children }: { children: ReactNode }) {
  const token = useSyncExternalStore(subscribeToken, getAccessToken);
  const [isBooting, setIsBooting] = useState(true);

  // Restore the session from the HttpOnly refresh cookie on first load.
  useEffect(() => {
    refreshAccessToken().finally(() => setIsBooting(false));
  }, []);

  const value: AuthContextValue = {
    isAuthenticated: token !== null,
    userName: getUserNameFromToken(token),
    userLogin: getUserLoginFromToken(token),
    isBooting,
    login: (credentials) => apiLogin(credentials),
    logout: () => apiLogout(),
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
```

## Connections
- [[AdminDashboardPage.tsx]] - `imports_from` [EXTRACTED]
- [[AdminLayout.tsx]] - `imports_from` [EXTRACTED]
- [[App.tsx]] - `imports_from` [EXTRACTED]
- [[AuthContext]] - `contains` [EXTRACTED]
- [[AuthContextValue]] - `contains` [EXTRACTED]
- [[AuthProvider()]] - `contains` [EXTRACTED]
- [[LoginPage.tsx]] - `imports_from` [EXTRACTED]
- [[LoginRequest]] - `imports` [EXTRACTED]
- [[ProtectedRoute.tsx]] - `imports_from` [EXTRACTED]
- [[auth.ts]] - `imports_from` [EXTRACTED]
- [[client.ts]] - `imports_from` [EXTRACTED]
- [[getAccessToken()]] - `imports` [EXTRACTED]
- [[getUserLoginFromToken()]] - `imports` [EXTRACTED]
- [[getUserNameFromToken()]] - `imports` [EXTRACTED]
- [[jwt.ts]] - `imports_from` [EXTRACTED]
- [[login()]] - `imports` [EXTRACTED]
- [[logout()]] - `imports` [EXTRACTED]
- [[refreshAccessToken()]] - `imports` [EXTRACTED]
- [[subscribeToken()]] - `imports` [EXTRACTED]
- [[tokenStore.ts]] - `imports_from` [EXTRACTED]
- [[types.ts]] - `imports_from` [EXTRACTED]
- [[useAuth()]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Auth__API_Client