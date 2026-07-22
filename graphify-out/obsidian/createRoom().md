---
source_file: "frontend/src/api/admin.ts"
type: "code"
community: "Admin Frontend Pages"
location: "L25"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Frontend_Pages
---

# createRoom()

## Context

_Source: `frontend/src/api/admin.ts` (defined near L25; showing L23–L70 of 254)._

```typescript
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
  startDate: string;
  endDate: string;
  pricePerPersonGrosze: number;
  depositPerPersonGrosze: number;
  status: CampSessionStatus;
  rowVersion: number;
}

export interface CampSessionInput {
  name: string;
  startDate: string;
  endDate: string;
  pricePerPersonGrosze: number;
  depositPerPersonGrosze: number;
}

export async function getSessions(): Promise<CampSession[]> {
  const { data } = await api.get<CampSession[]>("/admin/sessions");
  return data;
}
```

## Connections
- [[RoomsPage.tsx]] - `imports` [EXTRACTED]
- [[admin.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Frontend_Pages