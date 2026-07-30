namespace CampCenter.Application.DTOs.AdminPanel;

/// One room on housekeeping's list for a day: why it needs doing, the groups on either
/// side of it, and how far the work has got.
public record HousekeepingRoomDto(
    Guid RoomId,
    string RoomNumber,
    int Capacity,
    /// Departure / Arrival / Turnaround — derived from the room's assignments each time
    /// the day is read, so it follows a reassignment.
    string Kind,
    /// Pending / InProgress / Done. Pending for a room nobody has touched yet.
    string Status,
    /// The group leaving that morning: null for a room that was already free.
    Guid? OutgoingBookingId,
    string? OutgoingOrganizationName,
    /// Beds to strip — the outgoing group's occupancy of this room.
    int? OutgoingPeopleCount,
    /// The group moving in that day: null when nobody is due.
    Guid? IncomingBookingId,
    string? IncomingOrganizationName,
    /// Beds to make up — the incoming group's occupancy of this room.
    int? IncomingPeopleCount,
    string? Note,
    DateTime? DoneAt,
    /// Open RoomTasks on this room ("dostawić łóżko"), so the list says when a room needs
    /// more than cleaning. The tasks themselves live on the occupancy page.
    int OpenTaskCount,
    /// Blocked by a closure — worth knowing before walking over to it.
    bool Closed,
    string? ClosureReason,
    uint RowVersion
);

/// Everything housekeeping needs for one day, already ordered: turnarounds first (a room
/// that has to be emptied and remade before evening), then departures, then arrivals.
public record HousekeepingDayDto(
    DateOnly Date,
    List<HousekeepingRoomDto> Rooms,
    int TurnaroundCount,
    int DepartureCount,
    int ArrivalCount,
    int DoneCount
);

/// Rooms done out of rooms needing attention, per day — the strip above the day view, so
/// a glance shows which day still has work outstanding.
public record HousekeepingDaySummaryDto(DateOnly Date, int RoomCount, int DoneCount);

public record HousekeepingRangeDto(DateOnly From, DateOnly To, List<HousekeepingDaySummaryDto> Days);

/// Pending / InProgress / Done, plus an optional note from whoever did the room.
public record SetRoomCleaningRequestDto(string Status, string? Note);
