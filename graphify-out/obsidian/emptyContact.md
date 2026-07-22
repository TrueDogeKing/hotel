---
source_file: "frontend/src/pages/BookingWizardPage.tsx"
type: "code"
community: "Public Booking Frontend"
location: "L19"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Frontend
---

# emptyContact

## Context

_Source: `frontend/src/pages/BookingWizardPage.tsx` (defined near L19; showing L17–L64 of 343)._

```tsx
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
```

## Connections
- [[BookingWizardPage.tsx]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Frontend