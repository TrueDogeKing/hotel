---
source_file: "frontend/src/auth/AuthContext.tsx"
type: "code"
community: "Frontend Auth & API Client"
location: "L25"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_Auth__API_Client
---

# AuthContext

## Context

_Source: `frontend/src/auth/AuthContext.tsx` (defined near L25; showing L23–L56 of 56)._

```tsx
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

#graphify/code #graphify/EXTRACTED #community/Frontend_Auth__API_Client