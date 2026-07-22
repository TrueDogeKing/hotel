---
source_file: "src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs"
type: "code"
community: "Admin Bookings Controller & DTOs"
location: "L37"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Bookings_Controller__DTOs
---

# RoomOccupancyDto

## Context

_Source: `src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs` (defined near L37; showing L35–L82 of 92)._

```csharp
/// One row per room in the occupancy grid over a date range. A room is either
/// free, taken by a booking, or blocked by a closure (Closed = true).
public record RoomOccupancyDto(
    Guid RoomId,
    string RoomNumber,
    int Capacity,
    bool IsActive,
    Guid? BookingId,
    string? OrganizationName,
    string? BookingStatus,
    int? PeopleCount,
    bool Closed,
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
```

## Connections
- [[AdminPanelDtos.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Bookings_Controller__DTOs