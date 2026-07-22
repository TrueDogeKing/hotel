---
source_file: "src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs"
type: "code"
community: "Admin Bookings Controller & DTOs"
location: "L71"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Bookings_Controller__DTOs
---

# CreateRoomTaskRequestDto

## Context

_Source: `src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs` (defined near L71; showing L69–L92 of 92)._

```csharp

public record CreateRoomTaskRequestDto(Guid RoomId, string Text, Guid? BookingId);

public record ReassignmentEntryDto(Guid RoomId, int PeopleCount);

public record ReassignBookingRequestDto(List<ReassignmentEntryDto> Assignments);

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
- [[.Create()_2]] - `references` [EXTRACTED]
- [[.CreateAsync()_3]] - `references` [EXTRACTED]
- [[.CreateAsync()_7]] - `references` [EXTRACTED]
- [[AdminPanelDtos.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Bookings_Controller__DTOs