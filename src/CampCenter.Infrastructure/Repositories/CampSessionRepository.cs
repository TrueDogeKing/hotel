using CampCenter.Domain.Entities;
using CampCenter.Domain.Repositories;
using CampCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CampCenter.Infrastructure.Repositories;

public class CampSessionRepository : ICampSessionRepository
{
    private readonly AppDbContext _db;

    public CampSessionRepository(AppDbContext db) => _db = db;

    public Task<List<CampSession>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _db.CampSessions.OrderBy(s => s.StartDate).ToListAsync(cancellationToken);

    public Task<CampSession?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    ) => _db.CampSessions.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public Task<bool> AnyPublishedOverlappingAsync(
        Guid excludeSessionId,
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken = default
    ) =>
        _db.CampSessions.AnyAsync(
            s =>
                s.Id != excludeSessionId
                && s.Status == CampSessionStatus.Published
                && s.StartDate <= endDate
                && startDate <= s.EndDate,
            cancellationToken
        );

    public Task<bool> HasBookingsAsync(Guid id, CancellationToken cancellationToken = default) =>
        _db.Bookings.AnyAsync(b => b.CampSessionId == id, cancellationToken);

    public async Task AddAsync(
        CampSession session,
        CancellationToken cancellationToken = default
    ) => await _db.CampSessions.AddAsync(session, cancellationToken);

    public void Remove(CampSession session) => _db.CampSessions.Remove(session);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _db.SaveChangesAsync(cancellationToken);
}
