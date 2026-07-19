import { BrowserRouter, Routes, Route } from "react-router-dom";
import { AuthProvider } from "./auth/AuthContext";
import ProtectedRoute from "./auth/ProtectedRoute";
import PublicHomePage from "./pages/PublicHomePage";
import LoginPage from "./pages/LoginPage";
import AdminDashboardPage from "./pages/admin/AdminDashboardPage";
import RoomsPage from "./pages/admin/RoomsPage";
import SessionsPage from "./pages/admin/SessionsPage";
import "./App.css";

export default function App() {
  return (
    <BrowserRouter>
      <AuthProvider>
        <Routes>
          <Route path="/" element={<PublicHomePage />} />
          <Route path="/admin/logowanie" element={<LoginPage />} />

          {/* Admin panel (requires authentication) */}
          <Route element={<ProtectedRoute />}>
            <Route path="/admin" element={<AdminDashboardPage />} />
            <Route path="/admin/pokoje" element={<RoomsPage />} />
            <Route path="/admin/turnusy" element={<SessionsPage />} />
          </Route>
        </Routes>
      </AuthProvider>
    </BrowserRouter>
  );
}
