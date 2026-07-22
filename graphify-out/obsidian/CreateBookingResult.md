---
source_file: "frontend/src/api/public.ts"
type: "code"
community: "Public Booking Frontend"
location: "L35"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Frontend
---

# CreateBookingResult

## Context

_Source: `frontend/src/api/public.ts` (defined near L35; showing L33–L80 of 112)._

```typescript
}

export interface CreateBookingResult {
  bookingId: string;
  manageToken: string;
}

export async function createBooking(input: CreateBookingInput): Promise<CreateBookingResult> {
  const { data } = await api.post<CreateBookingResult>("/public/bookings", {
    ...input,
    language: getStoredLanguage(),
  });
  return data;
}

export interface BookingPayment {
  id: string;
  kind: "Deposit" | "Final";
  status: "Pending" | "Completed" | "Failed";
  amountGrosze: number;
  createdAt: string;
  completedAt: string | null;
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
```

## Connections
- [[public.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Frontend