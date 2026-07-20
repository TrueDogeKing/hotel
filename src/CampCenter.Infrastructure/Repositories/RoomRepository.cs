using CampCenter.Domain.Entities;
using CampCenter.Domain.Repositories;
using CampCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CampCenter.Infrastructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly AppDbContext _db;

    public RoomRepository(AppDbContext db) => _db = db;

    public Task<List<Room>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _db.Rooms.OrderBy(r => r.Number).ToListAsync(cancellationToken);

    public Task<List<Room>> GetActiveAsync(CancellationToken cancellationToken = default) =>
        _db.Rooms.Where(r => r.IsActive).OrderBy(r => r.Number).ToListAsync(cancellationToken);

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
