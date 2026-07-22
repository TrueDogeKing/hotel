---
source_file: "frontend/src/api/public.ts"
type: "code"
community: "Public Booking Frontend"
location: "L82"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Frontend
---

# cancelBooking()

## Context

_Source: `frontend/src/api/public.ts` (defined near L82; showing L80–L112 of 112)._

```typescript
}

export async function cancelBooking(token: string): Promise<void> {
  await api.post(`/public/bookings/${token}/cancel`);
}

// Client-side mirror of the server's mix rules, for live wizard feedback.
export function validateMix(
  headcount: number,
  counts: Record<string, number>,
  free: Record<string, number>,
): "ok" | "too-small" | "unavailable" | "redundant" {
  const entries = Object.entries(counts).filter(([, v]) => v > 0);
  for (const [cap, count] of entries) {
    if (count > (free[cap] ?? 0)) return "unavailable";
  }
  const total = entries.reduce((sum, [cap, count]) => sum + Number(cap) * count, 0);
  if (total < headcount) return "too-small";
  for (const [cap] of entries) {
    if (total - Number(cap) >= headcount) return "redundant";
  }
  return "ok";
}

export async function initiatePayment(
  token: string,
  kind: "Deposit" | "Final",
): Promise<{ redirectUrl: string }> {
  const { data } = await api.post<{ redirectUrl: string }>(`/public/bookings/${token}/payments`, {
    kind,
  });
  return data;
}
```

## Connections
- [[BookingManagePage.tsx]] - `imports` [EXTRACTED]
- [[public.ts]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Public_Booking_Frontend