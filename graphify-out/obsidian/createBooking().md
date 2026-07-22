---
source_file: "frontend/src/api/public.ts"
type: "code"
community: "Public Booking Frontend"
location: "L40"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Frontend
---

# createBooking()

## Context

_Source: `frontend/src/api/public.ts` (defined near L40; showing L38–L85 of 112)._

```typescript
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

export async function cancelBooking(token: string): Promise<void> {
  await api.post(`/public/bookings/${token}/cancel`);
}

```

## Connections
- [[BookingWizardPage.tsx]] - `imports` [EXTRACTED]
- [[getStoredLanguage()]] - `calls` [EXTRACTED]
- [[public.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Frontend