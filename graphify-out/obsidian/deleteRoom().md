---
source_file: "frontend/src/api/admin.ts"
type: "code"
community: "Admin Frontend Pages"
location: "L39"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Frontend_Pages
---

# deleteRoom()

## Context

_Source: `frontend/src/api/admin.ts` (defined near L39; showing L37–L84 of 254)._

```typescript

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

export async function createSession(input: CampSessionInput): Promise<CampSession> {
  const { data } = await api.post<CampSession>("/admin/sessions", input);
  return data;
}

export async function updateSession(
  id: string,
  input: CampSessionInput & { rowVersion: number },
): Promise<CampSession> {
  const { data } = await api.put<CampSession>(`/admin/sessions/${id}`, input);
  return data;
}

```

## Connections
- [[RoomsPage.tsx]] - `imports` [EXTRACTED]
- [[admin.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Frontend_Pages