---
source_file: "frontend/src/api/admin.ts"
type: "code"
community: "Admin Frontend Pages"
location: "L59"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Frontend_Pages
---

# CampSessionInput

## Context

_Source: `frontend/src/api/admin.ts` (defined near L59; showing L57–L104 of 254)._

```typescript
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

export async function publishSession(id: string): Promise<CampSession> {
  const { data } = await api.post<CampSession>(`/admin/sessions/${id}/publish`);
  return data;
}

export async function archiveSession(id: string): Promise<CampSession> {
  const { data } = await api.post<CampSession>(`/admin/sessions/${id}/archive`);
  return data;
}

export async function deleteSession(id: string): Promise<void> {
  await api.delete(`/admin/sessions/${id}`);
}

// Grosze → "1 234,56 zł" style display; forms edit złote as decimal strings.
export function formatZl(grosze: number): string {
  return (
    new Intl.NumberFormat("pl-PL", { minimumFractionDigits: 2, maximumFractionDigits: 2 }).format(
      grosze / 100,
    ) + " zł"
```

## Connections
- [[admin.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Frontend_Pages