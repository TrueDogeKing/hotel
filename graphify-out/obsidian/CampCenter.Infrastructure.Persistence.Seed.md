---
source_file: "src/CampCenter.Infrastructure/Persistence/Seed/DataSeeder.cs"
type: "code"
community: "Rate Limiting & Startup"
location: "L7"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Rate_Limiting__Startup
---

# CampCenter.Infrastructure.Persistence.Seed

## Context

_Source: `src/CampCenter.Infrastructure/Persistence/Seed/DataSeeder.cs` (defined near L7; showing L5–L45 of 45)._

```csharp
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
- [[CampCenterApiFactory.cs]] - `imports` [EXTRACTED]
- [[DataSeeder.cs]] - `contains` [EXTRACTED]
- [[Program.cs]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Rate_Limiting__Startup