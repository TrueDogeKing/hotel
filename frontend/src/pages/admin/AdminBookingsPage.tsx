import { Fragment, useCallback, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import AdminLayout from "../../components/admin/AdminLayout";
import { useAuth } from "../../auth/AuthContext";
import ConfirmDialog from "../../components/ConfirmDialog";
import {
  bookingStates,
  formatZl,
  getAdminBookings,
  groszeToZl,
  setBookingState,
  updateBookingPricing,
  zlToGrosze,
  type AdminBooking,
  type BookingState,
} from "../../api/admin";
import PricingDefaultsPanel from "../../components/admin/PricingDefaultsPanel";
import { formatDate as formatIsoDate } from "../../utils/dates";

export default function AdminBookingsPage() {
  const { t, i18n } = useTranslation();
  const { canEdit } = useAuth();
  const [bookings, setBookings] = useState<AdminBooking[]>([]);
  const [stateFilter, setStateFilter] = useState<BookingState | "">("");
  const [expanded, setExpanded] = useState<string | null>(null);
  const [cancelTarget, setCancelTarget] = useState<AdminBooking | null>(null);
  const [error, setError] = useState<string | null>(null);
  // Price editing happens inside the expanded row, in złote — grosze are an
  // storage detail the owner should never have to count in.
  const [priceEditId, setPriceEditId] = useState<string | null>(null);
  const [priceRate, setPriceRate] = useState("");
  const [priceTotal, setPriceTotal] = useState("");
  const [priceDeposit, setPriceDeposit] = useState("");

  // The whole list comes down in one call, so the filter is applied here rather
  // than by the server: the merged state has no single status to ask it for.
  const reload = useCallback(async () => {
    setBookings(await getAdminBookings({}));
  }, []);

  useEffect(() => {
    let cancelled = false;
    void getAdminBookings({}).then((data) => {
      if (!cancelled) setBookings(data);
    });
    return () => {
      cancelled = true;
    };
  }, []);

  const shown = stateFilter ? bookings.filter((b) => b.state === stateFilter) : bookings;

  async function confirmCancel() {
    if (!cancelTarget) return;
    const target = cancelTarget;
    setCancelTarget(null);
    await handleStateChange(target.id, "Cancelled");
  }

  // The one control on a booking: what has been paid, or that the stay is
  // cancelled or over. Reviving a cancelled booking is only reachable from here —
  // the dashboard and the calendar both list live bookings only.
  async function handleStateChange(id: string, state: BookingState) {
    setError(null);
    try {
      await setBookingState(id, state);
      await reload();
    } catch (err) {
      if (isAxiosError(err) && err.response) {
        const detail = (err.response.data as { detail?: string } | undefined)?.detail;
        setError(detail ?? t("adminBookings.stateError"));
      } else {
        setError(t("adminBookings.stateError"));
      }
    }
  }

  function startPriceEdit(booking: AdminBooking) {
    setPriceEditId(booking.id);
    setPriceRate(groszeToZl(booking.pricePerPersonPerNightGrosze));
    setPriceTotal(groszeToZl(booking.totalGrosze));
    setPriceDeposit(groszeToZl(booking.depositGrosze));
  }

  /** The total follows the rate as it is typed — it is rate × people × nights —
   *  but stays editable, so a negotiated flat price can be written over it. */
  function handleRateChange(booking: AdminBooking, value: string) {
    setPriceRate(value);
    const rate = zlToGrosze(value);
    if (!Number.isNaN(rate)) {
      setPriceTotal(groszeToZl(rate * booking.headcount * booking.nights));
    }
  }

  async function savePrice(e: React.FormEvent, booking: AdminBooking) {
    e.preventDefault();
    setError(null);
    try {
      await updateBookingPricing(booking.id, {
        pricePerPersonPerNightGrosze: zlToGrosze(priceRate),
        depositGrosze: zlToGrosze(priceDeposit),
        totalGrosze: zlToGrosze(priceTotal),
      });
      setPriceEditId(null);
      await reload();
    } catch (err) {
      const detail =
        isAxiosError(err) && err.response
          ? (err.response.data as { detail?: string } | undefined)?.detail
          : undefined;
      setError(detail ?? t("adminBookings.priceError"));
    }
  }

  const formatDate = (iso: string) => formatIsoDate(iso, i18n.language);

  return (
    <AdminLayout>
      <h1>{t("adminBookings.title")}</h1>

      <PricingDefaultsPanel />

      <div className="admin-form">
        <label>
          {t("adminBookings.filterStatus")}
          <select
            value={stateFilter}
            onChange={(e) => setStateFilter(e.target.value as BookingState | "")}
          >
            <option value="">{t("adminBookings.allStatuses")}</option>
            {bookingStates.map((s) => (
              <option key={s} value={s}>
                {t(`adminBookings.states.${s}`)}
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
            <th>{t("adminBookings.state")}</th>
          </tr>
        </thead>
        <tbody>
          {/* A booking renders as two sibling rows, so the key belongs on the
              fragment holding them — not on the rows inside it, which are not a
              list of their own. */}
          {shown.map((booking) => (
            <Fragment key={booking.id}>
              <tr
                className={booking.finalOverdue ? "overdue" : ""}
                onClick={() => setExpanded(expanded === booking.id ? null : booking.id)}
              >
                <td>{booking.organizationName}</td>
                <td>
                  {formatDate(booking.startDate)} – {formatDate(booking.endDate)}
                </td>
                <td>{booking.headcount}</td>
                <td>{formatZl(booking.totalGrosze)}</td>
                <td>
                  {/* A worker reads the state; changing it is the owner's. Picking
                      "cancelled" frees the rooms and emails the group, so it asks
                      first — everything else applies straight away. */}
                  {canEdit ? (
                    <select
                      value={booking.state}
                      aria-label={t("adminBookings.state")}
                      onClick={(e) => e.stopPropagation()}
                      onChange={(e) => {
                        e.stopPropagation();
                        const next = e.target.value as BookingState;
                        if (next === "Cancelled") {
                          setCancelTarget(booking);
                        } else {
                          void handleStateChange(booking.id, next);
                        }
                      }}
                    >
                      {bookingStates.map((state) => (
                        <option key={state} value={state}>
                          {t(`adminBookings.states.${state}`)}
                        </option>
                      ))}
                    </select>
                  ) : (
                    t(`adminBookings.states.${booking.state}`)
                  )}
                </td>
              </tr>
              {expanded === booking.id && (
                <tr className="booking-details">
                  <td colSpan={5}>
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

                    {priceEditId === booking.id ? (
                      <form
                        className="price-form"
                        onSubmit={(e) => void savePrice(e, booking)}
                        onClick={(e) => e.stopPropagation()}
                      >
                        <label>
                          {t("adminBookings.perPerson")}
                          <input
                            type="text"
                            inputMode="decimal"
                            value={priceRate}
                            onChange={(e) => handleRateChange(booking, e.target.value)}
                          />
                        </label>
                        <label>
                          {t("adminBookings.total")}
                          <input
                            type="text"
                            inputMode="decimal"
                            value={priceTotal}
                            onChange={(e) => setPriceTotal(e.target.value)}
                          />
                        </label>
                        <label>
                          {t("adminBookings.deposit")}
                          <input
                            type="text"
                            inputMode="decimal"
                            value={priceDeposit}
                            onChange={(e) => setPriceDeposit(e.target.value)}
                          />
                        </label>
                        <button type="submit">{t("adminBookings.priceSave")}</button>
                        <button type="button" onClick={() => setPriceEditId(null)}>
                          {t("adminBookings.priceCancel")}
                        </button>
                      </form>
                    ) : (
                      <p className="price-summary">
                        {t("adminBookings.priceLine", {
                          perPerson: formatZl(booking.pricePerPersonPerNightGrosze),
                          people: booking.headcount,
                          nights: booking.nights,
                          total: formatZl(booking.totalGrosze),
                          deposit: formatZl(booking.depositGrosze),
                        })}{" "}
                        {canEdit && (
                          <button
                            type="button"
                            onClick={(e) => {
                              e.stopPropagation();
                              startPriceEdit(booking);
                            }}
                          >
                            {t("adminBookings.priceEdit")}
                          </button>
                        )}
                      </p>
                    )}
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
