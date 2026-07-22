---
source_file: "frontend/src/api/admin.ts"
type: "code"
community: "Admin Frontend Pages"
location: "L150"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Frontend_Pages
---

# getAdminBookings()

## Context

_Source: `frontend/src/api/admin.ts` (defined near L150; showing L148–L195 of 254)._

```typescript
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
  peopleCount: number | null;
  openTaskCount: number;
}

export interface SessionOccupancy {
  sessionId: string;
  sessionName: string;
  startDate: string;
  endDate: string;
  totalBeds: number;
  occupiedBeds: number;
  rooms: RoomOccupancy[];
}

export async function getOccupancy(sessionId: string): Promise<SessionOccupancy> {
  const { data } = await api.get<SessionOccupancy>(`/admin/sessions/${sessionId}/occupancy`);
  return data;
}

// --- Housekeeping tasks ---

export interface RoomTask {
  id: string;
  roomId: string;
```

## Connections
- [[AdminBookingsPage()]] - `calls` [EXTRACTED]
- [[AdminBookingsPage.tsx]] - `imports` [EXTRACTED]
- [[admin.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Frontend_Pages