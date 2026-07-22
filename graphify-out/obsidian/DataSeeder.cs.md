---
source_file: "src/CampCenter.Infrastructure/Persistence/Seed/DataSeeder.cs"
type: "code"
community: "Rate Limiting & Startup"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Rate_Limiting__Startup
---

# DataSeeder.cs

## Context

_Source: `src/CampCenter.Infrastructure/Persistence/Seed/DataSeeder.cs` (defined near L1; showing L1–L45 of 45)._

```csharp
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
```

## Connections
- [[CampCenter.Application.Interfaces]] - `imports` [EXTRACTED]
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Persistence.Seed]] - `contains` [EXTRACTED]
- [[DataSeeder]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Rate_Limiting__Startup