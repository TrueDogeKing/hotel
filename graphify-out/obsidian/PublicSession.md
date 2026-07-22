---
source_file: "frontend/src/api/public.ts"
type: "code"
community: "Public Booking Frontend"
location: "L4"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Frontend
---

# PublicSession

## Context

_Source: `frontend/src/api/public.ts` (defined near L4; showing L2–L49 of 112)._

```typescript
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

export interface BookingPayment {
  id: string;
```

## Connections
- [[BookingWizardPage.tsx]] - `imports` [EXTRACTED]
- [[public.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Frontend