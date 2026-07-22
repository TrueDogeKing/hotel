---
source_file: "frontend/src/pages/PaymentReturnPage.tsx"
type: "code"
community: "Public Booking Frontend"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Frontend
---

# PaymentReturnPage.tsx

## Context

_Source: `frontend/src/pages/PaymentReturnPage.tsx` (defined near L1; showing L1–L46 of 84)._

```tsx
import { useEffect, useState } from "react";
import { Link, useSearchParams } from "react-router-dom";
import { useTranslation } from "react-i18next";
import LanguageSwitcher from "../components/LanguageSwitcher";
import { getBooking, type BookingDetails } from "../api/public";

const POLL_INTERVAL_MS = 3000;
const POLL_TIMEOUT_MS = 60000;

// Return page after the P24 redirect. The webhook is the source of truth, so we
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

```

## Connections
- [[App.tsx]] - `imports_from` [EXTRACTED]
- [[BookingDetails]] - `imports` [EXTRACTED]
- [[LanguageSwitcher()]] - `imports` [EXTRACTED]
- [[LanguageSwitcher.tsx]] - `imports_from` [EXTRACTED]
- [[PaymentReturnPage()]] - `contains` [EXTRACTED]
- [[getBooking()]] - `imports` [EXTRACTED]
- [[public.ts]] - `imports_from` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Frontend