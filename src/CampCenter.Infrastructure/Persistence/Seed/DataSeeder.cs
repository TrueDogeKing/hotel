using CampCenter.Application.Interfaces;
using CampCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CampCenter.Infrastructure.Persistence.Seed;

public static class DataSeeder
{
    /// Creates the default administrator account if it doesn't already exist.
    /// Login details come from the "Admin" configuration section (with default values).
    /// <param name="services">Application service provider.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public static async Task SeedAdminUserAsync(
        IServiceProvider services,
        CancellationToken cancellationToken = default
    )
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        var login = configuration["Admin:Login"] ?? "admin";
        var password = configuration["Admin:Password"] ?? "Admin123!";

        if (await db.AdminUsers.AnyAsync(u => u.Login == login, cancellationToken))
        {
            return;
        }

        db.AdminUsers.Add(
            new AdminUser
            {
                Id = Guid.NewGuid(),
                Login = login,
                PasswordHash = passwordHasher.Hash(password),
                CreatedAt = DateTime.UtcNow,
            }
        );

        await db.SaveChangesAsync(cancellationToken);
    }
}
