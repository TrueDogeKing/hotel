import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { isAxiosError } from "axios";
import {
  formatZl,
  getPricingDefaults,
  groszeToZl,
  updatePricingDefaults,
  zlToGrosze,
  type PricingDefaults,
} from "../../api/admin";
import { useAuth } from "../../auth/AuthContext";

/**
 * The centre's current rates, sitting above the bookings they price.
 *
 * They are defaults only: a new group is created at these amounts and then keeps
 * its own copy, so raising the rate here never changes what a group already on
 * the books owes. It lives on the bookings page rather than behind a tile of its
 * own — it is two numbers, and this is where the owner is when they matter.
 */
export default function PricingDefaultsPanel() {
  const { t } = useTranslation();
  const { canEdit } = useAuth();
  const [defaults, setDefaults] = useState<PricingDefaults | null>(null);
  const [open, setOpen] = useState(false);
  const [price, setPrice] = useState("");
  const [supervisorPrice, setSupervisorPrice] = useState("");
  const [deposit, setDeposit] = useState("");
  const [error, setError] = useState<string | null>(null);
  const [saved, setSaved] = useState(false);

  useEffect(() => {
    let cancelled = false;
    void getPricingDefaults().then((data) => {
      if (!cancelled) setDefaults(data);
    });
    return () => {
      cancelled = true;
    };
  }, []);

  function startEdit() {
    if (!defaults) return;
    setPrice(groszeToZl(defaults.pricePerPersonPerNightGrosze));
    setSupervisorPrice(groszeToZl(defaults.supervisorPricePerPersonPerNightGrosze));
    setDeposit(groszeToZl(defaults.depositPerPersonPerNightGrosze));
    setError(null);
    setSaved(false);
    setOpen(true);
  }

  async function save(e: React.FormEvent) {
    e.preventDefault();
    setError(null);
    try {
      setDefaults(
        await updatePricingDefaults({
          pricePerPersonPerNightGrosze: zlToGrosze(price),
          supervisorPricePerPersonPerNightGrosze: zlToGrosze(supervisorPrice),
          depositPerPersonPerNightGrosze: zlToGrosze(deposit),
        }),
      );
      setOpen(false);
      setSaved(true);
    } catch (err) {
      const detail =
        isAxiosError(err) && err.response
          ? (err.response.data as { detail?: string } | undefined)?.detail
          : undefined;
      setError(detail ?? t("pricing.saveError"));
    }
  }

  if (!defaults) return null;

  return (
    <section className="pricing-defaults">
      <h2>{t("pricing.title")}</h2>
      <p>
        {t("pricing.current", {
          price: formatZl(defaults.pricePerPersonPerNightGrosze),
          supervisorPrice: formatZl(defaults.supervisorPricePerPersonPerNightGrosze),
          deposit: formatZl(defaults.depositPerPersonPerNightGrosze),
        })}{" "}
        {canEdit && !open && (
          <button type="button" onClick={startEdit}>
            {t("pricing.edit")}
          </button>
        )}
      </p>
      <p className="pricing-hint">{t("pricing.hint")}</p>
      {saved && <p role="status">{t("pricing.saved")}</p>}

      {open && (
        <form className="admin-form" onSubmit={(e) => void save(e)}>
          <label>
            {t("pricing.pricePerPerson")}
            <input
              type="text"
              inputMode="decimal"
              value={price}
              onChange={(e) => setPrice(e.target.value)}
            />
          </label>
          <label>
            {t("pricing.supervisorPricePerPerson")}
            <input
              type="text"
              inputMode="decimal"
              value={supervisorPrice}
              onChange={(e) => setSupervisorPrice(e.target.value)}
            />
          </label>
          <label>
            {t("pricing.depositPerPerson")}
            <input
              type="text"
              inputMode="decimal"
              value={deposit}
              onChange={(e) => setDeposit(e.target.value)}
            />
          </label>
          <button type="submit">{t("pricing.save")}</button>
          <button type="button" onClick={() => setOpen(false)}>
            {t("pricing.cancel")}
          </button>
        </form>
      )}
      {error && <p role="alert">{error}</p>}
    </section>
  );
}
