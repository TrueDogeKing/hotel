import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { useAuth } from "../../auth/AuthContext";
import AdminLayout from "../../components/admin/AdminLayout";
import { getDashboard, type Dashboard } from "../../api/admin";
import { formatDate as formatIsoDate } from "../../utils/dates";

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

  const formatDate = (iso: string) => formatIsoDate(iso, i18n.language);

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
            <div className="stat-card">
              <strong>{dashboard.activeClosureCount}</strong>
              <span>
                <Link to="/admin/blokady">{t("dashboard.activeClosures")}</Link>
              </span>
            </div>
          </div>

          <h2>{t("dashboard.upcomingBookings")}</h2>
          {dashboard.upcomingBookings.length === 0 && <p>{t("dashboard.noBookings")}</p>}
          <table className="admin-table">
            <tbody>
              {dashboard.upcomingBookings.map((booking) => (
                <tr key={booking.id}>
                  <td>{booking.organizationName}</td>
                  <td>
                    {formatDate(booking.startDate)} –{" "}
                    {formatDate(booking.endDate)}
                  </td>
                  <td>{t("dashboard.beds", { count: booking.occupiedBeds })}</td>
                  <td>{t(`adminBookings.statuses.${booking.status}`)}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </>
      )}
    </AdminLayout>
  );
}
