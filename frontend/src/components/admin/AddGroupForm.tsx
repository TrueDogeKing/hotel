import { useEffect, useState, type FormEvent } from "react";
import { useTranslation } from "react-i18next";
import {
  bookingStatuses,
  createAdminBooking,
  formatZl,
  getPricingDefaults,
  groszeToZl,
  zlToGrosze,
  type AdminBooking,
  type BookingStatus,
} from "../../api/admin";
import DateRangeField from "../calendar/DateRangeField";

interface Props {
  onCreated: (booking: AdminBooking) => void | Promise<void>;
  onError: (error: unknown) => void;
  onCancel: () => void;
}

function isoInDays(days: number): string {
  const date = new Date();
  date.setDate(date.getDate() + days);
  return date.toISOString().slice(0, 10);
}

function nightsBetween(startDate: string, endDate: string): number {
  const nights = (Date.parse(endDate) - Date.parse(startDate)) / 86_400_000;
  return Number.isFinite(nights) && nights > 0 ? Math.round(nights) : 0;
}

/// Staff-entered group: the rooms are picked automatically from what is free —
/// the supervisors into their own — so this asks only for who is coming, when,
/// and what they are charged.
export default function AddGroupForm({ onCreated, onError, onCancel }: Props) {
  const { t, i18n } = useTranslation();
  const [organizationName, setOrganizationName] = useState("");
  const [contactName, setContactName] = useState("");
  const [email, setEmail] = useState("");
  const [phone, setPhone] = useState("");
  const [startDate, setStartDate] = useState(isoInDays(7));
  const [endDate, setEndDate] = useState(isoInDays(14));
  const [campers, setCampers] = useState("10");
  const [supervisors, setSupervisors] = useState("2");
  const [status, setStatus] = useState<BookingStatus>("Confirmed");
  const [notes, setNotes] = useState("");
  const [saving, setSaving] = useState(false);

  // Prices in złote, prefilled from the centre's rates and editable here so a
  // group can be entered at its agreed price in one go.
  const [camperRate, setCamperRate] = useState("");
  const [supervisorRate, setSupervisorRate] = useState("");
  // Only set once the owner types a total of their own: it is then a negotiated
  // figure, and recomputing it would throw the negotiation away. Null means the
  // total still follows the rates.
  const [pinnedTotal, setPinnedTotal] = useState<string | null>(null);

  const camperCount = Number(campers) || 0;
  const supervisorCount = Number(supervisors) || 0;
  const headcount = camperCount + supervisorCount;
  const nights = nightsBetween(startDate, endDate);

  useEffect(() => {
    let cancelled = false;
    void getPricingDefaults().then((rates) => {
      if (cancelled) return;
      setCamperRate(groszeToZl(rates.pricePerPersonPerNightGrosze));
      setSupervisorRate(groszeToZl(rates.supervisorPricePerPersonPerNightGrosze));
    });
    return () => {
      cancelled = true;
    };
  }, []);

  // Derived, not stored: the total is a function of the two rates, the two
  // counts and the nights, and only stops being one when it is pinned.
  const computedGrosze =
    zlToGrosze(camperRate || "0") * camperCount * nights +
    zlToGrosze(supervisorRate || "0") * supervisorCount * nights;
  const total = pinnedTotal ?? groszeToZl(Number.isNaN(computedGrosze) ? 0 : computedGrosze);

  async function handleSubmit(event: FormEvent) {
    event.preventDefault();
    setSaving(true);
    try {
      const booking = await createAdminBooking({
        organizationName: organizationName.trim(),
        contactName: contactName.trim(),
        email: email.trim(),
        phone: phone.trim(),
        startDate,
        endDate,
        headcount,
        supervisorCount,
        pricePerPersonPerNightGrosze: camperRate === "" ? null : zlToGrosze(camperRate),
        supervisorPricePerPersonPerNightGrosze:
          supervisorRate === "" ? null : zlToGrosze(supervisorRate),
        totalGrosze: total === "" ? null : zlToGrosze(total),
        depositGrosze: null,
        notes: notes.trim() || null,
        status,
        language: i18n.language.startsWith("en") ? "en" : "pl",
      });
      await onCreated(booking);
    } catch (err) {
      onError(err);
    } finally {
      setSaving(false);
    }
  }

  return (
    <form className="modal-form add-group-form" onSubmit={(e) => void handleSubmit(e)}>
      <div className="add-group-row">
        <label>
          {t("dashboard.organization")}
          <input
            value={organizationName}
            onChange={(e) => setOrganizationName(e.target.value)}
            required
            maxLength={256}
          />
        </label>
        <label>
          {t("dashboard.contactName")}
          <input
            value={contactName}
            onChange={(e) => setContactName(e.target.value)}
            required
            maxLength={128}
          />
        </label>
      </div>

      <div className="add-group-row">
        <label>
          {t("dashboard.email")}
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
            maxLength={256}
          />
        </label>
        <label>
          {t("dashboard.phone")}
          <input
            value={phone}
            onChange={(e) => setPhone(e.target.value)}
            required
            maxLength={32}
            pattern="[0-9+\-() ]+"
          />
        </label>
      </div>

      <div className="add-group-row">
        <label>
          {t("dashboard.campers")}
          <input
            type="number"
            min={0}
            max={2000}
            value={campers}
            onChange={(e) => setCampers(e.target.value)}
            required
          />
        </label>
        <label>
          {/* The kadra are housed apart from the children, so they are counted
              apart from them too. */}
          {t("dashboard.supervisors")}
          <input
            type="number"
            min={0}
            max={2000}
            value={supervisors}
            onChange={(e) => setSupervisors(e.target.value)}
            required
          />
        </label>
        <p className="add-group-total">{t("dashboard.headcountTotal", { count: headcount })}</p>
      </div>

      {/* The app's own calendar rather than a native date input, which renders its
          picker in the browser's language and ignores the panel's. Folded behind
          the field so the rest of the form keeps its compact single row. Past
          dates stay pickable: staff backfill groups that already arrived. */}
      <DateRangeField
        label={t("dashboard.stayDates")}
        startDate={startDate}
        endDate={endDate}
        headcount={headcount}
        allowPast
        onChange={(range) => {
          setStartDate(range.startDate);
          setEndDate(range.endDate);
        }}
      />

      <div className="add-group-row">
        <label>
          {t("dashboard.camperRate")}
          <input
            type="text"
            inputMode="decimal"
            value={camperRate}
            onChange={(e) => setCamperRate(e.target.value)}
          />
        </label>
        <label>
          {t("dashboard.supervisorRate")}
          <input
            type="text"
            inputMode="decimal"
            value={supervisorRate}
            onChange={(e) => setSupervisorRate(e.target.value)}
          />
        </label>
        <label>
          {t("dashboard.total")}
          <input
            type="text"
            inputMode="decimal"
            value={total}
            onChange={(e) => setPinnedTotal(e.target.value)}
          />
        </label>
      </div>
      {pinnedTotal !== null && (
        <p className="hk-section-hint">
          {t("dashboard.totalPinned")}{" "}
          <button type="button" className="link-button" onClick={() => setPinnedTotal(null)}>
            {t("dashboard.totalRecompute")}
          </button>
        </p>
      )}
      {pinnedTotal === null && nights > 0 && (
        <p className="hk-section-hint">
          {t("dashboard.priceLine", {
            nights,
            campers: camperCount,
            supervisors: supervisorCount,
            total: formatZl(zlToGrosze(total || "0")),
          })}
        </p>
      )}

      <div className="add-group-row">
        <label>
          {t("dashboard.status")}
          <select value={status} onChange={(e) => setStatus(e.target.value as BookingStatus)}>
            {bookingStatuses.map((option) => (
              <option key={option} value={option}>
                {t(`adminBookings.statuses.${option}`)}
              </option>
            ))}
          </select>
        </label>
        <label className="add-group-notes">
          {t("dashboard.notes")}
          <input value={notes} onChange={(e) => setNotes(e.target.value)} maxLength={2000} />
        </label>
      </div>

      <div className="modal-actions">
        <button type="button" className="secondary" onClick={onCancel}>
          {t("dashboard.addGroupCancel")}
        </button>
        <button type="submit" disabled={saving || headcount < 1}>
          {saving ? t("common.loading") : t("dashboard.addGroupSubmit")}
        </button>
      </div>
    </form>
  );
}
