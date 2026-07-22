---
source_file: "frontend/src/api/admin.ts"
type: "code"
community: "Admin Frontend Pages"
location: "L118"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Frontend_Pages
---

# AdminAssignment

## Context

_Source: `frontend/src/api/admin.ts` (defined near L118; showing L116–L163 of 254)._

```typescript
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

export async function cancelAdminBooking(id: string): Promise<void> {
  await api.post(`/admin/bookings/${id}/cancel`);
}

// --- Occupancy ---

```

## Connections
- [[admin.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Frontend_Pages