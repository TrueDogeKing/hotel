---
source_file: "frontend/src/api/public.ts"
type: "code"
community: "Public Booking Frontend"
location: "L104"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Public_Booking_Frontend
---

# initiatePayment()

## Context

_Source: `frontend/src/api/public.ts` (defined near L104; showing L102–L112 of 112)._

```typescript
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