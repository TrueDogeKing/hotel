---
source_file: "frontend/src/pages/BookingManagePage.tsx"
type: "code"
community: "Public Booking Frontend"
location: "L10"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Frontend
---

# BookingManagePage()

## Context

_Source: `frontend/src/pages/BookingManagePage.tsx` (defined near L10; showing L8–L55 of 196)._

```tsx

// Booking manage page, reached via the secret link from the confirmation email.
export default function BookingManagePage() {
  const { t, i18n } = useTranslation();
  const { token } = useParams<{ token: string }>();
  const location = useLocation();
  const justCreated = (location.state as { justCreated?: boolean } | null)?.justCreated ?? false;

  const [booking, setBooking] = useState<BookingDetails | null>(null);
  const [notFound, setNotFound] = useState(false);
  const [confirmCancel, setConfirmCancel] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [paying, setPaying] = useState(false);

  useEffect(() => {
    if (!token) return;
    let cancelled = false;
    getBooking(token)
      .then((data) => {
        if (!cancelled) setBooking(data);
      })
      .catch(() => {
        if (!cancelled) setNotFound(true);
      });
    return () => {
      cancelled = true;
    };
  }, [token]);

  const dateFormatter = useMemo(
    () =>
      new Intl.DateTimeFormat(i18n.language === "en" ? "en-GB" : "pl-PL", {
        dateStyle: "medium",
      }),
    [i18n.language],
  );
  const dateTimeFormatter = useMemo(
    () =>
      new Intl.DateTimeFormat(i18n.language === "en" ? "en-GB" : "pl-PL", {
        dateStyle: "medium",
        timeStyle: "short",
      }),
    [i18n.language],
  );

  async function pay(kind: "Deposit" | "Final") {
    if (!token) return;
    setError(null);
```

## Connections
- [[BookingManagePage.tsx]] - `contains` [EXTRACTED]
- [[formatZl()]] - `calls` [EXTRACTED]
- [[getBooking()]] - `calls` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Frontend