using CampCenter.Domain.Entities;
using CampCenter.Domain.Repositories;
using CampCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CampCenter.Infrastructure.Repositories;

public class PricingDefaultsRepository : IPricingDefaultsRepository
{
    private readonly AppDbContext _db;

    public PricingDefaultsRepository(AppDbContext db) => _db = db;

    public Task<PricingDefaults?> GetAsync(CancellationToken cancellationToken = default) =>
        _db.PricingDefaults.FirstOrDefaultAsync(cancellationToken);

    public async Task AddAsync(
        PricingDefaults defaults,
        CancellationToken cancellationToken = default
    ) => await _db.PricingDefaults.AddAsync(defaults, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _db.SaveChangesAsync(cancellationToken);
}
