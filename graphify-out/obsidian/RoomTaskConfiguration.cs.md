---
source_file: "src/CampCenter.Infrastructure/Persistence/Configurations/RoomTaskConfiguration.cs"
type: "code"
community: "Booking Persistence & Entities"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Booking_Persistence__Entities
---

# RoomTaskConfiguration.cs

## Context

_Source: `src/CampCenter.Infrastructure/Persistence/Configurations/RoomTaskConfiguration.cs` (defined near L1; showing L1–L42 of 42)._

```csharp
using CampCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampCenter.Infrastructure.Persistence.Configurations;

public class RoomTaskConfiguration : IEntityTypeConfiguration<RoomTask>
{
    public void Configure(EntityTypeBuilder<RoomTask> builder)
    {
        builder.ToTable("RoomTasks");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Text).IsRequired().HasMaxLength(1000);

        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(8);

        // The housekeeping list filters by status.
        builder.HasIndex(x => x.Status);

        builder
            .HasOne(x => x.Room)
            .WithMany()
            .HasForeignKey(x => x.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Booking)
            .WithMany()
            .HasForeignKey(x => x.BookingId)
            .OnDelete(DeleteBehavior.SetNull);

        // Optimistic concurrency via the PostgreSQL "xmin" system column.
        builder
            .Property(x => x.RowVersion)
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .ValueGeneratedOnAddOrUpdate()
            .IsConcurrencyToken();
    }
}
```

## Connections
- [[CampCenter.Domain.Entities]] - `imports` [EXTRACTED]
- [[CampCenter.Infrastructure.Persistence.Configurations]] - `contains` [EXTRACTED]
- [[RoomTaskConfiguration]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Booking_Persistence__Entities