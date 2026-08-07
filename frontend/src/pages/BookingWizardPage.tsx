import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import PublicHeader from "../components/PublicHeader";
import PublicFooter from "../components/PublicFooter";
import RangeCalendar from "../components/calendar/RangeCalendar";
import MixEditor from "../components/MixEditor";
import { formatZl } from "../api/admin";
import {
  createBooking,
  getAvailability,
  getPublicPricing,
  validateSplitMix,
  type Availability,
  type PublicPricing,
} from "../api/public";
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

const STEPS: Step[] = ["dates", "rooms", "contact", "summary"];

export default function BookingWizardPage() {
  const { t, i18n } = useTranslation();
  const navigate = useNavigate();

  const [step, setStep] = useState<Step>("dates");
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  // Campers and supervisors are asked for separately because the kadra sleep in
  // rooms of their own: the centre has to be able to seat them apart, and that is
  // part of what the availability check answers.
  const [camperInput, setCamperInput] = useState("");
  const [supervisorInput, setSupervisorInput] = useState("");
  const [headcount, setHeadcount] = useState(0);
  const [supervisors, setSupervisors] = useState(0);
  const [availability, setAvailability] = useState<Availability | null>(null);
  const [counts, setCounts] = useState<Record<string, number>>({});
  const [supervisorCounts, setSupervisorCounts] = useState<Record<string, number>>({});
  const [contact, setContact] = useState<ContactForm>(emptyContact);
  const [error, setError] = useState<string | null>(null);
  const [busy, setBusy] = useState(false);
  // The centre's rates, asked for on arrival so the page can quote a price
  // before any dates exist — availability only answers for a concrete stay.
  const [rates, setRates] = useState<PublicPricing | null>(null);

  useEffect(() => {
    let cancelled = false;
    void getPublicPricing()
      .then((data) => {
        if (!cancelled) setRates(data);
      })
      // A missing quote is not worth an error banner over: the booking still
      // works, and the real total arrives with availability a step later.
      .catch(() => undefined);
    return () => {
      cancelled = true;
    };
  }, []);

  const formatDate = (iso: string) => formatIsoDate(iso, i18n.language);

  const campersTyped = Number(camperInput) || 0;
  const supervisorsTyped = Number(supervisorInput) || 0;
  const headcountTyped = campersTyped + supervisorsTyped;

  const datesValid =
    startDate !== "" && endDate !== "" && endDate > startDate && headcountTyped >= 1;

  async function submitDates() {
    if (!datesValid) return;
    setBusy(true);
    setError(null);
    try {
      const result = await getAvailability(startDate, endDate, headcountTyped, supervisorsTyped);
      setHeadcount(headcountTyped);
      setSupervisors(supervisorsTyped);
      setAvailability(result);
      if (result.centerClosed) {
        setError(t("wizard.centerClosed", { reason: result.centerClosedReason ?? "" }));
      } else if (!result.fits) {
        // A group that fits the centre may still not fit it with the kadra housed
        // separately, which is a different thing to tell them.
        setError(
          supervisorsTyped > 0 ? t("wizard.supervisorsDoNotFit") : t("wizard.doesNotFitRange"),
        );
      } else {
        setCounts(result.suggestedMix ?? {});
        setSupervisorCounts(result.suggestedSupervisorMix ?? {});
        setStep("rooms");
      }
    } catch {
      setError(t("wizard.loadError"));
    } finally {
      setBusy(false);
    }
  }

  const campers = headcount - supervisors;
  const mixState = availability
    ? validateSplitMix(
        campers,
        supervisors,
        counts,
        supervisorCounts,
        availability.freeRoomsByCapacity,
      )
    : "ok";
  const bedsIn = (mix: Record<string, number>) =>
    Object.entries(mix).reduce((sum, [cap, count]) => sum + Number(cap) * count, 0);
  const totalBeds = bedsIn(counts) + bedsIn(supervisorCounts);

  /** Rooms of a capacity still free for one cohort: the centre's, less whatever
   *  the other cohort has already taken. Keeps the two from claiming one room. */
  function freeFor(capacity: string, cohort: "campers" | "supervisors"): number {
    const free = availability?.freeRoomsByCapacity[capacity] ?? 0;
    const takenByOther =
      cohort === "campers" ? (supervisorCounts[capacity] ?? 0) : (counts[capacity] ?? 0);
    return free - takenByOther;
  }

  function adjustCount(capacity: string, delta: number, cohort: "campers" | "supervisors") {
    const setter = cohort === "campers" ? setCounts : setSupervisorCounts;
    setter((prev) => ({
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
        supervisorCount: supervisors,
        roomCounts: Object.fromEntries(Object.entries(counts).filter(([, v]) => v > 0)),
        supervisorRoomCounts: Object.fromEntries(
          Object.entries(supervisorCounts).filter(([, v]) => v > 0),
        ),
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

  /** "2 × 4-os., 1 × 3-os." — a chosen mix, largest rooms first. */
  const roomLines = (mix: Record<string, number>) =>
    Object.entries(mix)
      .filter(([, v]) => v > 0)
      .sort(([a], [b]) => Number(b) - Number(a))
      .map(([cap, count]) => t("wizard.roomLine", { count, capacity: cap }))
      .join(", ");

  const currentIndex = STEPS.indexOf(step);
  // One rate when the kadra are charged the same as the children, two when they
  // are not — repeating an identical figure under two labels reads as a mistake.
  const ratesDiffer =
    rates !== null &&
    rates.supervisorPricePerPersonPerNightGrosze !== rates.pricePerPersonPerNightGrosze;

  return (
    <div className="home booking-page">
      <PublicHeader variant="sub" />

      <main className="booking-main">
        <div className="booking-intro">
          <h1>{t("wizard.pageTitle")}</h1>
          <p>{t("wizard.pageLead")}</p>

          {/* The price, before anything has been chosen. Everything else on this
              page asks the visitor for something; this is the page answering
              first. */}
          {rates && (
            <ul className="rate-chips">
              <li>
                <strong>{formatZl(rates.pricePerPersonPerNightGrosze)}</strong>
                <span>{ratesDiffer ? t("wizard.ratePerCamper") : t("wizard.ratePerPerson")}</span>
              </li>
              {ratesDiffer && (
                <li>
                  <strong>{formatZl(rates.supervisorPricePerPersonPerNightGrosze)}</strong>
                  <span>{t("wizard.ratePerSupervisor")}</span>
                </li>
              )}
              <li>
                <strong>{formatZl(rates.depositPerPersonPerNightGrosze)}</strong>
                <span>{t("wizard.rateDeposit")}</span>
              </li>
            </ul>
          )}
        </div>

        <ol className="wizard-steps" aria-label={t("wizard.stepsLabel")}>
          {STEPS.map((s, i) => {
            const state = i === currentIndex ? "current" : i < currentIndex ? "done" : "upcoming";
            return (
              <li key={s} className={state} aria-current={state === "current" ? "step" : undefined}>
                <span className="wizard-step-num" aria-hidden="true">
                  {i + 1}
                </span>
                <span className="wizard-step-label">{t(`wizard.steps.${s}`)}</span>
              </li>
            );
          })}
        </ol>

        {error && (
          <p className="booking-alert" role="alert">
            {error}
          </p>
        )}

        <section className="wizard-panel" key={step}>
          {step === "dates" && (
            <>
              <h2>{t("wizard.datesTitle")}</h2>
              {/* Headcount first: how many beds a night must have free is what
                  decides which days the calendar can offer, so asking for it after
                  the dates would grey out the wrong ones. */}
              <div className="booking-counts">
                <label>
                  {t("wizard.campersLabel")}
                  <input
                    type="number"
                    min={0}
                    max={2000}
                    value={camperInput}
                    onChange={(e) => setCamperInput(e.target.value)}
                  />
                </label>
                <label>
                  {t("wizard.supervisorsLabel")}
                  <input
                    type="number"
                    min={0}
                    max={2000}
                    value={supervisorInput}
                    onChange={(e) => setSupervisorInput(e.target.value)}
                  />
                </label>
                {headcountTyped > 0 && (
                  <p className="booking-count-total">
                    {t("wizard.headcountTotal", { count: headcountTyped })}
                  </p>
                )}
              </div>

              <RangeCalendar
                startDate={startDate}
                endDate={endDate}
                headcount={headcountTyped}
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

              <div className="wizard-nav end">
                <button
                  type="button"
                  className="btn-primary"
                  disabled={busy || !datesValid}
                  onClick={() => void submitDates()}
                >
                  {busy ? t("common.loading") : t("wizard.checkAvailability")}
                </button>
              </div>
            </>
          )}

          {step === "rooms" && availability && (
            <>
              <h2>{t("wizard.roomsTitle")}</h2>
              <p className="wizard-stay">
                {formatDate(availability.startDate)} – {formatDate(availability.endDate)} ·{" "}
                {t("wizard.nights", { count: availability.nights })} ·{" "}
                {t("wizard.headcountTotal", { count: headcount })}
              </p>
              <p className="wizard-hint">{t("wizard.roomsHint", { headcount })}</p>

              {/* Two editors when the group brings kadra, one when it does not. Each
                  stepper caps at what the other cohort has left free, so the two can
                  never claim the same room. */}
              {supervisors > 0 && (
                <>
                  <h3 className="mix-heading">
                    {t("wizard.supervisorRooms", { count: supervisors })}
                  </h3>
                  <MixEditor
                    capacities={Object.keys(availability.freeRoomsByCapacity)}
                    counts={supervisorCounts}
                    freeFor={(capacity) => freeFor(capacity, "supervisors")}
                    onAdjust={(capacity, delta) => adjustCount(capacity, delta, "supervisors")}
                  />
                  <h3 className="mix-heading">{t("wizard.camperRooms", { count: campers })}</h3>
                </>
              )}
              <MixEditor
                capacities={Object.keys(availability.freeRoomsByCapacity)}
                counts={counts}
                freeFor={(capacity) => freeFor(capacity, "campers")}
                onAdjust={(capacity, delta) => adjustCount(capacity, delta, "campers")}
              />
              <p className={mixState === "ok" ? "mix-status ok" : "mix-status bad"}>
                {t(`wizard.mix.${mixState}`, { beds: totalBeds, headcount })}
              </p>

              <PriceSummary availability={availability} />

              <div className="wizard-nav">
                <button type="button" className="secondary" onClick={() => setStep("dates")}>
                  {t("wizard.back")}
                </button>
                <button
                  type="button"
                  className="btn-primary"
                  disabled={mixState !== "ok"}
                  onClick={() => setStep("contact")}
                >
                  {t("wizard.next")}
                </button>
              </div>
            </>
          )}

          {step === "contact" && (
            <>
              <h2>{t("wizard.contactTitle")}</h2>
              <div className="booking-form">
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
                <label className="booking-form-wide">
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
                <button type="button" className="secondary" onClick={() => setStep("rooms")}>
                  {t("wizard.back")}
                </button>
                <button
                  type="button"
                  className="btn-primary"
                  disabled={!contactValid}
                  onClick={() => setStep("summary")}
                >
                  {t("wizard.next")}
                </button>
              </div>
            </>
          )}

          {step === "summary" && availability && (
            <>
              <h2>{t("wizard.summaryTitle")}</h2>
              <dl className="summary-list">
                <dt>{t("wizard.summaryDates")}</dt>
                <dd>
                  {formatDate(availability.startDate)} – {formatDate(availability.endDate)} (
                  {t("wizard.nights", { count: availability.nights })})
                </dd>
                <dt>{t("wizard.summaryHeadcount")}</dt>
                <dd>
                  {supervisors > 0
                    ? t("wizard.summaryHeadcountSplit", { campers, supervisors, count: headcount })
                    : headcount}
                </dd>
                <dt>
                  {supervisors > 0 ? t("wizard.summaryCamperRooms") : t("wizard.summaryRooms")}
                </dt>
                <dd>{roomLines(counts)}</dd>
                {/* The kadra's rooms are chosen separately, so listing only the
                    children's would quietly under-report what was booked. */}
                {supervisors > 0 && (
                  <>
                    <dt>{t("wizard.summarySupervisorRooms")}</dt>
                    <dd>{roomLines(supervisorCounts)}</dd>
                  </>
                )}
                <dt>{t("wizard.summaryContact")}</dt>
                <dd>
                  {contact.organizationName}, {contact.contactName}, {contact.email},{" "}
                  {contact.phone}
                </dd>
              </dl>

              <PriceSummary availability={availability} />

              <p className="wizard-hint">{t("wizard.summaryNote")}</p>
              <div className="wizard-nav">
                <button type="button" className="secondary" onClick={() => setStep("contact")}>
                  {t("wizard.back")}
                </button>
                {/* The one amber call to action in the flow, as on the landing
                    page: the moment the booking actually happens. */}
                <button
                  type="button"
                  className="cta-amber"
                  disabled={busy}
                  onClick={() => void submitBooking()}
                >
                  {busy ? t("wizard.submitting") : t("wizard.submit")}
                </button>
              </div>
            </>
          )}
        </section>
      </main>

      <PublicFooter />
    </div>
  );
}

/** What the stay costs, once a real date range and group are known. */
function PriceSummary({ availability }: { availability: Availability }) {
  const { t } = useTranslation();

  return (
    <div className="booking-price">
      <div className="booking-price-total">
        <span>{t("wizard.summaryTotal")}</span>
        <strong>{formatZl(availability.totalGrosze ?? 0)}</strong>
      </div>
      <div className="booking-price-deposit">
        <span>{t("wizard.summaryDeposit")}</span>
        <strong>{formatZl(availability.depositGrosze ?? 0)}</strong>
      </div>
    </div>
  );
}
