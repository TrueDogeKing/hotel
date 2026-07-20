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
    string SessionName,
    Guid CampSessionId,
    DateOnly StartDate,
    DateOnly EndDate,
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

/// One row per room in the occupancy grid of a session.
public record RoomOccupancyDto(
    Guid RoomId,
    string RoomNumber,
    int Capacity,
    bool IsActive,
    Guid? BookingId,
    string? OrganizationName,
    string? BookingStatus,
    int? PeopleCount,
    int OpenTaskCount
);

public record SessionOccupancyDto(
    Guid SessionId,
    string SessionName,
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
    Guid? CampSessionId,
    Guid? BookingId,
    string Text,
    string Status,
    DateTime CreatedAt,
    DateTime? DoneAt
);

public record CreateRoomTaskRequestDto(
    Guid RoomId,
    string Text,
    Guid? CampSessionId,
    Guid? BookingId
);

public record ReassignmentEntryDto(Guid RoomId, int PeopleCount);

public record ReassignBookingRequestDto(List<ReassignmentEntryDto> Assignments);

public record DashboardSessionDto(
    Guid Id,
    string Name,
    DateOnly StartDate,
    DateOnly EndDate,
    int TotalBeds,
    int OccupiedBeds,
    int BookingCount
);

public record DashboardDto(
    List<DashboardSessionDto> UpcomingSessions,
    int PendingDepositCount,
    int OverdueFinalCount,
    int OpenTaskCount
);
