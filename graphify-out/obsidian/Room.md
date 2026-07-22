---
source_file: "frontend/src/api/admin.ts"
type: "code"
community: "Admin Frontend Pages"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Frontend_Pages
---

# Room

## Context

_Source: `frontend/src/api/admin.ts` (defined near L5; showing L3–L50 of 254)._

```typescript
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

export interface CampSession {
  id: string;
  name: string;
```

## Connections
- [[RoomsPage.tsx]] - `imports` [EXTRACTED]
- [[admin.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Frontend_Pages