using CampCenter.Domain.Entities;

namespace CampCenter.Domain.Repositories;

public interface ICampSessionRepository
{
    Task<List<CampSession>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<CampSession?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// True when another Published session overlaps the [start, end] date range.
    Task<bool> AnyPublishedOverlappingAsync(
        Guid excludeSessionId,
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken = default
    );

    /// True when any booking references the session.
    Task<bool> HasBookingsAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(CampSession session, CancellationToken cancellationToken = default);

    void Remove(CampSession session);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
