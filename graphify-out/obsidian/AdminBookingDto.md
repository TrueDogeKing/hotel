---
source_file: "src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs"
type: "code"
community: "Admin Booking & Notifications"
location: "L11"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Booking__Notifications
---

# AdminBookingDto

## Context

_Source: `src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs` (defined near L11; showing L9–L56 of 92)._

```csharp
);

public record AdminBookingDto(
    Guid Id,
    DateOnly StartDate,
    DateOnly EndDate,
    int Nights,
    string OrganizationName,
    string ContactName,
    string Email,
    string Phone,
    int Headcount,
    string? Notes,
    string Status,
    string? CancelReason,
    long TotalGrosze,
    long DepositGrosze,
    bool DepositPaid,
    bool FinalPaid,
    /// Confirmed, final unpaid, and past the final-payment due date ("zaległa dopłata").
    bool FinalOverdue,
    DateOnly FinalPaymentDueDate,
    DateTime CreatedAt,
    List<AdminAssignmentDto> Assignments
);

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
```

## Connections
- [[.GetAsync()]] - `references` [EXTRACTED]
- [[.GetAsync()_1]] - `references` [EXTRACTED]
- [[.ListAsync()]] - `references` [EXTRACTED]
- [[.ListAsync()_2]] - `references` [EXTRACTED]
- [[.ReassignAsync()]] - `references` [EXTRACTED]
- [[.ReassignAsync()_1]] - `references` [EXTRACTED]
- [[.ToDto()]] - `references` [EXTRACTED]
- [[AdminPanelDtos.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Booking__Notifications