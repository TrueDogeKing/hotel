import { useCallback, useEffect, useRef, useState } from "react";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import { useAuth } from "../../auth/AuthContext";
import AdminLayout from "../../components/admin/AdminLayout";
import GroupSchedulePanel from "../../components/admin/GroupSchedulePanel";
import AddGroupForm from "../../components/admin/AddGroupForm";
import AdminTiles from "../../components/admin/AdminTiles";
import BookingGroupSection from "../../components/admin/BookingGroupSection";
import {
  bookingGroupCategories,
  getDashboard,
  setBookingStatus,
  type BookingStatus,
  type Dashboard,
} from "../../api/admin";
import { scrollPanelIntoView } from "../../utils/scroll";

export default function AdminDashboardPage() {
  const { t } = useTranslation();
  // A worker reads the dashboard; adding a group and changing a status are writes.
  const { userLogin, canEdit } = useAuth();
  const [dashboard, setDashboard] = useState<Dashboard | null>(null);
  const [selectedBookingId, setSelectedBookingId] = useState<string | null>(null);
  const [adding, setAdding] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [notice, setNotice] = useState<string | null>(null);
  const groupPanelRef = useRef<HTMLDivElement>(null);
  // Bumped after anything that can move a group between the three lists, so the
  // open folds re-fetch. A counter rather than a callback: each fold decides for
  // itself whether it has anything loaded to refresh.
  const [refreshToken, setRefreshToken] = useState(0);

  const reload = useCallback(async () => {
    setDashboard(await getDashboard());
    setRefreshToken((current) => current + 1);
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


  return (
    <AdminLayout>
      <p>{t("admin.welcome", { login: userLogin ?? "" })}</p>

      {/* The sections, as tiles. This is the way into them — the header carries no
          section links, so the dashboard is where a view is chosen. */}
      <AdminTiles />

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
            <h2>{t("dashboard.groups.title")}</h2>
            {canEdit && (
              <button type="button" onClick={() => setAdding((open) => !open)}>
                {adding ? t("dashboard.addGroupCancel") : t("dashboard.addGroup")}
              </button>
            )}
          </div>

          {error && <p role="alert">{error}</p>}
          {notice && <p className="group-panel-notice">{notice}</p>}

          {canEdit && adding && (
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
              {/* Who is here, who is coming, and everything finished with — in that
                  order, and each loading only when it is opened. Only the current
                  groups are open to begin with: the other two are reference. */}
              {bookingGroupCategories.map((category) => (
                <BookingGroupSection
                  key={category}
                  category={category}
                  defaultOpen={category === "Current"}
                  selectedBookingId={selectedBookingId}
                  onSelect={setSelectedBookingId}
                  onStatusChange={handleStatusChange}
                  refreshToken={refreshToken}
                />
              ))}
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
