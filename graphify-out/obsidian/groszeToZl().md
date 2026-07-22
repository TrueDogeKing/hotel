---
source_file: "frontend/src/api/admin.ts"
type: "code"
community: "Admin Frontend Pages"
location: "L112"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Frontend_Pages
---

# groszeToZl()

## Context

_Source: `frontend/src/api/admin.ts` (defined near L112; showing L110–L157 of 254)._

```typescript
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
  createdAt: string;
  assignments: AdminAssignment[];
}

export async function getAdminBookings(filters: {
  sessionId?: string;
  status?: string;
}): Promise<AdminBooking[]> {
  const { data } = await api.get<AdminBooking[]>("/admin/bookings", { params: filters });
  return data;
}

```

## Connections
- [[SessionsPage.tsx]] - `imports` [EXTRACTED]
- [[admin.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Frontend_Pages