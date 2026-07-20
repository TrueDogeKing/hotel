using CampCenter.Application.DTOs.Public;
using CampCenter.Application.Interfaces;
using CampCenter.Domain.Repositories;

namespace CampCenter.Application.Services;

public class AvailabilityService : IAvailabilityService
{
    private readonly ICampSessionRepository _sessions;
    private readonly IRoomRepository _rooms;
    private readonly IBookingRepository _bookings;

    public AvailabilityService(
        ICampSessionRepository sessions,
        IRoomRepository rooms,
        IBookingRepository bookings
    )
    {
        _sessions = sessions;
        _rooms = rooms;
        _bookings = bookings;
    }

    public async Task<List<PublicSessionDto>> GetPublicSessionsAsync(
        int? headcount,
        CancellationToken cancellationToken = default
    )
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var sessions = await _sessions.GetPublishedUpcomingAsync(today, cancellationToken);

        var result = new List<PublicSessionDto>();
        foreach (var session in sessions)
        {
            var free = await GetFreeRoomsByCapacityAsync(session.Id, cancellationToken);
            var remaining = (int)RoomMixCalculator.TotalCapacity(free);
            Dictionary<int, int>? mix = null;
            bool? fits = null;
            if (headcount is > 0)
            {
                mix = RoomMixCalculator.SuggestMix(headcount.Value, free);
                fits = mix is not null;
            }

            result.Add(
                new PublicSessionDto(
                    session.Id,
                    session.Name,
                    session.StartDate,
                    session.EndDate,
                    session.PricePerPersonGrosze,
                    session.DepositPerPersonGrosze,
                    remaining,
                    free,
                    fits,
                    mix
                )
            );
        }

        return result;
    }

    public async Task<Dictionary<int, int>> GetFreeRoomsByCapacityAsync(
        Guid campSessionId,
        CancellationToken cancellationToken = default
    )
    {
        var activeRooms = await _rooms.GetActiveAsync(cancellationToken);
        var assigned = (
            await _bookings.GetLiveAssignedRoomIdsAsync(campSessionId, cancellationToken)
        ).ToHashSet();

        return activeRooms
            .Where(r => !assigned.Contains(r.Id))
            .GroupBy(r => r.Capacity)
            .ToDictionary(g => g.Key, g => g.Count());
    }
}
