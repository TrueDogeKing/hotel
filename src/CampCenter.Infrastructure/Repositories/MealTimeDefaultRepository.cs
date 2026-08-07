using CampCenter.Domain.Entities;
using CampCenter.Domain.Repositories;
using CampCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CampCenter.Infrastructure.Repositories;

public class MealTimeDefaultRepository : IMealTimeDefaultRepository
{
    private readonly AppDbContext _db;

    public MealTimeDefaultRepository(AppDbContext db) => _db = db;

    public Task<List<MealTimeDefault>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _db.MealTimeDefaults.OrderBy(m => m.StartTime).ToListAsync(cancellationToken);

    public Task<List<MealTimeDefault>> GetActiveAsync(
        CancellationToken cancellationToken = default
    ) =>
        _db
            .MealTimeDefaults.Where(m => m.IsActive)
            .OrderBy(m => m.StartTime)
            .ToListAsync(cancellationToken);

    public Task<MealTimeDefault?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    ) => _db.MealTimeDefaults.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

    public Task<bool> IsReferencedAsync(Guid id, CancellationToken cancellationToken = default) =>
        _db.ScheduleEntries.AnyAsync(e => e.MealTimeDefaultId == id, cancellationToken);

    public async Task AddAsync(
        MealTimeDefault mealTime,
        CancellationToken cancellationToken = default
    ) => await _db.MealTimeDefaults.AddAsync(mealTime, cancellationToken);

    public void Remove(MealTimeDefault mealTime) => _db.MealTimeDefaults.Remove(mealTime);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _db.SaveChangesAsync(cancellationToken);
}
