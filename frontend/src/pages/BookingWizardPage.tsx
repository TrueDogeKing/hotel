import { useMemo, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import LanguageSwitcher from "../components/LanguageSwitcher";
import { formatZl } from "../api/admin";
import { createBooking, getPublicSessions, validateMix, type PublicSession } from "../api/public";

type Step = "headcount" | "session" | "rooms" | "contact" | "summary";

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

  const [step, setStep] = useState<Step>("headcount");
  const [headcountInput, setHeadcountInput] = useState("");
  const [headcount, setHeadcount] = useState(0);
  const [sessions, setSessions] = useState<PublicSession[]>([]);
  const [session, setSession] = useState<PublicSession | null>(null);
  const [counts, setCounts] = useState<Record<string, number>>({});
  const [contact, setContact] = useState<ContactForm>(emptyContact);
  const [error, setError] = useState<string | null>(null);
  const [busy, setBusy] = useState(false);

  const dateFormatter = useMemo(
    () =>
      new Intl.DateTimeFormat(i18n.language === "en" ? "en-GB" : "pl-PL", {
        dateStyle: "medium",
      }),
    [i18n.language],
  );
  const formatDate = (iso: string) => dateFormatter.format(new Date(iso));

  async function submitHeadcount() {
    const value = Number(headcountInput);
    if (!Number.isInteger(value) || value < 1) return;
    setBusy(true);
    setError(null);
    try {
      setSessions(await getPublicSessions(value));
      setHeadcount(value);
      setStep("session");
    } catch {
      setError(t("wizard.loadError"));
    } finally {
      setBusy(false);
    }
  }

  function pickSession(s: PublicSession) {
    if (!s.fits) return;
    setSession(s);
    setCounts(s.suggestedMix ?? {});
    setStep("rooms");
  }

  const mixState = session ? validateMix(headcount, counts, session.freeRoomsByCapacity) : "ok";
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
    if (!session) return;
    setBusy(true);
    setError(null);
    try {
      const result = await createBooking({
        campSessionId: session.id,
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

  const steps: Step[] = ["headcount", "session", "rooms", "contact", "summary"];

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

        {step === "headcount" && (
          <div className="wizard-panel">
            <h1>{t("wizard.headcountTitle")}</h1>
            <input
              type="number"
              min={1}
              max={2000}
              value={headcountInput}
              onChange={(e) => setHeadcountInput(e.target.value)}
              autoFocus
            />
            <button
              type="button"
              disabled={busy || !headcountInput}
              onClick={() => void submitHeadcount()}
            >
              {t("wizard.next")}
            </button>
          </div>
        )}

        {step === "session" && (
          <div className="wizard-panel">
            <h1>{t("wizard.sessionTitle", { headcount })}</h1>
            {sessions.length === 0 && <p>{t("wizard.noSessions")}</p>}
            <div className="session-cards">
              {sessions.map((s) => (
                <button
                  key={s.id}
                  type="button"
                  className={`session-card${s.fits ? "" : " unavailable"}`}
                  disabled={!s.fits}
                  onClick={() => pickSession(s)}
                >
                  <strong>{s.name}</strong>
                  <span>
                    {formatDate(s.startDate)} – {formatDate(s.endDate)}
                  </span>
                  <span>
                    {t("wizard.pricePerPerson")}: {formatZl(s.pricePerPersonGrosze)}
                  </span>
                  <span>
                    {s.fits
                      ? t("wizard.freeBeds", { count: s.remainingCapacity })
                      : t("wizard.doesNotFit")}
                  </span>
                </button>
              ))}
            </div>
            <button type="button" onClick={() => setStep("headcount")}>
              {t("wizard.back")}
            </button>
          </div>
        )}

        {step === "rooms" && session && (
          <div className="wizard-panel">
            <h1>{t("wizard.roomsTitle")}</h1>
            <p>{t("wizard.roomsHint", { headcount })}</p>
            <div className="mix-editor">
              {Object.entries(session.freeRoomsByCapacity)
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
              <button type="button" onClick={() => setStep("session")}>
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

        {step === "summary" && session && (
          <div className="wizard-panel">
            <h1>{t("wizard.summaryTitle")}</h1>
            <dl className="summary-list">
              <dt>{t("wizard.summarySession")}</dt>
              <dd>
                {session.name} ({formatDate(session.startDate)} – {formatDate(session.endDate)})
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
              <dd>{formatZl(session.pricePerPersonGrosze * headcount)}</dd>
              <dt>{t("wizard.summaryDeposit")}</dt>
              <dd>{formatZl(session.depositPerPersonGrosze * headcount)}</dd>
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
