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

    /// Creates the center's default meal slots if none exist yet. These drive meal
    /// generation for every confirmed booking, so an empty table would silently
    /// mean "no meals ever".
    public static async Task SeedMealTimeDefaultsAsync(
        IServiceProvider services,
        CancellationToken cancellationToken = default
    )
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (await db.MealTimeDefaults.AnyAsync(cancellationToken))
        {
            return;
        }

        var now = DateTime.UtcNow;
        db.MealTimeDefaults.AddRange(
            new MealTimeDefault
            {
                Id = Guid.NewGuid(),
                MealKind = MealKind.Breakfast,
                Label = "Śniadanie",
                StartTime = new TimeOnly(8, 0),
                EndTime = new TimeOnly(9, 0),
                CreatedAt = now,
            },
            new MealTimeDefault
            {
                Id = Guid.NewGuid(),
                MealKind = MealKind.Lunch,
                Label = "Obiad",
                StartTime = new TimeOnly(13, 0),
                EndTime = new TimeOnly(14, 0),
                CreatedAt = now,
            },
            new MealTimeDefault
            {
                Id = Guid.NewGuid(),
                MealKind = MealKind.Dinner,
                Label = "Kolacja",
                StartTime = new TimeOnly(18, 0),
                EndTime = new TimeOnly(19, 0),
                CreatedAt = now,
            }
        );

        await db.SaveChangesAsync(cancellationToken);
    }
}
