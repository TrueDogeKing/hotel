---
source_file: "frontend/src/auth/AuthContext.tsx"
type: "code"
community: "Frontend Auth & API Client"
location: "L15"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Auth__API_Client
---

# AuthContextValue

## Context

_Source: `frontend/src/auth/AuthContext.tsx` (defined near L15; showing L13–L56 of 56)._

```tsx
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
- [[AuthContext.tsx]] - `contains` [EXTRACTED]
- [[LoginRequest]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_Auth__API_Client