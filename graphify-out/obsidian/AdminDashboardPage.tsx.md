---
source_file: "frontend/src/pages/admin/AdminDashboardPage.tsx"
type: "code"
community: "Frontend App Shell & i18n"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_App_Shell__i18n
---

# AdminDashboardPage.tsx

## Context

_Source: `frontend/src/pages/admin/AdminDashboardPage.tsx` (defined near L1; showing L1–L46 of 82)._

```tsx
import { useEffect, useMemo, useState } from "react";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { useAuth } from "../../auth/AuthContext";
import AdminLayout from "../../components/admin/AdminLayout";
import { getDashboard, type Dashboard } from "../../api/admin";

export default function AdminDashboardPage() {
  const { t, i18n } = useTranslation();
  const { userLogin } = useAuth();
  const [dashboard, setDashboard] = useState<Dashboard | null>(null);

  useEffect(() => {
    let cancelled = false;
    void getDashboard().then((data) => {
      if (!cancelled) setDashboard(data);
    });
    return () => {
      cancelled = true;
    };
  }, []);

  const dateFormatter = useMemo(
    () =>
      new Intl.DateTimeFormat(i18n.language === "en" ? "en-GB" : "pl-PL", {
        dateStyle: "medium",
      }),
    [i18n.language],
  );

  return (
    <AdminLayout>
      <p>{t("admin.welcome", { login: userLogin ?? "" })}</p>

      {dashboard && (
        <>
          <div className="stat-cards">
            <div className="stat-card">
              <strong>{dashboard.pendingDepositCount}</strong>
              <span>{t("dashboard.pendingDeposits")}</span>
            </div>
            <div className={`stat-card${dashboard.overdueFinalCount > 0 ? " warn" : ""}`}>
              <strong>{dashboard.overdueFinalCount}</strong>
              <span>{t("dashboard.overdueFinals")}</span>
            </div>
            <div className="stat-card">
```

## Connections
- [[AdminDashboardPage()]] - `contains` [EXTRACTED]
- [[AdminLayout.tsx]] - `imports_from` [EXTRACTED]
- [[App.tsx]] - `imports_from` [EXTRACTED]
- [[AuthContext.tsx]] - `imports_from` [EXTRACTED]
- [[Dashboard]] - `imports` [EXTRACTED]
- [[admin.ts]] - `imports_from` [EXTRACTED]
- [[getDashboard()]] - `imports` [EXTRACTED]
- [[useAuth()]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_App_Shell__i18n