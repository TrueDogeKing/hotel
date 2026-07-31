import { Navigate, Outlet, useLocation } from "react-router-dom";
import { useAuth } from "./AuthContext";

// Guards nested routes: waits for the initial silent refresh, then renders the routes
// when authenticated or redirects to /admin/logowanie (remembering where the user was headed).
//
// Authentication only — there is no per-route role gate, because both roles see
// every section. What a worker may *do* is decided by the API, and mirrored by
// each page hiding the controls it would refuse.
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
