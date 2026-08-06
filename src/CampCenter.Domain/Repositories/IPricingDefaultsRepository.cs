using CampCenter.Domain.Entities;

namespace CampCenter.Domain.Repositories;

public interface IPricingDefaultsRepository
{
    /// The single settings row, or null before it has ever been written.
    Task<PricingDefaults?> GetAsync(CancellationToken cancellationToken = default);

    Task AddAsync(PricingDefaults defaults, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
