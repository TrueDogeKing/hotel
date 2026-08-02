import { Fragment, useCallback, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import AdminLayout from "../../components/admin/AdminLayout";
import { useAuth } from "../../auth/AuthContext";
import ConfirmDialog from "../../components/ConfirmDialog";
import {
  bookingStatuses,
  cancelAdminBooking,
  getAdminBookings,
  setBookingStatus,
  type AdminBooking,
  type BookingStatus,
} from "../../api/admin";
import { formatDate as formatIsoDate } from "../../utils/dates";

export default function AdminBookingsPage() {
  const { t, i18n } = useTranslation();
  const { canEdit } = useAuth();
  const [bookings, setBookings] = useState<AdminBooking[]>([]);
  const [statusFilter, setStatusFilter] = useState("");
  const [expanded, setExpanded] = useState<string | null>(null);
  const [cancelTarget, setCancelTarget] = useState<AdminBooking | null>(null);
  const [error, setError] = useState<string | null>(null);

  const reload = useCallback(async () => {
    setBookings(await getAdminBookings({ status: statusFilter || undefined }));
  }, [statusFilter]);

  useEffect(() => {
    let cancelled = false;
    void getAdminBookings({ status: statusFilter || undefined }).then((data) => {
      if (!cancelled) setBookings(data);
    });
    return () => {
      cancelled = true;
    };
  }, [statusFilter]);

  async function confirmCancel() {
    if (!cancelTarget) return;
    setError(null);
    try {
      await cancelAdminBooking(cancelTarget.id);
      await reload();
    } catch {
      setError(t("adminBookings.cancelError"));
    } finally {
      setCancelTarget(null);
    }
  }

  // Reviving a cancelled booking is only reachable from here: the dashboard and the
  // calendar both list live bookings only, so a cancelled group disappears from them.
  async function handleStatusChange(id: string, status: BookingStatus) {
    setError(null);
    try {
      await setBookingStatus(id, status);
      await reload();
    } catch (err) {
      if (isAxiosError(err) && err.response) {
        const detail = (err.response.data as { detail?: string } | undefined)?.detail;
        setError(detail ?? t("adminBookings.statusError"));
      } else {
        setError(t("adminBookings.statusError"));
      }
    }
  }

  const formatDate = (iso: string) => formatIsoDate(iso, i18n.language);

  return (
    <AdminLayout>
      <h1>{t("adminBookings.title")}</h1>

      <div className="admin-form">
        <label>
          {t("adminBookings.filterStatus")}
          <select value={statusFilter} onChange={(e) => setStatusFilter(e.target.value)}>
            <option value="">{t("adminBookings.allStatuses")}</option>
            {["PendingDeposit", "Confirmed", "Cancelled", "Completed"].map((s) => (
              <option key={s} value={s}>
                {t(`adminBookings.statuses.${s}`)}
              </option>
            ))}
          </select>
        </label>
      </div>

      {error && <p role="alert">{error}</p>}

      <table className="admin-table">
        <thead>
          <tr>
            <th>{t("adminBookings.organization")}</th>
            <th>{t("adminBookings.dates")}</th>
            <th>{t("adminBookings.headcount")}</th>
            <th />
          </tr>
        </thead>
        <tbody>
          {/* A booking renders as two sibling rows, so the key belongs on the
              fragment holding them — not on the rows inside it, which are not a
              list of their own. */}
          {bookings.map((booking) => (
            <Fragment key={booking.id}>
              <tr
                className={booking.finalOverdue ? "overdue" : ""}
                onClick={() => setExpanded(expanded === booking.id ? null : booking.id)}
              >
                <td>{booking.organizationName}</td>
                <td>
                  {formatDate(booking.startDate)} –{" "}
                  {formatDate(booking.endDate)}
                </td>
                <td>{booking.headcount}</td>
                <td className="row-actions">
                  {/* A worker reads the status; setting it and cancelling are writes. */}
                  {canEdit ? (
                    <select
                      value={booking.status}
                      aria-label={t("adminBookings.status")}
                      onClick={(e) => e.stopPropagation()}
                      onChange={(e) => {
                        e.stopPropagation();
                        void handleStatusChange(booking.id, e.target.value as BookingStatus);
                      }}
                    >
                      {bookingStatuses.map((status) => (
                        <option key={status} value={status}>
                          {t(`adminBookings.statuses.${status}`)}
                        </option>
                      ))}
                    </select>
                  ) : (
                    t(`adminBookings.statuses.${booking.status}`)
                  )}
                  {canEdit &&
                    (booking.status === "PendingDeposit" ||
                      booking.status === "Confirmed") && (
                    <button
                      type="button"
                      onClick={(e) => {
                        e.stopPropagation();
                        setCancelTarget(booking);
                      }}
                    >
                      {t("adminBookings.cancel")}
                    </button>
                  )}
                </td>
              </tr>
              {expanded === booking.id && (
                <tr className="booking-details">
                  <td colSpan={4}>
                    <p>
                      {booking.contactName} · {booking.email} · {booking.phone}
                    </p>
                    <p>{t("adminBookings.nights", { count: booking.nights })}</p>
                    <p>
                      {t("adminBookings.rooms")}:{" "}
                      {booking.assignments
                        .map((a) => `${a.roomNumber} (${a.peopleCount}/${a.capacity})`)
                        .join(", ")}
                    </p>
                    {booking.notes && <p>{booking.notes}</p>}
                  </td>
                </tr>
              )}
            </Fragment>
          ))}
        </tbody>
      </table>

      {cancelTarget && (
        <ConfirmDialog
          title={t("adminBookings.cancelTitle")}
          message={t("adminBookings.cancelMessage", {
            organization: cancelTarget.organizationName,
          })}
          confirmLabel={t("adminBookings.cancelConfirm")}
          cancelLabel={t("adminBookings.cancelKeep")}
          onConfirm={() => void confirmCancel()}
          onCancel={() => setCancelTarget(null)}
        />
      )}
    </AdminLayout>
  );
}
