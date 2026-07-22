---
source_file: "src/CampCenter.Infrastructure/Persistence/Configurations/AdminUserConfiguration.cs"
type: "code"
community: "Booking Persistence & Entities"
location: "L5"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Booking_Persistence__Entities
---

# CampCenter.Infrastructure.Persistence.Configurations

## Context

_Source: `src/CampCenter.Infrastructure/Persistence/Configurations/AdminUserConfiguration.cs` (defined near L5; showing L3–L33 of 33)._

```csharp
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampCenter.Infrastructure.Persistence.Configurations;

/// <summary>Konfiguracja EF Core dla encji <see cref="AdminUser"/>.</summary>
public class AdminUserConfiguration : IEntityTypeConfiguration<AdminUser>
{
    public void Configure(EntityTypeBuilder<AdminUser> builder)
    {
        builder.ToTable("AdminUsers");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Login).IsRequired().HasMaxLength(32);

        // Unikalny indeks na login (przechowywany małymi literami).
        builder.HasIndex(u => u.Login).IsUnique();

        builder.Property(u => u.PasswordHash).IsRequired();

        builder.Property(u => u.CreatedAt).IsRequired();

        // Optimistic concurrency oparte o systemową kolumnę PostgreSQL "xmin".
        builder
            .Property(u => u.RowVersion)
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .ValueGeneratedOnAddOrUpdate()
            .IsConcurrencyToken();
    }
}
```

## Connections
- [[AdminUserConfiguration.cs]] - `contains` [EXTRACTED]
- [[BookingConfiguration.cs]] - `contains` [EXTRACTED]
- [[BookingRoomAssignmentConfiguration.cs]] - `contains` [EXTRACTED]
- [[ClosureConfiguration.cs]] - `contains` [EXTRACTED]
- [[PaymentConfiguration.cs]] - `contains` [EXTRACTED]
- [[RefreshTokenConfiguration.cs]] - `contains` [EXTRACTED]
- [[RoomConfiguration.cs]] - `contains` [EXTRACTED]
- [[RoomTaskConfiguration.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Booking_Persistence__Entities