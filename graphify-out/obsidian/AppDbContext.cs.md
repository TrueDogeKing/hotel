---
source_file: "src/CampCenter.Infrastructure/Persistence/AppDbContext.cs"
type: "code"
community: "Domain & Infra Namespaces"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Domain__Infra_Namespaces
---

# AppDbContext.cs

## Context

_Source: `src/CampCenter.Infrastructure/Persistence/AppDbContext.cs` (defined near L1; showing L1–L34 of 34)._

```csharp
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
```

## Connections
- [[AppDbContext]] - `contains` [EXTRACTED]
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Persistence]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Domain__Infra_Namespaces