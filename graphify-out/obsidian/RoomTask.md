---
source_file: "frontend/src/api/admin.ts"
type: "code"
community: "Admin Tasks & Occupancy Pages"
location: "L193"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Tasks__Occupancy_Pages
---

# RoomTask

## Context

_Source: `frontend/src/api/admin.ts` (defined near L193; showing L191–L238 of 254)._

```typescript
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

// --- Dashboard ---

export interface DashboardSession {
  id: string;
  name: string;
  startDate: string;
  endDate: string;
```

## Connections
- [[SessionOccupancyPage.tsx]] - `imports` [EXTRACTED]
- [[TasksPage.tsx]] - `imports` [EXTRACTED]
- [[admin.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Tasks__Occupancy_Pages