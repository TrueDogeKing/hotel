---
source_file: "src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs"
type: "code"
community: "Admin Bookings Controller & DTOs"
location: "L3"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_Bookings_Controller__DTOs
---

# AdminAssignmentDto

## Context

_Source: `src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs` (defined near L3; showing L1–L48 of 92)._

```csharp
namespace CampCenter.Application.DTOs.AdminPanel;

public record AdminAssignmentDto(
    Guid Id,
    Guid RoomId,
    string RoomNumber,
    int Capacity,
    int PeopleCount
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
```

## Connections
- [[AdminPanelDtos.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_Bookings_Controller__DTOs