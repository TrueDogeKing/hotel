---
source_file: "frontend/src/api/admin.ts"
type: "code"
community: "Admin Frontend Pages"
location: "L126"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Frontend_Pages
---

# AdminBooking

## Context

_Source: `frontend/src/api/admin.ts` (defined near L126; showing L124–L171 of 254)._

```typescript
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

export interface RoomOccupancy {
  roomId: string;
  roomNumber: string;
  capacity: number;
  isActive: boolean;
  bookingId: string | null;
  organizationName: string | null;
  bookingStatus: string | null;
```

## Connections
- [[AdminBookingsPage.tsx]] - `imports` [EXTRACTED]
- [[admin.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Frontend_Pages