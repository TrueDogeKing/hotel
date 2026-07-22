---
source_file: "frontend/src/pages/admin/AdminBookingsPage.tsx"
type: "code"
community: "Admin Frontend Pages"
location: "L14"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Frontend_Pages
---

# AdminBookingsPage()

## Context

_Source: `frontend/src/pages/admin/AdminBookingsPage.tsx` (defined near L14; showing L12–L59 of 196)._

```tsx
} from "../../api/admin";

export default function AdminBookingsPage() {
  const { t, i18n } = useTranslation();
  const [bookings, setBookings] = useState<AdminBooking[]>([]);
  const [sessions, setSessions] = useState<CampSession[]>([]);
  const [sessionFilter, setSessionFilter] = useState("");
  const [statusFilter, setStatusFilter] = useState("");
  const [expanded, setExpanded] = useState<string | null>(null);
  const [cancelTarget, setCancelTarget] = useState<AdminBooking | null>(null);
  const [error, setError] = useState<string | null>(null);

  const reload = useCallback(async () => {
    setBookings(
      await getAdminBookings({
        sessionId: sessionFilter || undefined,
        status: statusFilter || undefined,
      }),
    );
  }, [sessionFilter, statusFilter]);

  useEffect(() => {
    let cancelled = false;
    void getSessions().then((data) => {
      if (!cancelled) setSessions(data);
    });
    return () => {
      cancelled = true;
    };
  }, []);

  useEffect(() => {
    let cancelled = false;
    void getAdminBookings({
      sessionId: sessionFilter || undefined,
      status: statusFilter || undefined,
    }).then((data) => {
      if (!cancelled) setBookings(data);
    });
    return () => {
      cancelled = true;
    };
  }, [sessionFilter, statusFilter]);

  async function confirmCancel() {
    if (!cancelTarget) return;
    setError(null);
    try {
```

## Connections
- [[AdminBookingsPage.tsx]] - `contains` [EXTRACTED]
- [[formatZl()]] - `calls` [EXTRACTED]
- [[getAdminBookings()]] - `calls` [EXTRACTED]
- [[getSessions()]] - `calls` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Frontend_Pages