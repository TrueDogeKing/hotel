using CampCenter.Domain.Entities;
using CampCenter.Domain.Exceptions;
using CampCenter.Domain.Repositories;
using CampCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CampCenter.Infrastructure.Repositories;

public class RoomCleaningRepository : IRoomCleaningRepository
{
    private readonly AppDbContext _db;

    public RoomCleaningRepository(AppDbContext db) => _db = db;

    public Task<List<RoomCleaning>> ListForDateAsync(
        DateOnly date,
        CancellationToken cancellationToken = default
    ) => _db.RoomCleanings.Where(c => c.Date == date).ToListAsync(cancellationToken);

    public async Task<Dictionary<DateOnly, int>> CountDoneByDateAsync(
        DateOnly from,
        DateOnly to,
        CancellationToken cancellationToken = default
    ) =>
        (
            await _db
                .RoomCleanings.Where(c =>
                    c.Date >= from && c.Date <= to && c.Status == RoomCleaningStatus.Done
                )
                .GroupBy(c => c.Date)
                .Select(g => new { g.Key, Count = g.Count() })
                .ToListAsync(cancellationToken)
        ).ToDictionary(x => x.Key, x => x.Count);

    public Task<RoomCleaning?> GetAsync(
        Guid roomId,
        DateOnly date,
        CancellationToken cancellationToken = default
    ) =>
        _db.RoomCleanings.FirstOrDefaultAsync(
            c => c.RoomId == roomId && c.Date == date,
            cancellationToken
        );

    public async Task AddAsync(
        RoomCleaning cleaning,
        CancellationToken cancellationToken = default
    ) => await _db.RoomCleanings.AddAsync(cleaning, cancellationToken);

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
            when (ex.InnerException
                    is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation }
            )
        {
            // Two people ticked the same room at the same moment. The index held; the
            // caller retries and finds the row the other one wrote.
            throw new ConflictException(
                "That room's cleaning was just updated by someone else. Reload and try again."
            );
        }
    }
}
