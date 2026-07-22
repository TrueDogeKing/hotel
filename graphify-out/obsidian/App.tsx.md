---
source_file: "frontend/src/App.tsx"
type: "code"
community: "Frontend App Shell & i18n"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_App_Shell__i18n
---

# App.tsx

## Context

_Source: `frontend/src/App.tsx` (defined near L1; showing L1–L41 of 41)._

```tsx
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
import SessionsPage from "./pages/admin/SessionsPage";
import SessionOccupancyPage from "./pages/admin/SessionOccupancyPage";
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
            <Route path="/admin/turnusy" element={<SessionsPage />} />
            <Route path="/admin/turnusy/:id" element={<SessionOccupancyPage />} />
            <Route path="/admin/rezerwacje" element={<AdminBookingsPage />} />
            <Route path="/admin/zadania" element={<TasksPage />} />
          </Route>
        </Routes>
      </AuthProvider>
    </BrowserRouter>
  );
}
```

## Connections
- [[AdminBookingsPage.tsx]] - `imports_from` [EXTRACTED]
- [[AdminDashboardPage.tsx]] - `imports_from` [EXTRACTED]
- [[App()]] - `contains` [EXTRACTED]
- [[AuthContext.tsx]] - `imports_from` [EXTRACTED]
- [[AuthProvider()]] - `imports` [EXTRACTED]
- [[BookingManagePage.tsx]] - `imports_from` [EXTRACTED]
- [[BookingWizardPage.tsx]] - `imports_from` [EXTRACTED]
- [[LoginPage.tsx]] - `imports_from` [EXTRACTED]
- [[PaymentReturnPage.tsx]] - `imports_from` [EXTRACTED]
- [[ProtectedRoute()]] - `imports` [EXTRACTED]
- [[ProtectedRoute.tsx]] - `imports_from` [EXTRACTED]
- [[PublicHomePage.tsx]] - `imports_from` [EXTRACTED]
- [[RoomsPage.tsx]] - `imports_from` [EXTRACTED]
- [[SessionOccupancyPage.tsx]] - `imports_from` [EXTRACTED]
- [[SessionsPage.tsx]] - `imports_from` [EXTRACTED]
- [[TasksPage.tsx]] - `imports_from` [EXTRACTED]
- [[main.tsx]] - `imports_from` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_App_Shell__i18n