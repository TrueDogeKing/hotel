---
source_file: "frontend/src/api/admin.ts"
type: "code"
community: "Admin Tasks & Occupancy Pages"
location: "L186"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Tasks__Occupancy_Pages
---

# getOccupancy()

## Context

_Source: `frontend/src/api/admin.ts` (defined near L186; showing L184–L231 of 254)._

```typescript
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
  return data;
}

export async function createTask(input: {
  roomId: string;
  text: string;
  campSessionId: string | null;
  bookingId: string | null;
}): Promise<RoomTask> {
  const { data } = await api.post<RoomTask>("/admin/tasks", input);
  return data;
}

export async function setTaskDone(id: string, done: boolean): Promise<RoomTask> {
  const { data } = await api.post<RoomTask>(`/admin/tasks/${id}/${done ? "done" : "reopen"}`);
  return data;
}

export async function deleteTask(id: string): Promise<void> {
  await api.delete(`/admin/tasks/${id}`);
}

```

## Connections
- [[SessionOccupancyPage()]] - `calls` [EXTRACTED]
- [[SessionOccupancyPage.tsx]] - `imports` [EXTRACTED]
- [[admin.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Tasks__Occupancy_Pages