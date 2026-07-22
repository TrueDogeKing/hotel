---
source_file: "src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs"
type: "code"
community: "Admin Bookings Controller & DTOs"
location: "L49"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Bookings_Controller__DTOs
---

# SessionOccupancyDto

## Context

_Source: `src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs` (defined near L49; showing L47–L92 of 92)._

```csharp
    string? ClosureReason,
    int OpenTaskCount
);

public record OccupancyDto(
    DateOnly StartDate,
    DateOnly EndDate,
    int TotalBeds,
    int OccupiedBeds,
    List<RoomOccupancyDto> Rooms
);

public record RoomTaskDto(
    Guid Id,
    Guid RoomId,
    string RoomNumber,
    Guid? BookingId,
    string Text,
    string Status,
    DateTime CreatedAt,
    DateTime? DoneAt
);

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
- [[.GetOccupancyAsync()]] - `references` [EXTRACTED]
- [[.GetOccupancyAsync()_1]] - `references` [EXTRACTED]
- [[AdminPanelDtos.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Bookings_Controller__DTOs