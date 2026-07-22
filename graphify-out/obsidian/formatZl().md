---
source_file: "frontend/src/api/admin.ts"
type: "code"
community: "Public Booking Frontend"
location: "L100"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Frontend
---

# formatZl()

## Context

_Source: `frontend/src/api/admin.ts` (defined near L100; showing L98–L145 of 254)._

```typescript

// Grosze → "1 234,56 zł" style display; forms edit złote as decimal strings.
export function formatZl(grosze: number): string {
  return (
    new Intl.NumberFormat("pl-PL", { minimumFractionDigits: 2, maximumFractionDigits: 2 }).format(
      grosze / 100,
    ) + " zł"
  );
}

export function zlToGrosze(zl: string): number {
  return Math.round(Number(zl.replace(",", ".")) * 100);
}

export function groszeToZl(grosze: number): string {
  return (grosze / 100).toFixed(2);
}

// --- Bookings (admin) ---

export interface AdminAssignment {
  id: string;
  roomId: string;
  roomNumber: string;
  capacity: number;
  peopleCount: number;
}

export interface AdminBooking {
  id: string;
  sessionName: string;
  campSessionId: string;
  startDate: string;
  endDate: string;
  organizationName: string;
  contactName: string;
  email: string;
  phone: string;
  headcount: number;
  notes: string | null;
  status: "PendingDeposit" | "Confirmed" | "Cancelled" | "Completed";
  cancelReason: string | null;
  totalGrosze: number;
  depositGrosze: number;
  depositPaid: boolean;
  finalPaid: boolean;
  finalOverdue: boolean;
  finalPaymentDueDate: string;
```

## Connections
- [[AdminBookingsPage()]] - `calls` [EXTRACTED]
- [[AdminBookingsPage.tsx]] - `imports` [EXTRACTED]
- [[BookingManagePage()]] - `calls` [EXTRACTED]
- [[BookingManagePage.tsx]] - `imports` [EXTRACTED]
- [[BookingWizardPage()]] - `calls` [EXTRACTED]
- [[BookingWizardPage.tsx]] - `imports` [EXTRACTED]
- [[SessionsPage()]] - `calls` [EXTRACTED]
- [[SessionsPage.tsx]] - `imports` [EXTRACTED]
- [[admin.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Frontend