import { useEffect, useState } from "react";
import { Link, useSearchParams } from "react-router-dom";
import { useTranslation } from "react-i18next";
import LanguageSwitcher from "../components/LanguageSwitcher";
import { getBooking, type BookingDetails } from "../api/public";

const POLL_INTERVAL_MS = 3000;
const POLL_TIMEOUT_MS = 60000;

// Return page after the P24 redirect. The webhook is the source of truth, so we
// poll the booking until the payment shows Completed (or give up and point the
// user at the manage page).
export default function PaymentReturnPage() {
  const { t } = useTranslation();
  const [params] = useSearchParams();
  const token = params.get("token");
  const [state, setState] = useState<"waiting" | "paid" | "timeout" | "error">(
    token ? "waiting" : "error",
  );
  const [booking, setBooking] = useState<BookingDetails | null>(null);

  useEffect(() => {
    if (!token) return;

    let cancelled = false;
    const startedAt = Date.now();

    async function poll() {
      try {
        const data = await getBooking(token!);
        if (cancelled) return;
        setBooking(data);
        if (data.payments.some((p) => p.status === "Completed")) {
          setState("paid");
          return;
        }
      } catch {
        if (!cancelled) setState("error");
        return;
      }

      if (Date.now() - startedAt > POLL_TIMEOUT_MS) {
        if (!cancelled) setState("timeout");
        return;
      }

      setTimeout(() => void poll(), POLL_INTERVAL_MS);
    }

    void poll();
    return () => {
      cancelled = true;
    };
  }, [token]);

  return (
    <main className="public-page">
      <header className="public-header">
        <Link className="auth-brand" to="/">
          <span className="mark">C</span> {t("common.appName")}
        </Link>
        <LanguageSwitcher />
      </header>

      <section className="manage">
        {state === "waiting" && <p>{t("paymentReturn.waiting")}</p>}
        {state === "paid" && (
          <>
            <h1>{t("paymentReturn.paidTitle")}</h1>
            <p>{t("paymentReturn.paidBody")}</p>
          </>
        )}
        {state === "timeout" && <p>{t("paymentReturn.timeout")}</p>}
        {state === "error" && <p role="alert">{t("paymentReturn.error")}</p>}

        {token && booking && (
          <p>
            <Link to={`/rezerwacja/${token}`}>{t("paymentReturn.backToBooking")}</Link>
          </p>
        )}
      </section>
    </main>
  );
}
