---
source_file: "frontend/src/pages/admin/SessionsPage.tsx"
type: "code"
community: "Admin Frontend Pages"
location: "L27"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Frontend_Pages
---

# emptyForm

## Context

_Source: `frontend/src/pages/admin/SessionsPage.tsx` (defined near L27; showing L25–L72 of 235)._

```tsx
}

const emptyForm: SessionFormState = {
  name: "",
  startDate: "",
  endDate: "",
  priceZl: "",
  depositZl: "",
};

export default function SessionsPage() {
  const { t, i18n } = useTranslation();
  const [sessions, setSessions] = useState<CampSession[]>([]);
  const [form, setForm] = useState<SessionFormState>(emptyForm);
  const [editing, setEditing] = useState<CampSession | null>(null);
  const [error, setError] = useState<string | null>(null);

  async function reload() {
    setSessions(await getSessions());
  }

  useEffect(() => {
    let cancelled = false;
    void getSessions().then((data) => {
      if (!cancelled) setSessions(data);
    });
    return () => {
      cancelled = true;
    };
  }, []);

  function handleApiError(err: unknown) {
    if (isAxiosError(err) && err.response?.status === 400) {
      // Business-rule violations (overlap, frozen dates) come back as ProblemDetails.
      const detail = (err.response.data as { detail?: string } | undefined)?.detail;
      setError(detail ?? t("adminSessions.genericError"));
    } else if (isAxiosError(err) && err.response?.status === 409) {
      setError(t("adminSessions.conflict"));
    } else {
      setError(t("adminSessions.genericError"));
    }
  }

  async function handleSubmit(event: FormEvent) {
    event.preventDefault();
    setError(null);
    const input = {
      name: form.name.trim(),
```

## Connections
- [[SessionsPage.tsx]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Frontend_Pages