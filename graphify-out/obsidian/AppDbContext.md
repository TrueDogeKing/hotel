---
source_file: "src/CampCenter.Infrastructure/Persistence/AppDbContext.cs"
type: "code"
community: "Booking Persistence & Entities"
location: "L6"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Booking_Persistence__Entities
---

# AppDbContext

## Context

_Source: `src/CampCenter.Infrastructure/Persistence/AppDbContext.cs` (defined near L6; showing L4–L34 of 34)._

```csharp
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
```

## Connections
- [[.CreateDbContext()]] - `references` [EXTRACTED]
- [[.OnModelCreating()]] - `method` [EXTRACTED]
- [[AdminUser]] - `references` [EXTRACTED]
- [[AdminUserRepository]] - `references` [EXTRACTED]
- [[AppDbContext.cs]] - `contains` [EXTRACTED]
- [[Booking]] - `references` [EXTRACTED]
- [[BookingRepository]] - `references` [EXTRACTED]
- [[BookingRoomAssignment]] - `references` [EXTRACTED]
- [[Closure]] - `references` [EXTRACTED]
- [[ClosureRepository]] - `references` [EXTRACTED]
- [[DbContext]] - `inherits` [EXTRACTED]
- [[DbSet]] - `references` [EXTRACTED]
- [[DesignTimeDbContextFactory]] - `references` [EXTRACTED]
- [[Payment]] - `references` [EXTRACTED]
- [[RefreshToken]] - `references` [EXTRACTED]
- [[RefreshTokenRepository]] - `references` [EXTRACTED]
- [[Room_1]] - `references` [EXTRACTED]
- [[RoomRepository]] - `references` [EXTRACTED]
- [[RoomTask_1]] - `references` [EXTRACTED]
- [[RoomTaskRepository]] - `references` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Booking_Persistence__Entities