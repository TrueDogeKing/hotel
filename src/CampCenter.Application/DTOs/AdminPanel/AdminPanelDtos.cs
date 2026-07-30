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

/// A room this booking may occupy: active, and free of other bookings and closures
/// over the whole stay. The booking's own rooms are in the list too, flagged with
/// Assigned — moving a group is picking from this one set.
public record AssignableRoomDto(Guid RoomId, string RoomNumber, int Capacity, bool Assigned);

public record ReassignmentEntryDto(Guid RoomId, int PeopleCount);

public record ReassignBookingRequestDto(List<ReassignmentEntryDto> Assignments);

/// A group entered by staff (phone or walk-in) rather than booked through the
/// public wizard. Rooms are picked automatically to fit the headcount and the
/// booker gets no confirmation email — the admin owns the booking's state, so
/// the starting status is theirs to choose.
public record CreateAdminBookingRequestDto(
    DateOnly StartDate,
    DateOnly EndDate,
    string OrganizationName,
    string ContactName,
    string Email,
    string Phone,
    int Headcount,
    string? Notes,
    /// PendingDeposit / Confirmed / Cancelled / Completed. Defaults to Confirmed.
    string? Status,
    /// "pl" or "en"; drives the language of any later email. Defaults to "pl".
    string? Language
);

/// Manual status override. Any status may be set from any other — see
/// IAdminBookingService.SetStatusAsync for what each transition does.
public record SetBookingStatusRequestDto(string Status);

public record DashboardBookingDto(
    Guid Id,
    string OrganizationName,
    DateOnly StartDate,
    DateOnly EndDate,
    int Headcount,
    int OccupiedBeds,
    string Status
);

/// One page of a dashboard group list. Total is the whole category, so the fold can
/// show its size without having loaded a single row of it.
public record BookingGroupPageDto(
    string Category,
    int Total,
    int Skip,
    List<DashboardBookingDto> Items
);

public record DashboardDto(
    List<DashboardBookingDto> UpcomingBookings,
    int PendingDepositCount,
    int OverdueFinalCount,
    int OpenTaskCount,
    int ActiveClosureCount
);
