import { BrowserRouter, Routes, Route } from "react-router-dom";
import { AuthProvider } from "./auth/AuthContext";
import ProtectedRoute from "./auth/ProtectedRoute";
import PublicHomePage from "./pages/PublicHomePage";
import BookingWizardPage from "./pages/BookingWizardPage";
import BookingManagePage from "./pages/BookingManagePage";
import PaymentReturnPage from "./pages/PaymentReturnPage";
import LoginPage from "./pages/LoginPage";
import AdminDashboardPage from "./pages/admin/AdminDashboardPage";
import RoomsPage from "./pages/admin/RoomsPage";
import ClosuresPage from "./pages/admin/ClosuresPage";
import OccupancyPage from "./pages/admin/OccupancyPage";
import AdminBookingsPage from "./pages/admin/AdminBookingsPage";
import TasksPage from "./pages/admin/TasksPage";
import "./App.css";

export default function App() {
  return (
    <BrowserRouter>
      <AuthProvider>
        <Routes>
          <Route path="/" element={<PublicHomePage />} />
          <Route path="/rezerwacja" element={<BookingWizardPage />} />
          <Route path="/rezerwacja/:token" element={<BookingManagePage />} />
          <Route path="/platnosc/powrot" element={<PaymentReturnPage />} />
          <Route path="/admin/logowanie" element={<LoginPage />} />

          {/* Admin panel (requires authentication) */}
          <Route element={<ProtectedRoute />}>
            <Route path="/admin" element={<AdminDashboardPage />} />
            <Route path="/admin/pokoje" element={<RoomsPage />} />
            <Route path="/admin/blokady" element={<ClosuresPage />} />
            <Route path="/admin/oblozenie" element={<OccupancyPage />} />
            <Route path="/admin/rezerwacje" element={<AdminBookingsPage />} />
            <Route path="/admin/zadania" element={<TasksPage />} />
          </Route>
        </Routes>
      </AuthProvider>
    </BrowserRouter>
  );
}
