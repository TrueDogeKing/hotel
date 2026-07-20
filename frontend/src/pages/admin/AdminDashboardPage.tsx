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
              <strong>{dashboard.openTaskCount}</strong>
              <span>
                <Link to="/admin/zadania">{t("dashboard.openTasks")}</Link>
              </span>
            </div>
          </div>

          <h2>{t("dashboard.upcomingSessions")}</h2>
          {dashboard.upcomingSessions.length === 0 && <p>{t("dashboard.noSessions")}</p>}
          <table className="admin-table">
            <tbody>
              {dashboard.upcomingSessions.map((session) => (
                <tr key={session.id}>
                  <td>
                    <Link to={`/admin/turnusy/${session.id}`}>{session.name}</Link>
                  </td>
                  <td>
                    {dateFormatter.format(new Date(session.startDate))} –{" "}
                    {dateFormatter.format(new Date(session.endDate))}
                  </td>
                  <td>
                    {t("dashboard.occupancy", {
                      occupied: session.occupiedBeds,
                      total: session.totalBeds,
                    })}
                  </td>
                  <td>{t("dashboard.bookings", { count: session.bookingCount })}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </>
      )}
    </AdminLayout>
  );
}
