using CampCenter.Domain.Entities;
using CampCenter.Domain.Repositories;
using CampCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CampCenter.Infrastructure.Repositories;

/// Implementacja <see cref="IAdminUserRepository"/> oparta o <see cref="AppDbContext"/>.
public class AdminUserRepository : IAdminUserRepository
{
    private readonly AppDbContext _db;

    /// Creates repository with database context.
    public AdminUserRepository(AppDbContext db) => _db = db;

    public Task<AdminUser?> GetByLoginAsync(
        string login,
        CancellationToken cancellationToken = default
    ) => _db.AdminUsers.FirstOrDefaultAsync(u => u.Login == login, cancellationToken);

    public Task<AdminUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _db.AdminUsers.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public Task<List<AdminUser>> ListAsync(CancellationToken cancellationToken = default) =>
        _db.AdminUsers.OrderBy(u => u.CreatedAt).ThenBy(u => u.Id).ToListAsync(cancellationToken);

    public Task<int> CountByRoleAsync(
        AdminUserRole role,
        CancellationToken cancellationToken = default
    ) => _db.AdminUsers.CountAsync(u => u.Role == role, cancellationToken);

    public async Task AddAsync(AdminUser user, CancellationToken cancellationToken = default) =>
        await _db.AdminUsers.AddAsync(user, cancellationToken);

    public void Remove(AdminUser user) => _db.AdminUsers.Remove(user);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _db.SaveChangesAsync(cancellationToken);
}
