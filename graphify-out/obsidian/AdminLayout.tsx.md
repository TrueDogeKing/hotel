---
source_file: "frontend/src/components/admin/AdminLayout.tsx"
type: "code"
community: "Frontend App Shell & i18n"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_App_Shell__i18n
---

# AdminLayout.tsx

## Context

_Source: `frontend/src/components/admin/AdminLayout.tsx` (defined near L1; showing L1–L40 of 40)._

```tsx
import { type ReactNode } from "react";
import { NavLink } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { useAuth } from "../../auth/AuthContext";
import LanguageSwitcher from "../LanguageSwitcher";

// Shared admin shell: brand, section nav, language flags, logout.
export default function AdminLayout({ children }: { children: ReactNode }) {
  const { t } = useTranslation();
  const { logout } = useAuth();

  return (
    <main className="admin-page">
      <header className="public-header">
        <div className="admin-header-left">
          <div className="auth-brand">
            <span className="mark">C</span> {t("admin.dashboardTitle")}
          </div>
          <nav className="admin-nav">
            <NavLink to="/admin" end>
              {t("admin.nav.dashboard")}
            </NavLink>
            <NavLink to="/admin/turnusy">{t("admin.nav.sessions")}</NavLink>
            <NavLink to="/admin/pokoje">{t("admin.nav.rooms")}</NavLink>
            <NavLink to="/admin/rezerwacje">{t("admin.nav.bookings")}</NavLink>
            <NavLink to="/admin/zadania">{t("admin.nav.tasks")}</NavLink>
          </nav>
        </div>
        <div className="admin-header-actions">
          <LanguageSwitcher />
          <button type="button" onClick={() => void logout()}>
            {t("admin.logout")}
          </button>
        </div>
      </header>

      <section className="admin-content">{children}</section>
    </main>
  );
}
```

## Connections
- [[AdminBookingsPage.tsx]] - `imports_from` [EXTRACTED]
- [[AdminDashboardPage.tsx]] - `imports_from` [EXTRACTED]
- [[AdminLayout()]] - `contains` [EXTRACTED]
- [[AuthContext.tsx]] - `imports_from` [EXTRACTED]
- [[LanguageSwitcher()]] - `imports` [EXTRACTED]
- [[LanguageSwitcher.tsx]] - `imports_from` [EXTRACTED]
- [[RoomsPage.tsx]] - `imports_from` [EXTRACTED]
- [[SessionOccupancyPage.tsx]] - `imports_from` [EXTRACTED]
- [[SessionsPage.tsx]] - `imports_from` [EXTRACTED]
- [[TasksPage.tsx]] - `imports_from` [EXTRACTED]
- [[useAuth()]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_App_Shell__i18n