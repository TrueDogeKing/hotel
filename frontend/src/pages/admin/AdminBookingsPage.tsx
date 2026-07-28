import { useCallback, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import AdminLayout from "../../components/admin/AdminLayout";
import ConfirmDialog from "../../components/ConfirmDialog";
import { cancelAdminBooking, formatZl, getAdminBookings, type AdminBooking } from "../../api/admin";
import { formatDate as formatIsoDate } from "../../utils/dates";

export default function AdminBookingsPage() {
  const { t, i18n } = useTranslation();
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

  const formatDate = (iso: string) => formatIsoDate(iso, i18n.language);

  function paymentBadge(booking: AdminBooking) {
    if (booking.status === "Cancelled") return t(`adminBookings.statuses.${booking.status}`);
    if (booking.finalPaid) return t("adminBookings.fullyPaid");
    if (booking.finalOverdue) return t("adminBookings.finalOverdue");
    if (booking.depositPaid) return t("adminBookings.depositPaidBadge");
    return t("adminBookings.awaitingDeposit");
  }

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
            <th>{t("adminBookings.total")}</th>
            <th>{t("adminBookings.paymentState")}</th>
            <th />
          </tr>
        </thead>
        <tbody>
          {bookings.map((booking) => (
            <>
              <tr
                key={booking.id}
                className={booking.finalOverdue ? "overdue" : ""}
                onClick={() => setExpanded(expanded === booking.id ? null : booking.id)}
              >
                <td>{booking.organizationName}</td>
                <td>
                  {formatDate(booking.startDate)} –{" "}
                  {formatDate(booking.endDate)}
                </td>
                <td>{booking.headcount}</td>
                <td>{formatZl(booking.totalGrosze)}</td>
                <td>{paymentBadge(booking)}</td>
                <td className="row-actions">
                  {(booking.status === "PendingDeposit" || booking.status === "Confirmed") && (
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
                <tr key={`${booking.id}-details`} className="booking-details">
                  <td colSpan={6}>
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
                    <p>
                      {t("adminBookings.deposit")}: {formatZl(booking.depositGrosze)} ·{" "}
                      {t("adminBookings.finalDue")}:{" "}
                      {formatDate(booking.finalPaymentDueDate)}
                    </p>
                    {booking.notes && <p>{booking.notes}</p>}
                  </td>
                </tr>
              )}
            </>
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
