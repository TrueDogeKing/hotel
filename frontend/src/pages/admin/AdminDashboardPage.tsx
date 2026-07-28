import { useCallback, useEffect, useRef, useState } from "react";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import { useAuth } from "../../auth/AuthContext";
import AdminLayout from "../../components/admin/AdminLayout";
import GroupSchedulePanel from "../../components/admin/GroupSchedulePanel";
import AddGroupForm from "../../components/admin/AddGroupForm";
import {
  bookingStatuses,
  getDashboard,
  setBookingStatus,
  type BookingStatus,
  type Dashboard,
} from "../../api/admin";
import { formatDate as formatIsoDate } from "../../utils/dates";
import { scrollPanelIntoView } from "../../utils/scroll";

export default function AdminDashboardPage() {
  const { t, i18n } = useTranslation();
  const { userLogin } = useAuth();
  const [dashboard, setDashboard] = useState<Dashboard | null>(null);
  const [selectedBookingId, setSelectedBookingId] = useState<string | null>(null);
  const [adding, setAdding] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [notice, setNotice] = useState<string | null>(null);
  const groupPanelRef = useRef<HTMLDivElement>(null);

  const reload = useCallback(async () => {
    setDashboard(await getDashboard());
  }, []);

  useEffect(() => {
    let cancelled = false;
    void getDashboard()
      .then((data) => {
        if (!cancelled) setDashboard(data);
      })
      .catch(() => {
        if (!cancelled) setError(t("dashboard.loadError"));
      });
    return () => {
      cancelled = true;
    };
  }, [t]);

  function handleApiError(err: unknown, fallback: string) {
    if (isAxiosError(err) && err.response) {
      const detail = (err.response.data as { detail?: string } | undefined)?.detail;
      setError(detail ?? fallback);
    } else {
      setError(fallback);
    }
  }

  async function handleStatusChange(id: string, status: BookingStatus) {
    setError(null);
    setNotice(null);
    try {
      await setBookingStatus(id, status);
      setNotice(t("dashboard.statusChanged", { status: t(`adminBookings.statuses.${status}`) }));
      await reload();
    } catch (err) {
      handleApiError(err, t("dashboard.statusError"));
    }
  }

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

          <div className="schedule-toolbar">
            <h2>{t("dashboard.upcomingBookings")}</h2>
            <button type="button" onClick={() => setAdding((open) => !open)}>
              {adding ? t("dashboard.addGroupCancel") : t("dashboard.addGroup")}
            </button>
          </div>

          {error && <p role="alert">{error}</p>}
          {notice && <p className="group-panel-notice">{notice}</p>}

          {adding && (
            <AddGroupForm
              onCreated={async (booking) => {
                setAdding(false);
                setError(null);
                setNotice(
                  t("dashboard.groupAdded", { organization: booking.organizationName }),
                );
                await reload();
                setSelectedBookingId(booking.id);
              }}
              onError={(err) => handleApiError(err, t("dashboard.addGroupError"))}
              onCancel={() => setAdding(false)}
            />
          )}

          <div className="schedule-layout">
            <div className="schedule-main">
              {dashboard.upcomingBookings.length === 0 && <p>{t("dashboard.noBookings")}</p>}
              <table className="admin-table">
                <tbody>
                  {dashboard.upcomingBookings.map((booking) => (
                    <tr
                      key={booking.id}
                      className={booking.id === selectedBookingId ? "selected-row" : ""}
                      onClick={() =>
                        setSelectedBookingId(
                          selectedBookingId === booking.id ? null : booking.id,
                        )
                      }
                    >
                      <td>{booking.organizationName}</td>
                      <td>
                        {formatDate(booking.startDate)} – {formatDate(booking.endDate)}
                      </td>
                      <td>{t("dashboard.beds", { count: booking.occupiedBeds })}</td>
                      <td>
                        {/* Stop propagation so picking a status doesn't also
                            toggle the row's programme panel. */}
                        <select
                          value={booking.status}
                          aria-label={t("dashboard.status")}
                          onClick={(e) => e.stopPropagation()}
                          onChange={(e) => {
                            e.stopPropagation();
                            void handleStatusChange(
                              booking.id,
                              e.target.value as BookingStatus,
                            );
                          }}
                        >
                          {bookingStatuses.map((status) => (
                            <option key={status} value={status}>
                              {t(`adminBookings.statuses.${status}`)}
                            </option>
                          ))}
                        </select>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>

            {selectedBookingId && (
              <div ref={groupPanelRef} className="schedule-group-anchor">
                {/* Scrolls once the panel has content: the bookings table above is
                    short, so an empty loading box leaves the page too short to
                    bring the panel to the top. */}
                <GroupSchedulePanel
                  bookingId={selectedBookingId}
                  onClose={() => setSelectedBookingId(null)}
                  onChanged={() => void reload()}
                  onLoaded={() => scrollPanelIntoView(groupPanelRef.current)}
                />
              </div>
            )}
          </div>
        </>
      )}
    </AdminLayout>
  );
}
