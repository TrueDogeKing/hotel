import { useState, type FormEvent } from "react";
import { useTranslation } from "react-i18next";
import {
  bookingStatuses,
  createAdminBooking,
  type AdminBooking,
  type BookingStatus,
} from "../../api/admin";

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

/// Staff-entered group: the rooms are picked automatically from what is free, so
/// this asks only for who is coming and when — no room mix, unlike the public wizard.
export default function AddGroupForm({ onCreated, onError, onCancel }: Props) {
  const { t, i18n } = useTranslation();
  const [organizationName, setOrganizationName] = useState("");
  const [contactName, setContactName] = useState("");
  const [email, setEmail] = useState("");
  const [phone, setPhone] = useState("");
  const [startDate, setStartDate] = useState(isoInDays(7));
  const [endDate, setEndDate] = useState(isoInDays(14));
  const [headcount, setHeadcount] = useState("10");
  const [status, setStatus] = useState<BookingStatus>("Confirmed");
  const [notes, setNotes] = useState("");
  const [saving, setSaving] = useState(false);

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
        headcount: Number(headcount),
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
    <form className="admin-form" onSubmit={(e) => void handleSubmit(e)}>
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
      <label>
        {t("dashboard.from")}
        <input
          type="date"
          value={startDate}
          onChange={(e) => setStartDate(e.target.value)}
          required
        />
      </label>
      <label>
        {t("dashboard.to")}
        <input
          type="date"
          value={endDate}
          onChange={(e) => setEndDate(e.target.value)}
          required
        />
      </label>
      <label>
        {t("dashboard.headcount")}
        <input
          type="number"
          min={1}
          max={2000}
          value={headcount}
          onChange={(e) => setHeadcount(e.target.value)}
          required
        />
      </label>
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
      <label>
        {t("dashboard.notes")}
        <input value={notes} onChange={(e) => setNotes(e.target.value)} maxLength={2000} />
      </label>
      <div className="row-actions">
        <button type="submit" disabled={saving}>
          {saving ? t("common.loading") : t("dashboard.addGroupSubmit")}
        </button>
        <button type="button" className="secondary" onClick={onCancel}>
          {t("dashboard.addGroupCancel")}
        </button>
      </div>
    </form>
  );
}
