---
source_file: "src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs"
type: "code"
community: "Admin Bookings Controller & DTOs"
location: "L82"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Bookings_Controller__DTOs
---

# DashboardSessionDto

## Context

_Source: `src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs` (defined near L82; showing L80–L92 of 92)._

```csharp
    DateOnly EndDate,
    int Headcount,
    int OccupiedBeds,
    string Status
);

public record DashboardDto(
    List<DashboardBookingDto> UpcomingBookings,
    int PendingDepositCount,
    int OverdueFinalCount,
    int OpenTaskCount,
    int ActiveClosureCount
);
```

## Connections
- [[AdminPanelDtos.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Bookings_Controller__DTOs