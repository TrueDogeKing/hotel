---
source_file: "frontend/src/api/admin.ts"
type: "code"
community: "Admin Tasks & Occupancy Pages"
location: "L228"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Tasks__Occupancy_Pages
---

# deleteTask()

## Context

_Source: `frontend/src/api/admin.ts` (defined near L228; showing L226–L254 of 254)._

```typescript
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
- [[TasksPage()]] - `calls` [EXTRACTED]
- [[TasksPage.tsx]] - `imports` [EXTRACTED]
- [[admin.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Tasks__Occupancy_Pages