---
source_file: "frontend/src/api/admin.ts"
type: "code"
community: "Frontend App Shell & i18n"
location: "L244"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_App_Shell__i18n
---

# Dashboard

## Context

_Source: `frontend/src/api/admin.ts` (defined near L244; showing L242–L254 of 254)._

```typescript
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
- [[AdminDashboardPage.tsx]] - `imports` [EXTRACTED]
- [[admin.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_App_Shell__i18n