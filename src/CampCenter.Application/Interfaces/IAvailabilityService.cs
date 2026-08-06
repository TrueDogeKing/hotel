using CampCenter.Application.DTOs.Public;

namespace CampCenter.Application.Interfaces;

public interface IAvailabilityService
{
    /// Availability for a requested date range [start, end); when a headcount is
    /// given it also carries Fits, a SuggestedMix and the computed amounts.
    Task<AvailabilityDto> GetAvailabilityAsync(
        DateOnly start,
        DateOnly end,
        int? headcount,
        int? supervisors,
        CancellationToken cancellationToken = default
    );

    /// Night-by-night availability over [start, end] — both ends inclusive,
    /// because the caller is a calendar drawing those days, not a stay.
    ///
    /// Answers one question per night: could a group of this size sleep here?
    /// Computed from one read of the rooms, closures and assignments rather than a
    /// query per night, so a six-week grid costs the same as a single range.
    Task<AvailabilityCalendarDto> GetCalendarAsync(
        DateOnly start,
        DateOnly end,
        int? headcount,
        CancellationToken cancellationToken = default
    );

    /// Free (active, unassigned, not-closed) room counts by capacity for the
    /// stay [start, end). <paramref name="excludeBookingId"/> ignores one
    /// booking's own rooms (used when reassigning).
    Task<Dictionary<int, int>> GetFreeRoomsByCapacityAsync(
        DateOnly start,
        DateOnly end,
        Guid? excludeBookingId = null,
        CancellationToken cancellationToken = default
    );

    /// The reason the whole center is closed on some day of [start, end), or null
    /// if it is open throughout.
    Task<string?> GetCenterClosureReasonAsync(
        DateOnly start,
        DateOnly end,
        CancellationToken cancellationToken = default
    );

    /// Room ids that cannot be used for the stay [start, end) — already booked or
    /// blocked by a closure (all active rooms when the whole center is closed).
    /// <paramref name="excludeBookingId"/> ignores one booking's own rooms.
    Task<HashSet<Guid>> GetBlockedRoomIdsAsync(
        DateOnly start,
        DateOnly end,
        Guid? excludeBookingId = null,
        CancellationToken cancellationToken = default
    );
}
