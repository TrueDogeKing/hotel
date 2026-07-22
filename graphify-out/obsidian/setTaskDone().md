---
source_file: "frontend/src/api/admin.ts"
type: "code"
community: "Admin Tasks & Occupancy Pages"
location: "L223"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Tasks__Occupancy_Pages
---

# setTaskDone()

## Context

_Source: `frontend/src/api/admin.ts` (defined near L223; showing L221–L254 of 254)._

```typescript
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
  totalBeds: number;
  occupiedBeds: number;
  bookingCount: number;
}

export interface Dashboard {
  upcomingSessions: DashboardSession[];
  pendingDepositCount: number;
  overdueFinalCount: number;
  openTaskCount: number;
}

export async function getDashboard(): Promise<Dashboard> {
  const { data } = await api.get<Dashboard>("/admin/dashboard");
  return data;
}
```

## Connections
- [[SessionOccupancyPage.tsx]] - `imports` [EXTRACTED]
- [[TasksPage()]] - `calls` [EXTRACTED]
- [[TasksPage.tsx]] - `imports` [EXTRACTED]
- [[admin.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Tasks__Occupancy_Pages