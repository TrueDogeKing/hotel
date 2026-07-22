---
source_file: "frontend/src/api/admin.ts"
type: "code"
community: "Frontend App Shell & i18n"
location: "L251"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Frontend_App_Shell__i18n
---

# getDashboard()

## Context

_Source: `frontend/src/api/admin.ts` (defined near L251; showing L249–L254 of 254)._

```typescript
}

export async function getDashboard(): Promise<Dashboard> {
  const { data } = await api.get<Dashboard>("/admin/dashboard");
  return data;
}
```

## Connections
- [[AdminDashboardPage()]] - `calls` [EXTRACTED]
- [[AdminDashboardPage.tsx]] - `imports` [EXTRACTED]
- [[admin.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Frontend_App_Shell__i18n