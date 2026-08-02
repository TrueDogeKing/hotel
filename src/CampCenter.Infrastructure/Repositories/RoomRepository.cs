using CampCenter.Domain.Entities;
using CampCenter.Domain.Repositories;
using CampCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CampCenter.Infrastructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly AppDbContext _db;

    public RoomRepository(AppDbContext db) => _db = db;

    public async Task<List<Room>> GetAllAsync(CancellationToken cancellationToken = default) =>
        (await _db.Rooms.ToListAsync(cancellationToken))
            .OrderBy(r => r.Number, RoomNumberComparer.Instance)
            .ToList();

    public async Task<List<Room>> GetActiveAsync(CancellationToken cancellationToken = default) =>
        (await _db.Rooms.Where(r => r.IsActive).ToListAsync(cancellationToken))
            .OrderBy(r => r.Number, RoomNumberComparer.Instance)
            .ToList();

    public Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _db.Rooms.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    public Task<Room?> GetByNumberAsync(
        string number,
        CancellationToken cancellationToken = default
    ) => _db.Rooms.FirstOrDefaultAsync(r => r.Number == number, cancellationToken);

    public Task<bool> HasAssignmentsAsync(Guid id, CancellationToken cancellationToken = default) =>
        _db.BookingRoomAssignments.AnyAsync(a => a.RoomId == id, cancellationToken);

    public async Task AddAsync(Room room, CancellationToken cancellationToken = default) =>
        await _db.Rooms.AddAsync(room, cancellationToken);

    public void Remove(Room room) => _db.Rooms.Remove(room);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _db.SaveChangesAsync(cancellationToken);
}
