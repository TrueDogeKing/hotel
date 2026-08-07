import { useEffect, useMemo, useState } from "react";
import { useLocation, useParams } from "react-router-dom";
import { useTranslation } from "react-i18next";
import PublicHeader from "../components/PublicHeader";
import PublicFooter from "../components/PublicFooter";
import ConfirmDialog from "../components/ConfirmDialog";
import { formatZl } from "../api/admin";
import { cancelBooking, getBooking, type BookingDetails } from "../api/public";
import { formatDate as formatIsoDate } from "../utils/dates";
import BookingSchedule from "../components/BookingSchedule";

// Booking manage page, reached via the secret link from the confirmation email.
export default function BookingManagePage() {
  const { t, i18n } = useTranslation();
  const { token } = useParams<{ token: string }>();
  const location = useLocation();
  const justCreated = (location.state as { justCreated?: boolean } | null)?.justCreated ?? false;

  const [booking, setBooking] = useState<BookingDetails | null>(null);
  const [notFound, setNotFound] = useState(false);
  const [confirmCancel, setConfirmCancel] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!token) return;
    let cancelled = false;
    getBooking(token)
      .then((data) => {
        if (!cancelled) setBooking(data);
      })
      .catch(() => {
        if (!cancelled) setNotFound(true);
      });
    return () => {
      cancelled = true;
    };
  }, [token]);

  const formatDate = (iso: string) => formatIsoDate(iso, i18n.language);
  // holdExpiresAt is a genuine UTC timestamp, not a date-only string, so it keeps
  // its own formatter and plain `new Date(iso)`.
  const dateTimeFormatter = useMemo(
    () =>
      new Intl.DateTimeFormat(i18n.language === "en" ? "en-GB" : "pl-PL", {
        dateStyle: "medium",
        timeStyle: "short",
      }),
    [i18n.language],
  );

  // Paying online is switched off: the group settles with the centre directly
  // and the owner records it in the panel. See PublicPaymentsController for the
  // commented-out P24 flow this replaced.
  // async function pay(kind: "Deposit" | "Final") { … }

  async function handleCancel() {
    if (!token) return;
    setError(null);
    try {
      await cancelBooking(token);
      setBooking(await getBooking(token));
    } catch {
      setError(t("manage.cancelError"));
    } finally {
      setConfirmCancel(false);
    }
  }

  return (
    <div className="home booking-page">
      <PublicHeader variant="sub" />

      <main className="booking-main">
        <section className="wizard-panel manage">
          {notFound && <p role="alert">{t("manage.notFound")}</p>}
          {!booking && !notFound && <p>{t("common.loading")}</p>}

          {booking && (
            <>
              {justCreated && <p className="manage-banner">{t("manage.created")}</p>}
              <div className="manage-head">
                <h1>
                  {formatDate(booking.startDate)} – {formatDate(booking.endDate)}
                </h1>
                <p className={`status-badge status-${booking.status.toLowerCase()}`}>
                  {t(`manage.statuses.${booking.status}`)}
                </p>
              </div>

              <dl className="summary-list">
                <dt>{t("manage.dates")}</dt>
                <dd>
                  {formatDate(booking.startDate)} – {formatDate(booking.endDate)}
                </dd>
                <dt>{t("manage.organization")}</dt>
                <dd>{booking.organizationName}</dd>
                <dt>{t("manage.headcount")}</dt>
                <dd>{booking.headcount}</dd>
                <dt>{t("manage.rooms")}</dt>
                <dd>
                  {Object.entries(booking.roomCounts)
                    .sort(([a], [b]) => Number(b) - Number(a))
                    .map(([cap, count]) => t("wizard.roomLine", { count, capacity: cap }))
                    .join(", ")}
                </dd>
                {booking.status === "PendingDeposit" && booking.holdExpiresAt && (
                  <>
                    <dt>{t("manage.holdExpires")}</dt>
                    <dd>{dateTimeFormatter.format(new Date(booking.holdExpiresAt))}</dd>
                  </>
                )}
                {(booking.status === "PendingDeposit" || booking.status === "Confirmed") && (
                  <>
                    <dt>{t("manage.finalDue")}</dt>
                    <dd>{formatDate(booking.finalPaymentDueDate)}</dd>
                  </>
                )}
              </dl>

              <div className="booking-price">
                <div className="booking-price-total">
                  <span>{t("manage.total")}</span>
                  <strong>{formatZl(booking.totalGrosze)}</strong>
                </div>
                <div className="booking-price-deposit">
                  <span>{t("manage.deposit")}</span>
                  <strong>{formatZl(booking.depositGrosze)}</strong>
                </div>
              </div>

              <p className="wizard-hint">{t("manage.payOffline")}</p>

              {error && <p role="alert">{error}</p>}

              {booking.status === "PendingDeposit" && (
                <div className="manage-actions">
                  <button type="button" onClick={() => setConfirmCancel(true)}>
                    {t("manage.cancel")}
                  </button>
                </div>
              )}

              {/* The camp programme only exists once the booking is confirmed. */}
              {token && (booking.status === "Confirmed" || booking.status === "Completed") && (
                <BookingSchedule token={token} />
              )}
            </>
          )}
        </section>
      </main>

      <PublicFooter />

      {confirmCancel && (
        <ConfirmDialog
          title={t("manage.cancelTitle")}
          message={t("manage.cancelMessage")}
          confirmLabel={t("manage.cancelConfirm")}
          cancelLabel={t("manage.cancelKeep")}
          onConfirm={() => void handleCancel()}
          onCancel={() => setConfirmCancel(false)}
        />
      )}
    </div>
  );
}
