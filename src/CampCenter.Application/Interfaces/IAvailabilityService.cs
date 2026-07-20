using CampCenter.Application.DTOs.Public;

namespace CampCenter.Application.Interfaces;

public interface IAvailabilityService
{
    /// Published upcoming sessions with their free-room breakdown; when a
    /// headcount is given each session also carries Fits and a SuggestedMix.
    Task<List<PublicSessionDto>> GetPublicSessionsAsync(
        int? headcount,
        CancellationToken cancellationToken = default
    );

    /// Free (active, unassigned) room counts by capacity for one session.
    Task<Dictionary<int, int>> GetFreeRoomsByCapacityAsync(
        Guid campSessionId,
        CancellationToken cancellationToken = default
    );
}
