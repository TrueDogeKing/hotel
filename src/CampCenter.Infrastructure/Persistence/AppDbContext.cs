using CampCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CampCenter.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<AdminUser> AdminUsers => Set<AdminUser>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<Room> Rooms => Set<Room>();

    public DbSet<Closure> Closures => Set<Closure>();

    public DbSet<Booking> Bookings => Set<Booking>();

    public DbSet<BookingRoomAssignment> BookingRoomAssignments => Set<BookingRoomAssignment>();

    public DbSet<RoomTask> RoomTasks => Set<RoomTask>();

    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Wczytaj wszystkie konfiguracje IEntityTypeConfiguration z tej assembly.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
