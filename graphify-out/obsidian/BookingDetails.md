---
source_file: "frontend/src/api/public.ts"
type: "code"
community: "Public Booking Frontend"
location: "L57"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Frontend
---

# BookingDetails

## Context

_Source: `frontend/src/api/public.ts` (defined near L57; showing L55–L102 of 112)._

```typescript
}

export interface BookingDetails {
  id: string;
  status: "PendingDeposit" | "Confirmed" | "Cancelled" | "Completed";
  cancelReason: string | null;
  sessionName: string;
  startDate: string;
  endDate: string;
  organizationName: string;
  contactName: string;
  email: string;
  phone: string;
  headcount: number;
  roomCounts: Record<string, number>;
  totalGrosze: number;
  depositGrosze: number;
  holdExpiresAt: string | null;
  finalPaymentDueDate: string;
  payments: BookingPayment[];
}

export async function getBooking(token: string): Promise<BookingDetails> {
  const { data } = await api.get<BookingDetails>(`/public/bookings/${token}`);
  return data;
}

export async function cancelBooking(token: string): Promise<void> {
  await api.post(`/public/bookings/${token}/cancel`);
}

// Client-side mirror of the server's mix rules, for live wizard feedback.
export function validateMix(
  headcount: number,
  counts: Record<string, number>,
  free: Record<string, number>,
): "ok" | "too-small" | "unavailable" | "redundant" {
  const entries = Object.entries(counts).filter(([, v]) => v > 0);
  for (const [cap, count] of entries) {
    if (count > (free[cap] ?? 0)) return "unavailable";
  }
  const total = entries.reduce((sum, [cap, count]) => sum + Number(cap) * count, 0);
  if (total < headcount) return "too-small";
  for (const [cap] of entries) {
    if (total - Number(cap) >= headcount) return "redundant";
  }
  return "ok";
}
```

## Connections
- [[BookingManagePage.tsx]] - `imports` [EXTRACTED]
- [[PaymentReturnPage.tsx]] - `imports` [EXTRACTED]
- [[public.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Frontend