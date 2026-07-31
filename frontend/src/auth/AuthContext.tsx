import {
  createContext,
  useContext,
  useEffect,
  useState,
  useSyncExternalStore,
  type ReactNode,
} from "react";
import { getAccessToken, subscribeToken } from "../api/tokenStore";
import { getUserLoginFromToken, getUserNameFromToken, getUserRoleFromToken } from "../api/jwt";
import type { UserRole } from "../api/jwt";
import { refreshAccessToken } from "../api/client";
import { login as apiLogin, logout as apiLogout } from "../api/auth";
import type { LoginRequest } from "../api/types";

interface AuthContextValue {
  isAuthenticated: boolean;
  userName: string | null;
  userLogin: string | null;
  role: UserRole | null;
  /** Convenience for the many places that only care whether anything may be
   *  changed. A worker sees the schedule and edits nothing. */
  canEdit: boolean;
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

  const role = getUserRoleFromToken(token);
  const value: AuthContextValue = {
    isAuthenticated: token !== null,
    userName: getUserNameFromToken(token),
    userLogin: getUserLoginFromToken(token),
    role,
    canEdit: role === "Administrator",
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
