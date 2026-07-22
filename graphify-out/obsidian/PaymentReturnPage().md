---
source_file: "frontend/src/pages/PaymentReturnPage.tsx"
type: "code"
community: "Public Booking Frontend"
location: "L13"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Frontend
---

# PaymentReturnPage()

## Context

_Source: `frontend/src/pages/PaymentReturnPage.tsx` (defined near L13; showing L11–L58 of 84)._

```tsx
// poll the booking until the payment shows Completed (or give up and point the
// user at the manage page).
export default function PaymentReturnPage() {
  const { t } = useTranslation();
  const [params] = useSearchParams();
  const token = params.get("token");
  const [state, setState] = useState<"waiting" | "paid" | "timeout" | "error">(
    token ? "waiting" : "error",
  );
  const [booking, setBooking] = useState<BookingDetails | null>(null);

  useEffect(() => {
    if (!token) return;

    let cancelled = false;
    const startedAt = Date.now();

    async function poll() {
      try {
        const data = await getBooking(token!);
        if (cancelled) return;
        setBooking(data);
        if (data.payments.some((p) => p.status === "Completed")) {
          setState("paid");
          return;
        }
      } catch {
        if (!cancelled) setState("error");
        return;
      }

      if (Date.now() - startedAt > POLL_TIMEOUT_MS) {
        if (!cancelled) setState("timeout");
        return;
      }

      setTimeout(() => void poll(), POLL_INTERVAL_MS);
    }

    void poll();
    return () => {
      cancelled = true;
    };
  }, [token]);

  return (
    <main className="public-page">
      <header className="public-header">
```

## Connections
- [[PaymentReturnPage.tsx]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Frontend