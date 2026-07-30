using CampCenter.Application.DTOs.AdminPanel;

namespace CampCenter.Application.Interfaces;

public interface IHousekeepingService
{
    /// The rooms needing attention on one day — groups leaving, groups arriving, and the
    /// rooms that are both — merged with how far the work has got.
    Task<HousekeepingDayDto> GetDayAsync(
        DateOnly date,
        CancellationToken cancellationToken = default
    );

    /// Rooms-to-do and rooms-done per day over [from, to], for the day strip.
    Task<HousekeepingRangeDto> GetRangeAsync(
        DateOnly from,
        DateOnly to,
        CancellationToken cancellationToken = default
    );

    /// Records progress on one room for one day, creating the row on first touch.
    /// Rejects a room that has nothing to do that day, so a stale page cannot write
    /// progress against a booking that has since moved.
    Task<HousekeepingRoomDto> SetStatusAsync(
        Guid roomId,
        DateOnly date,
        SetRoomCleaningRequestDto request,
        Guid adminUserId,
        CancellationToken cancellationToken = default
    );
}
