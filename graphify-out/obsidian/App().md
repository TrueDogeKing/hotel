---
source_file: "frontend/src/App.tsx"
type: "code"
community: "Frontend App Shell & i18n"
location: "L17"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_App_Shell__i18n
---

# App()

## Context

_Source: `frontend/src/App.tsx` (defined near L17; showing L15–L41 of 41)._

```tsx
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
- [[App.tsx]] - `contains` [EXTRACTED]
- [[main.tsx]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_App_Shell__i18n