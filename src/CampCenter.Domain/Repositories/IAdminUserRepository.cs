using CampCenter.Domain.Entities;

namespace CampCenter.Domain.Repositories;

public interface IAdminUserRepository
{
    Task<AdminUser?> GetByLoginAsync(string login, CancellationToken cancellationToken = default);

    Task<AdminUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// All accounts, oldest first — the panel's user list.
    Task<List<AdminUser>> ListAsync(CancellationToken cancellationToken = default);

    /// How many accounts hold this role. Counted before a delete or a demotion, to
    /// refuse the one that would leave the panel with no administrator in it.
    Task<int> CountByRoleAsync(AdminUserRole role, CancellationToken cancellationToken = default);

    Task AddAsync(AdminUser user, CancellationToken cancellationToken = default);

    void Remove(AdminUser user);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
