---
source_file: "frontend/src/pages/BookingManagePage.tsx"
type: "code"
community: "Public Booking Frontend"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Frontend
---

# BookingManagePage.tsx

## Context

_Source: `frontend/src/pages/BookingManagePage.tsx` (defined near L1; showing L1–L46 of 196)._

```tsx
import { useEffect, useMemo, useState } from "react";
import { Link, useLocation, useParams } from "react-router-dom";
import { useTranslation } from "react-i18next";
import LanguageSwitcher from "../components/LanguageSwitcher";
import ConfirmDialog from "../components/ConfirmDialog";
import { formatZl } from "../api/admin";
import { cancelBooking, getBooking, initiatePayment, type BookingDetails } from "../api/public";

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
```

## Connections
- [[App.tsx]] - `imports_from` [EXTRACTED]
- [[BookingDetails]] - `imports` [EXTRACTED]
- [[BookingManagePage()]] - `contains` [EXTRACTED]
- [[ConfirmDialog()]] - `imports` [EXTRACTED]
- [[ConfirmDialog.tsx]] - `imports_from` [EXTRACTED]
- [[LanguageSwitcher()]] - `imports` [EXTRACTED]
- [[LanguageSwitcher.tsx]] - `imports_from` [EXTRACTED]
- [[admin.ts]] - `imports_from` [EXTRACTED]
- [[cancelBooking()]] - `imports` [EXTRACTED]
- [[formatZl()]] - `imports` [EXTRACTED]
- [[getBooking()]] - `imports` [EXTRACTED]
- [[initiatePayment()]] - `imports` [EXTRACTED]
- [[public.ts]] - `imports_from` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Frontend