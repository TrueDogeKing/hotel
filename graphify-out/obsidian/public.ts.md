---
source_file: "frontend/src/api/public.ts"
type: "code"
community: "Public Booking Frontend"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Frontend
---

# public.ts

## Context

_Source: `frontend/src/api/public.ts` (defined near L1; showing L1–L46 of 112)._

```typescript
import { api } from "./client";
import { getStoredLanguage } from "../i18n";

export interface PublicSession {
  id: string;
  name: string;
  startDate: string;
  endDate: string;
  pricePerPersonGrosze: number;
  depositPerPersonGrosze: number;
  remainingCapacity: number;
  freeRoomsByCapacity: Record<string, number>;
  fits: boolean | null;
  suggestedMix: Record<string, number> | null;
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
```

## Connections
- [[BookingDetails]] - `contains` [EXTRACTED]
- [[BookingManagePage.tsx]] - `imports_from` [EXTRACTED]
- [[BookingPayment]] - `contains` [EXTRACTED]
- [[BookingWizardPage.tsx]] - `imports_from` [EXTRACTED]
- [[CreateBookingInput]] - `contains` [EXTRACTED]
- [[CreateBookingResult]] - `contains` [EXTRACTED]
- [[PaymentReturnPage.tsx]] - `imports_from` [EXTRACTED]
- [[PublicSession]] - `contains` [EXTRACTED]
- [[api]] - `imports` [EXTRACTED]
- [[cancelBooking()]] - `contains` [EXTRACTED]
- [[client.ts]] - `imports_from` [EXTRACTED]
- [[createBooking()]] - `contains` [EXTRACTED]
- [[getBooking()]] - `contains` [EXTRACTED]
- [[getPublicSessions()]] - `contains` [EXTRACTED]
- [[getStoredLanguage()]] - `imports` [EXTRACTED]
- [[index.ts]] - `imports_from` [EXTRACTED]
- [[initiatePayment()]] - `contains` [EXTRACTED]
- [[validateMix()]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Frontend