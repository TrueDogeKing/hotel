using CampCenter.Domain.Entities;

namespace CampCenter.Domain.Repositories;

public interface IMealTimeDefaultRepository
{
    /// All defaults (active and inactive) ordered by SortOrder, then start time.
    Task<List<MealTimeDefault>> GetAllAsync(CancellationToken cancellationToken = default);

    /// Only the active defaults — the ones meal generation works from.
    Task<List<MealTimeDefault>> GetActiveAsync(CancellationToken cancellationToken = default);

    Task<MealTimeDefault?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// Whether any schedule entry was generated from this default. Deleting a
    /// referenced default deactivates it instead, so history survives.
    Task<bool> IsReferencedAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(MealTimeDefault mealTime, CancellationToken cancellationToken = default);

    void Remove(MealTimeDefault mealTime);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
