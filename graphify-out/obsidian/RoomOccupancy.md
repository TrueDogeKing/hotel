---
source_file: "frontend/src/api/admin.ts"
type: "code"
community: "Admin Tasks & Occupancy Pages"
location: "L164"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Tasks__Occupancy_Pages
---

# RoomOccupancy

## Context

_Source: `frontend/src/api/admin.ts` (defined near L164; showing L162–L209 of 254)._

```typescript
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
  roomNumber: string;
  campSessionId: string | null;
  bookingId: string | null;
  text: string;
  status: "Open" | "Done";
  createdAt: string;
  doneAt: string | null;
}

export async function getTasks(filters: {
  status?: string;
  sessionId?: string;
}): Promise<RoomTask[]> {
  const { data } = await api.get<RoomTask[]>("/admin/tasks", { params: filters });
```

## Connections
- [[SessionOccupancyPage.tsx]] - `imports` [EXTRACTED]
- [[admin.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Tasks__Occupancy_Pages