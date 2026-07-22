---
source_file: "tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs"
type: "code"
community: "Rate Limiting & Startup"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Rate_Limiting__Startup
---

# CampCenterApiFactory.cs

## Context

_Source: `tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs` (defined near L1; showing L1–L46 of 58)._

```csharp
using CampCenter.Infrastructure.Persistence;
using CampCenter.Infrastructure.Persistence.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace CampCenter.IntegrationTests;

/// Boots the real API against a throwaway PostgreSQL container (Testcontainers). Using a real
/// PostgreSQL is required because optimistic concurrency is mapped onto the "xmin" system column,
/// which an in-memory provider cannot reproduce. The schema is created from the real EF migrations
/// and the database is seeded with the same DataSeeder the application uses on startup.
public class CampCenterApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _database = new PostgreSqlBuilder(
        "postgres:16-alpine"
    ).Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // The shared suite performs many admin logins; raise the auth rate limit so it never trips.
        // A dedicated test enables a strict limit on its own isolated host (WithWebHostBuilder).
        builder.UseSetting("RateLimiting:Auth:PermitLimit", "100000");

        builder.ConfigureTestServices(services =>
        {
            // Replace the application's DbContext (which points at the configured connection
            // string) with one pointing at the test container.
            services.RemoveAll<DbContextOptions<AppDbContext>>();
            services.RemoveAll<AppDbContext>();
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(_database.GetConnectionString())
            );
        });
    }

    public async Task InitializeAsync()
    {
        await _database.StartAsync();

        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
```

## Connections
- [[CampCenter.Infrastructure.Persistence]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Persistence.Seed]] - `imports` [EXTRACTED]
- [[CampCenter.IntegrationTests]] - `contains` [EXTRACTED]
- [[CampCenterApiFactory]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Rate_Limiting__Startup