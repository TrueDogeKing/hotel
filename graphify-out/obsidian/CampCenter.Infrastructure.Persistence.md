---
source_file: "src/CampCenter.Infrastructure/Persistence/AppDbContext.cs"
type: "code"
community: "Domain & Infra Namespaces"
location: "L4"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain__Infra_Namespaces
---

# CampCenter.Infrastructure.Persistence

## Context

_Source: `src/CampCenter.Infrastructure/Persistence/AppDbContext.cs` (defined near L4; showing L2–L34 of 34)._

```csharp
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
```

## Connections
- [[20260719142059_InitialAuth.Designer.cs]] - `imports` [EXTRACTED]
- [[20260719143540_CoreDomain.Designer.cs]] - `imports` [EXTRACTED]
- [[AdminUserRepository.cs]] - `imports` [EXTRACTED]
- [[AppDbContext.cs]] - `contains` [EXTRACTED]
- [[AppDbContextModelSnapshot.cs]] - `imports` [EXTRACTED]
- [[BookingRepository.cs]] - `imports` [EXTRACTED]
- [[CampCenterApiFactory.cs]] - `imports` [EXTRACTED]
- [[ClosureRepository.cs]] - `imports` [EXTRACTED]
- [[DependencyInjection.cs_1]] - `imports` [EXTRACTED]
- [[DesignTimeDbContextFactory.cs]] - `contains` [EXTRACTED]
- [[Program.cs]] - `imports` [EXTRACTED]
- [[RefreshTokenRepository.cs]] - `imports` [EXTRACTED]
- [[RoomRepository.cs]] - `imports` [EXTRACTED]
- [[RoomTaskRepository.cs]] - `imports` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain__Infra_Namespaces