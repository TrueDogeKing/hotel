---
source_file: "src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs"
type: "code"
community: "Application Namespaces & DTOs"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Application_Namespaces__DTOs
---

# CampCenter.Application.DTOs.AdminPanel

## Context

_Source: `src/CampCenter.Application/DTOs/AdminPanel/AdminPanelDtos.cs` (defined near L1; showing L1–L46 of 92)._

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
```

## Connections
- [[AdminBookingService.cs]] - `imports` [EXTRACTED]
- [[AdminPanelApiTests.cs]] - `imports` [EXTRACTED]
- [[AdminPanelDtos.cs]] - `contains` [EXTRACTED]
- [[BookingsController.cs]] - `imports` [EXTRACTED]
- [[DashboardController.cs]] - `imports` [EXTRACTED]
- [[IAdminBookingService.cs]] - `imports` [EXTRACTED]
- [[IRoomTaskService.cs]] - `imports` [EXTRACTED]
- [[RoomTaskService.cs]] - `imports` [EXTRACTED]
- [[SessionsController.cs]] - `imports` [EXTRACTED]
- [[TasksController.cs]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Application_Namespaces__DTOs