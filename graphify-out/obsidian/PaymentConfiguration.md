---
source_file: "src/CampCenter.Infrastructure/Persistence/Configurations/PaymentConfiguration.cs"
type: "code"
community: "Booking Persistence & Entities"
location: "L7"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Booking_Persistence__Entities
---

# PaymentConfiguration

## Context

_Source: `src/CampCenter.Infrastructure/Persistence/Configurations/PaymentConfiguration.cs` (defined near L7; showing L5–L47 of 47)._

```csharp
namespace CampCenter.Infrastructure.Persistence.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Kind).HasConversion<string>().HasMaxLength(8);

        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(10);

        builder.Property(x => x.P24SessionId).IsRequired().HasMaxLength(100);

        // The P24 status webhook looks payments up by sessionId.
        builder.HasIndex(x => x.P24SessionId).IsUnique();

        builder.Property(x => x.P24Token).HasMaxLength(100);

        // A booking can have many failed/abandoned attempts but only ever one
        // COMPLETED payment per kind — the database guarantees no double-charge.
        builder
            .HasIndex(x => new { x.BookingId, x.Kind })
            .IsUnique()
            .HasFilter("\"Status\" = 'Completed'");

        builder
            .HasOne(x => x.Booking)
            .WithMany()
            .HasForeignKey(x => x.BookingId)
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
- [[.Configure()_4]] - `method` [EXTRACTED]
- [[IEntityTypeConfiguration]] - `implements` [EXTRACTED]
- [[Payment]] - `references` [EXTRACTED]
- [[PaymentConfiguration.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Booking_Persistence__Entities