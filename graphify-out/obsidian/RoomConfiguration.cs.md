---
source_file: "src/CampCenter.Infrastructure/Persistence/Configurations/RoomConfiguration.cs"
type: "code"
community: "Room Management"
location: "L1"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Management
---

# RoomConfiguration.cs

## Context

_Source: `src/CampCenter.Infrastructure/Persistence/Configurations/RoomConfiguration.cs` (defined near L1; showing L1–L29 of 29)._

```csharp
using CampCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampCenter.Infrastructure.Persistence.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Number).IsRequired().HasMaxLength(32);

        builder.HasIndex(x => x.Number).IsUnique();

        builder.Property(x => x.Description).HasMaxLength(512);

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
- [[RoomConfiguration]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Management