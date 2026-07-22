---
source_file: "frontend/src/pages/admin/SessionsPage.tsx"
type: "code"
community: "Admin Frontend Pages"
location: "L35"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Frontend_Pages
---

# SessionsPage()

## Context

_Source: `frontend/src/pages/admin/SessionsPage.tsx` (defined near L35; showing L33–L80 of 235)._

```tsx
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
      startDate: form.startDate,
      endDate: form.endDate,
      pricePerPersonGrosze: zlToGrosze(form.priceZl),
      depositPerPersonGrosze: zlToGrosze(form.depositZl),
    };
    try {
      if (editing) {
        await updateSession(editing.id, { ...input, rowVersion: editing.rowVersion });
```

## Connections
- [[SessionsPage.tsx]] - `contains` [EXTRACTED]
- [[archiveSession()]] - `calls` [EXTRACTED]
- [[deleteSession()]] - `calls` [EXTRACTED]
- [[formatZl()]] - `calls` [EXTRACTED]
- [[getSessions()]] - `calls` [EXTRACTED]
- [[publishSession()]] - `calls` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Frontend_Pages