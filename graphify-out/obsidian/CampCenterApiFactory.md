---
source_file: "tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs"
type: "code"
community: "Integration Test Harness"
location: "L17"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Integration_Test_Harness
---

# CampCenterApiFactory

## Context

_Source: `tests/CampCenter.IntegrationTests/CampCenterApiFactory.cs` (defined near L17; showing L15–L58 of 58)._

```csharp
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
        await db.Database.MigrateAsync();

        // Seed the same baseline the app seeds on startup: the default admin user.
        await DataSeeder.SeedAdminUserAsync(Services);
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _database.DisposeAsync();
        await base.DisposeAsync();
    }
}
```

## Connections
- [[.ConfigureWebHost()]] - `method` [EXTRACTED]
- [[.DisposeAsync()]] - `method` [EXTRACTED]
- [[.InitializeAsync()]] - `method` [EXTRACTED]
- [[ApiCollection]] - `references` [EXTRACTED]
- [[CampCenterApiFactory.cs]] - `contains` [EXTRACTED]
- [[IAsyncLifetime]] - `implements` [EXTRACTED]
- [[IntegrationTestBase]] - `references` [EXTRACTED]
- [[PostgreSqlContainer]] - `references` [EXTRACTED]
- [[Program]] - `references` [EXTRACTED]
- [[WebApplicationFactory]] - `inherits` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Integration_Test_Harness