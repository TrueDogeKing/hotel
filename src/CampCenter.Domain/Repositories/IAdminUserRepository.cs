using CampCenter.Domain.Entities;

namespace CampCenter.Domain.Repositories;

public interface IAdminUserRepository
{
    Task<AdminUser?> GetByLoginAsync(string login, CancellationToken cancellationToken = default);

    Task<AdminUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
