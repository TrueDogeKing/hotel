---
source_file: "frontend/src/api/admin.ts"
type: "code"
community: "Admin Frontend Pages"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Frontend_Pages
---

# admin.ts

## Context

_Source: `frontend/src/api/admin.ts` (defined near L1; showing L1–L46 of 254)._

```typescript
import { api } from "./client";

// --- Rooms ---

export interface Room {
  id: string;
  number: string;
  capacity: number;
  isActive: boolean;
  description: string | null;
  rowVersion: number;
}

export interface RoomInput {
  number: string;
  capacity: number;
  description: string | null;
}

export async function getRooms(): Promise<Room[]> {
  const { data } = await api.get<Room[]>("/admin/rooms");
  return data;
}

export async function createRoom(input: RoomInput): Promise<Room> {
  const { data } = await api.post<Room>("/admin/rooms", input);
  return data;
}

export async function updateRoom(
  id: string,
  input: RoomInput & { isActive: boolean; rowVersion: number },
): Promise<Room> {
  const { data } = await api.put<Room>(`/admin/rooms/${id}`, input);
  return data;
}

// Hard-deletes an unreferenced room; a room with booking history is deactivated instead.
export async function deleteRoom(id: string): Promise<{ deleted: boolean }> {
  const { data } = await api.delete<{ deleted: boolean }>(`/admin/rooms/${id}`);
  return data;
}

// --- Camp sessions (turnusy) ---

export type CampSessionStatus = "Draft" | "Published" | "Archived";
```

## Connections
- [[AdminAssignment]] - `contains` [EXTRACTED]
- [[AdminBooking]] - `contains` [EXTRACTED]
- [[AdminBookingsPage.tsx]] - `imports_from` [EXTRACTED]
- [[AdminDashboardPage.tsx]] - `imports_from` [EXTRACTED]
- [[BookingManagePage.tsx]] - `imports_from` [EXTRACTED]
- [[BookingWizardPage.tsx]] - `imports_from` [EXTRACTED]
- [[CampSession]] - `contains` [EXTRACTED]
- [[CampSessionInput]] - `contains` [EXTRACTED]
- [[CampSessionStatus]] - `contains` [EXTRACTED]
- [[Dashboard]] - `contains` [EXTRACTED]
- [[DashboardSession]] - `contains` [EXTRACTED]
- [[Room]] - `contains` [EXTRACTED]
- [[RoomInput]] - `contains` [EXTRACTED]
- [[RoomOccupancy]] - `contains` [EXTRACTED]
- [[RoomTask]] - `contains` [EXTRACTED]
- [[RoomsPage.tsx]] - `imports_from` [EXTRACTED]
- [[SessionOccupancy]] - `contains` [EXTRACTED]
- [[SessionOccupancyPage.tsx]] - `imports_from` [EXTRACTED]
- [[SessionsPage.tsx]] - `imports_from` [EXTRACTED]
- [[TasksPage.tsx]] - `imports_from` [EXTRACTED]
- [[api]] - `imports` [EXTRACTED]
- [[archiveSession()]] - `contains` [EXTRACTED]
- [[cancelAdminBooking()]] - `contains` [EXTRACTED]
- [[client.ts]] - `imports_from` [EXTRACTED]
- [[createRoom()]] - `contains` [EXTRACTED]
- [[createSession()]] - `contains` [EXTRACTED]
- [[createTask()]] - `contains` [EXTRACTED]
- [[deleteRoom()]] - `contains` [EXTRACTED]
- [[deleteSession()]] - `contains` [EXTRACTED]
- [[deleteTask()]] - `contains` [EXTRACTED]
- [[formatZl()]] - `contains` [EXTRACTED]
- [[getAdminBookings()]] - `contains` [EXTRACTED]
- [[getDashboard()]] - `contains` [EXTRACTED]
- [[getOccupancy()]] - `contains` [EXTRACTED]
- [[getRooms()]] - `contains` [EXTRACTED]
- [[getSessions()]] - `contains` [EXTRACTED]
- [[getTasks()]] - `contains` [EXTRACTED]
- [[groszeToZl()]] - `contains` [EXTRACTED]
- [[publishSession()]] - `contains` [EXTRACTED]
- [[setTaskDone()]] - `contains` [EXTRACTED]
- [[updateRoom()]] - `contains` [EXTRACTED]
- [[updateSession()]] - `contains` [EXTRACTED]
- [[zlToGrosze()]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Frontend_Pages