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
    /// The rate the total was worked out from: total = rate × headcount × nights,
    /// unless the owner typed a flat total over the top of it.
    long PricePerPersonPerNightGrosze,
    /// Unpaid / DepositPaid / Paid, as recorded by the owner.
    string PaymentState,
    /// Status and payment folded into the single value the panel works in — see
    /// BookingState. Status and PaymentState above remain the stored truth.
    string State,
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
    Guid? RoomId,
    string? RoomNumber,
    Guid? BookingId,
    string Text,
    string Status,
    DateTime CreatedAt,
    DateTime? DoneAt
);

/// RoomId is optional: a general reminder not tied to any room leaves it null.
public record CreateRoomTaskRequestDto(Guid? RoomId, string Text, Guid? BookingId);

/// Editing a task rewrites its text and may move it to another room — or to no room
/// at all, by leaving RoomId null. Status and booking stay as they were.
public record UpdateRoomTaskRequestDto(Guid? RoomId, string Text);

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

/// What this one group is charged. The rate is what the owner edits; TotalGrosze
/// comes along so a flat, negotiated amount can be typed over the arithmetic
/// (null recomputes it as rate × headcount × nights).
public record UpdateBookingPricingRequestDto(
    long PricePerPersonPerNightGrosze,
    long DepositGrosze,
    long? TotalGrosze
);

/// Unpaid / DepositPaid / Paid — the owner's record of what has actually arrived.
public record SetBookingPaymentStateRequestDto(string PaymentState);

/// The one state the panel shows, folding the booking's status together with what
/// has been paid — see BookingState.
public record SetBookingStateRequestDto(string State);

/// The centre's rates. UpdatedAt is null while the configured defaults are still
/// in force and nothing has been saved in the panel.
public record PricingDefaultsDto(
    long PricePerPersonPerNightGrosze,
    long DepositPerPersonPerNightGrosze,
    DateTime? UpdatedAt
);

public record UpdatePricingDefaultsRequestDto(
    long PricePerPersonPerNightGrosze,
    long DepositPerPersonPerNightGrosze
);

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
