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
    /// Admin-managed dietary/preparation note; distinct from Notes, which the booker wrote.
    string? DietaryNotes,
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
