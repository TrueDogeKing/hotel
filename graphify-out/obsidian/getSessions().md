---
source_file: "frontend/src/api/admin.ts"
type: "code"
community: "Admin Frontend Pages"
location: "L67"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Frontend_Pages
---

# getSessions()

## Context

_Source: `frontend/src/api/admin.ts` (defined near L67; showing L65–L112 of 254)._

```typescript
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
  );
}

export function zlToGrosze(zl: string): number {
  return Math.round(Number(zl.replace(",", ".")) * 100);
}

export function groszeToZl(grosze: number): string {
```

## Connections
- [[AdminBookingsPage()]] - `calls` [EXTRACTED]
- [[AdminBookingsPage.tsx]] - `imports` [EXTRACTED]
- [[SessionsPage()]] - `calls` [EXTRACTED]
- [[SessionsPage.tsx]] - `imports` [EXTRACTED]
- [[admin.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Frontend_Pages