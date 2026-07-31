import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import LanguageSwitcher from "../components/LanguageSwitcher";
import RangeCalendar from "../components/calendar/RangeCalendar";
import { formatZl } from "../api/admin";
import { createBooking, getAvailability, validateMix, type Availability } from "../api/public";
import { formatDate as formatIsoDate } from "../utils/dates";

type Step = "dates" | "rooms" | "contact" | "summary";

interface ContactForm {
  organizationName: string;
  contactName: string;
  email: string;
  phone: string;
  notes: string;
}

const emptyContact: ContactForm = {
  organizationName: "",
  contactName: "",
  email: "",
  phone: "",
  notes: "",
};

export default function BookingWizardPage() {
  const { t, i18n } = useTranslation();
  const navigate = useNavigate();

  const [step, setStep] = useState<Step>("dates");
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [headcountInput, setHeadcountInput] = useState("");
  const [headcount, setHeadcount] = useState(0);
  const [availability, setAvailability] = useState<Availability | null>(null);
  const [counts, setCounts] = useState<Record<string, number>>({});
  const [contact, setContact] = useState<ContactForm>(emptyContact);
  const [error, setError] = useState<string | null>(null);
  const [busy, setBusy] = useState(false);

  const formatDate = (iso: string) => formatIsoDate(iso, i18n.language);

  const datesValid =
    startDate !== "" && endDate !== "" && endDate > startDate && Number(headcountInput) >= 1;

  async function submitDates() {
    const value = Number(headcountInput);
    if (!datesValid) return;
    setBusy(true);
    setError(null);
    try {
      const result = await getAvailability(startDate, endDate, value);
      setHeadcount(value);
      setAvailability(result);
      if (result.centerClosed) {
        setError(t("wizard.centerClosed", { reason: result.centerClosedReason ?? "" }));
      } else if (!result.fits) {
        setError(t("wizard.doesNotFitRange"));
      } else {
        setCounts(result.suggestedMix ?? {});
        setStep("rooms");
      }
    } catch {
      setError(t("wizard.loadError"));
    } finally {
      setBusy(false);
    }
  }

  const mixState = availability
    ? validateMix(headcount, counts, availability.freeRoomsByCapacity)
    : "ok";
  const totalBeds = Object.entries(counts).reduce(
    (sum, [cap, count]) => sum + Number(cap) * count,
    0,
  );

  function adjustCount(capacity: string, delta: number) {
    setCounts((prev) => ({
      ...prev,
      [capacity]: Math.max(0, (prev[capacity] ?? 0) + delta),
    }));
  }

  const contactValid =
    contact.organizationName.trim() !== "" &&
    contact.contactName.trim() !== "" &&
    /.+@.+\..+/.test(contact.email) &&
    contact.phone.trim() !== "";

  async function submitBooking() {
    if (!availability) return;
    setBusy(true);
    setError(null);
    try {
      const result = await createBooking({
        startDate,
        endDate,
        headcount,
        roomCounts: Object.fromEntries(Object.entries(counts).filter(([, v]) => v > 0)),
        organizationName: contact.organizationName.trim(),
        contactName: contact.contactName.trim(),
        email: contact.email.trim(),
        phone: contact.phone.trim(),
        notes: contact.notes.trim() || null,
      });
      navigate(`/rezerwacja/${result.manageToken}`, { state: { justCreated: true } });
    } catch (err) {
      if (isAxiosError(err) && err.response?.status === 409) {
        setError(t("wizard.roomsTaken"));
      } else if (isAxiosError(err) && err.response?.status === 400) {
        setError(t("wizard.invalidData"));
      } else {
        setError(t("wizard.genericError"));
      }
    } finally {
      setBusy(false);
    }
  }

  const steps: Step[] = ["dates", "rooms", "contact", "summary"];

  return (
    <main className="public-page">
      <header className="public-header">
        <Link className="auth-brand" to="/">
          <span className="mark">C</span> {t("common.appName")}
        </Link>
        <LanguageSwitcher />
      </header>

      <section className="wizard">
        <ol className="wizard-steps">
          {steps.map((s, i) => (
            <li key={s} className={s === step ? "current" : steps.indexOf(step) > i ? "done" : ""}>
              {t(`wizard.steps.${s}`)}
            </li>
          ))}
        </ol>

        {error && <p role="alert">{error}</p>}

        {step === "dates" && (
          <div className="wizard-panel">
            <h1>{t("wizard.datesTitle")}</h1>
            {/* Headcount first: how many beds a night must have free is what
                decides which days the calendar can offer, so asking for it after
                the dates would grey out the wrong ones. */}
            <div className="form">
              <label>
                {t("wizard.headcountLabel")}
                <input
                  type="number"
                  min={1}
                  max={2000}
                  value={headcountInput}
                  onChange={(e) => setHeadcountInput(e.target.value)}
                />
              </label>
            </div>

            <RangeCalendar
              startDate={startDate}
              endDate={endDate}
              headcount={Number(headcountInput) || 0}
              onChange={(range) => {
                setStartDate(range.startDate);
                setEndDate(range.endDate);
                setError(null);
              }}
            />

            {startDate !== "" && endDate !== "" && (
              <p className="wizard-chosen">
                {formatDate(startDate)} – {formatDate(endDate)}
              </p>
            )}

            <button type="button" disabled={busy || !datesValid} onClick={() => void submitDates()}>
              {t("wizard.checkAvailability")}
            </button>
          </div>
        )}

        {step === "rooms" && availability && (
          <div className="wizard-panel">
            <h1>{t("wizard.roomsTitle")}</h1>
            <p>
              {formatDate(availability.startDate)} – {formatDate(availability.endDate)} ·{" "}
              {t("wizard.nights", { count: availability.nights })}
            </p>
            <p>{t("wizard.roomsHint", { headcount })}</p>
            <div className="mix-editor">
              {Object.entries(availability.freeRoomsByCapacity)
                .sort(([a], [b]) => Number(b) - Number(a))
                .map(([capacity, free]) => (
                  <div key={capacity} className="mix-row">
                    <span>{t("wizard.roomType", { capacity })}</span>
                    <span className="mix-free">{t("wizard.freeRooms", { count: free })}</span>
                    <div className="mix-stepper">
                      <button type="button" onClick={() => adjustCount(capacity, -1)}>
                        −
                      </button>
                      <span>{counts[capacity] ?? 0}</span>
                      <button
                        type="button"
                        disabled={(counts[capacity] ?? 0) >= free}
                        onClick={() => adjustCount(capacity, 1)}
                      >
                        +
                      </button>
                    </div>
                  </div>
                ))}
            </div>
            <p className={mixState === "ok" ? "mix-status ok" : "mix-status bad"}>
              {t(`wizard.mix.${mixState}`, { beds: totalBeds, headcount })}
            </p>
            <div className="wizard-nav">
              <button type="button" onClick={() => setStep("dates")}>
                {t("wizard.back")}
              </button>
              <button type="button" disabled={mixState !== "ok"} onClick={() => setStep("contact")}>
                {t("wizard.next")}
              </button>
            </div>
          </div>
        )}

        {step === "contact" && (
          <div className="wizard-panel">
            <h1>{t("wizard.contactTitle")}</h1>
            <div className="form">
              <label>
                {t("wizard.organization")}
                <input
                  value={contact.organizationName}
                  onChange={(e) => setContact({ ...contact, organizationName: e.target.value })}
                  maxLength={256}
                  required
                />
              </label>
              <label>
                {t("wizard.contactName")}
                <input
                  value={contact.contactName}
                  onChange={(e) => setContact({ ...contact, contactName: e.target.value })}
                  maxLength={128}
                  required
                />
              </label>
              <label>
                {t("wizard.email")}
                <input
                  type="email"
                  value={contact.email}
                  onChange={(e) => setContact({ ...contact, email: e.target.value })}
                  maxLength={256}
                  required
                />
              </label>
              <label>
                {t("wizard.phone")}
                <input
                  type="tel"
                  value={contact.phone}
                  onChange={(e) => setContact({ ...contact, phone: e.target.value })}
                  maxLength={32}
                  required
                />
              </label>
              <label>
                {t("wizard.notes")}
                <textarea
                  value={contact.notes}
                  onChange={(e) => setContact({ ...contact, notes: e.target.value })}
                  maxLength={2000}
                  rows={3}
                />
              </label>
            </div>
            <div className="wizard-nav">
              <button type="button" onClick={() => setStep("rooms")}>
                {t("wizard.back")}
              </button>
              <button type="button" disabled={!contactValid} onClick={() => setStep("summary")}>
                {t("wizard.next")}
              </button>
            </div>
          </div>
        )}

        {step === "summary" && availability && (
          <div className="wizard-panel">
            <h1>{t("wizard.summaryTitle")}</h1>
            <dl className="summary-list">
              <dt>{t("wizard.summaryDates")}</dt>
              <dd>
                {formatDate(availability.startDate)} – {formatDate(availability.endDate)} (
                {t("wizard.nights", { count: availability.nights })})
              </dd>
              <dt>{t("wizard.summaryHeadcount")}</dt>
              <dd>{headcount}</dd>
              <dt>{t("wizard.summaryRooms")}</dt>
              <dd>
                {Object.entries(counts)
                  .filter(([, v]) => v > 0)
                  .sort(([a], [b]) => Number(b) - Number(a))
                  .map(([cap, count]) => t("wizard.roomLine", { count, capacity: cap }))
                  .join(", ")}
              </dd>
              <dt>{t("wizard.summaryTotal")}</dt>
              <dd>{formatZl(availability.totalGrosze ?? 0)}</dd>
              <dt>{t("wizard.summaryDeposit")}</dt>
              <dd>{formatZl(availability.depositGrosze ?? 0)}</dd>
              <dt>{t("wizard.summaryContact")}</dt>
              <dd>
                {contact.organizationName}, {contact.contactName}, {contact.email}, {contact.phone}
              </dd>
            </dl>
            <p>{t("wizard.summaryNote")}</p>
            <div className="wizard-nav">
              <button type="button" onClick={() => setStep("contact")}>
                {t("wizard.back")}
              </button>
              <button type="button" disabled={busy} onClick={() => void submitBooking()}>
                {busy ? t("wizard.submitting") : t("wizard.submit")}
              </button>
            </div>
          </div>
        )}
      </section>
    </main>
  );
}
