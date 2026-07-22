---
source_file: "src/CampCenter.Infrastructure/Persistence/Configurations/ClosureConfiguration.cs"
type: "code"
community: "Room Closure Management"
location: "L7"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Room_Closure_Management
---

# ClosureConfiguration

## Context

_Source: `src/CampCenter.Infrastructure/Persistence/Configurations/ClosureConfiguration.cs` (defined near L7; showing L5–L35 of 35)._

```csharp
namespace CampCenter.Infrastructure.Persistence.Configurations;

public class ClosureConfiguration : IEntityTypeConfiguration<Closure>
{
    public void Configure(EntityTypeBuilder<Closure> builder)
    {
        builder.ToTable("Closures");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Reason).IsRequired().HasMaxLength(256);

        // Availability lookups filter by span; per-room closures also filter by room.
        builder.HasIndex(x => new { x.StartDate, x.EndDate });

        // A null RoomId means the whole center is closed.
        builder
            .HasOne(x => x.Room)
            .WithMany()
            .HasForeignKey(x => x.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

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
- [[.Configure()_3]] - `method` [EXTRACTED]
- [[Closure]] - `references` [EXTRACTED]
- [[ClosureConfiguration.cs]] - `contains` [EXTRACTED]
- [[IEntityTypeConfiguration]] - `implements` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Room_Closure_Management