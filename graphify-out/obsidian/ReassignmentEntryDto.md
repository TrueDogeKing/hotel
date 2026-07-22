---
source_file: "src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs"
type: "code"
community: "Admin Bookings Controller & DTOs"
location: "L78"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Bookings_Controller__DTOs
---

# ReassignmentEntryDto

## Context

_Source: `src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs` (defined near L78; showing L76–L92 of 92)._

```csharp
public record DashboardBookingDto(
    Guid Id,
    string OrganizationName,
    DateOnly StartDate,
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