using CampCenter.Application.Interfaces;
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

    /// What the API tried to email, for tests that care — and, for every other
    /// test, the reason the log is not full of SMTP stack traces.
    public RecordingEmailSender Email { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Every test hits the API from the same loopback address, so all three per-IP
        // limiters see the whole suite as one client. Raise them all: otherwise
        // adding tests makes unrelated ones fail with 429 once the suite crosses the
        // production thresholds (100 req/10s global, 20/60s public booking, 5/30s auth).
        // A dedicated test enables a strict limit on its own isolated host
        // (WithWebHostBuilder).
        builder.UseSetting("RateLimiting:Auth:PermitLimit", "100000");

        // WebApplicationFactory runs as Development, so it would pick up the dev
        // auto-migrate/auto-seed flags — but the host is built (and would seed)
        // before InitializeAsync has pointed it at the container and migrated it.
        // Take both steps into our own hands below instead.
        builder.UseSetting("Database:MigrateAutomatically", "false");
        builder.UseSetting("Database:SeedAutomatically", "false");
        builder.UseSetting("RateLimiting:Global:PermitLimit", "100000");
        builder.UseSetting("RateLimiting:PublicBooking:PermitLimit", "100000");

        builder.ConfigureTestServices(services =>
        {
            // Replace the application's DbContext (which points at the configured connection
            // string) with one pointing at the test container.
            services.RemoveAll<DbContextOptions<AppDbContext>>();
            services.RemoveAll<AppDbContext>();
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(_database.GetConnectionString())
            );

            // No SMTP server here, and none wanted: the real sender would spend a
            // connect timeout per email and log a failure for each one.
            services.RemoveAll<IEmailSender>();
            services.AddSingleton<IEmailSender>(Email);
        });
    }

    public async Task InitializeAsync()
    {
        await _database.StartAsync();

        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();

        // Seed the same baseline the app seeds on startup: the default admin user
        // and the center's default meal slots.
        await DataSeeder.SeedAdminUserAsync(Services);
        await DataSeeder.SeedMealTimeDefaultsAsync(Services);
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _database.DisposeAsync();
        await base.DisposeAsync();
    }
}
