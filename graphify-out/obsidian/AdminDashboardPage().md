---
source_file: "frontend/src/pages/admin/AdminDashboardPage.tsx"
type: "code"
community: "Frontend App Shell & i18n"
location: "L8"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_App_Shell__i18n
---

# AdminDashboardPage()

## Context

_Source: `frontend/src/pages/admin/AdminDashboardPage.tsx` (defined near L8; showing L6–L53 of 82)._

```tsx
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
              <strong>{dashboard.openTaskCount}</strong>
              <span>
                <Link to="/admin/zadania">{t("dashboard.openTasks")}</Link>
              </span>
            </div>
          </div>

```

## Connections
- [[AdminDashboardPage.tsx]] - `contains` [EXTRACTED]
- [[getDashboard()]] - `calls` [EXTRACTED]
- [[useAuth()]] - `calls` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_App_Shell__i18n