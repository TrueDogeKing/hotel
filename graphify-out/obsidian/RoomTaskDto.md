---
source_file: "src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs"
type: "code"
community: "Room Task Management"
location: "L59"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Task_Management
---

# RoomTaskDto

## Context

_Source: `src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs` (defined near L59; showing L57–L92 of 92)._

```csharp
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
- [[.CreateAsync()_3]] - `references` [EXTRACTED]
- [[.CreateAsync()_7]] - `references` [EXTRACTED]
- [[.ListAsync()_1]] - `references` [EXTRACTED]
- [[.ListAsync()_3]] - `references` [EXTRACTED]
- [[.SetStatusAsync()]] - `references` [EXTRACTED]
- [[.SetStatusAsync()_1]] - `references` [EXTRACTED]
- [[.ToDto()_3]] - `references` [EXTRACTED]
- [[AdminPanelDtos.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Task_Management