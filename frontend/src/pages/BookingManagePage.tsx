import { useEffect, useMemo, useState } from "react";
import { Link, useLocation, useParams } from "react-router-dom";
import { useTranslation } from "react-i18next";
import LanguageSwitcher from "../components/LanguageSwitcher";
import ConfirmDialog from "../components/ConfirmDialog";
import { formatZl } from "../api/admin";
import { cancelBooking, getBooking, initiatePayment, type BookingDetails } from "../api/public";
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
  const [paying, setPaying] = useState(false);

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

  async function pay(kind: "Deposit" | "Final") {
    if (!token) return;
    setError(null);
    setPaying(true);
    try {
      const { redirectUrl } = await initiatePayment(token, kind);
      window.location.href = redirectUrl;
    } catch {
      setError(t("manage.paymentError"));
      setPaying(false);
    }
  }

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
    <main className="public-page">
      <header className="public-header">
        <Link className="auth-brand" to="/">
          <span className="mark">C</span> {t("common.appName")}
        </Link>
        <LanguageSwitcher />
      </header>

      <section className="manage">
        {notFound && <p role="alert">{t("manage.notFound")}</p>}
        {!booking && !notFound && <p>{t("common.loading")}</p>}

        {booking && (
          <>
            {justCreated && <p className="manage-banner">{t("manage.created")}</p>}
            <h1>
              {formatDate(booking.startDate)} –{" "}
              {formatDate(booking.endDate)}
            </h1>
            <p className={`status-badge status-${booking.status.toLowerCase()}`}>
              {t(`manage.statuses.${booking.status}`)}
            </p>

            <dl className="summary-list">
              <dt>{t("manage.dates")}</dt>
              <dd>
                {formatDate(booking.startDate)} –{" "}
                {formatDate(booking.endDate)}
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
              <dt>{t("manage.total")}</dt>
              <dd>{formatZl(booking.totalGrosze)}</dd>
              <dt>{t("manage.deposit")}</dt>
              <dd>{formatZl(booking.depositGrosze)}</dd>
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

            {booking.payments.length > 0 && (
              <>
                <h2>{t("manage.payments")}</h2>
                <ul>
                  {booking.payments.map((p) => (
                    <li key={p.id}>
                      {t(`manage.paymentKinds.${p.kind}`)} — {formatZl(p.amountGrosze)} —{" "}
                      {t(`manage.paymentStatuses.${p.status}`)}
                    </li>
                  ))}
                </ul>
              </>
            )}

            {error && <p role="alert">{error}</p>}

            {booking.status === "PendingDeposit" && (
              <div className="manage-actions">
                <button
                  type="button"
                  className="pay-button"
                  disabled={paying}
                  onClick={() => void pay("Deposit")}
                >
                  {paying ? t("manage.redirecting") : t("manage.payDeposit")}
                </button>
                <button type="button" onClick={() => setConfirmCancel(true)}>
                  {t("manage.cancel")}
                </button>
              </div>
            )}

            {/* The camp programme only exists once the booking is confirmed. */}
            {token && (booking.status === "Confirmed" || booking.status === "Completed") && (
              <BookingSchedule token={token} />
            )}

            {booking.status === "Confirmed" &&
              !booking.payments.some((p) => p.kind === "Final" && p.status === "Completed") && (
                <div className="manage-actions">
                  <button
                    type="button"
                    className="pay-button"
                    disabled={paying}
                    onClick={() => void pay("Final")}
                  >
                    {paying ? t("manage.redirecting") : t("manage.payFinal")}
                  </button>
                </div>
              )}
          </>
        )}
      </section>

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
    </main>
  );
}
