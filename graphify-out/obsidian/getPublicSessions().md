---
source_file: "frontend/src/api/public.ts"
type: "code"
community: "Public Booking Frontend"
location: "L17"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Frontend
---

# getPublicSessions()

## Context

_Source: `frontend/src/api/public.ts` (defined near L17; showing L15–L62 of 112)._

```typescript
}

export async function getPublicSessions(headcount?: number): Promise<PublicSession[]> {
  const { data } = await api.get<PublicSession[]>("/public/sessions", {
    params: headcount ? { headcount } : {},
  });
  return data;
}

export interface CreateBookingInput {
  campSessionId: string;
  headcount: number;
  roomCounts: Record<string, number>;
  organizationName: string;
  contactName: string;
  email: string;
  phone: string;
  notes: string | null;
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
```

## Connections
- [[BookingWizardPage.tsx]] - `imports` [EXTRACTED]
- [[public.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Frontend