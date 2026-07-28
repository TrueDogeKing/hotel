using CampCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampCenter.Infrastructure.Persistence.Configurations;

public class ScheduleEntryConfiguration : IEntityTypeConfiguration<ScheduleEntry>
{
    public void Configure(EntityTypeBuilder<ScheduleEntry> builder)
    {
        // Expressed through ToTable (not raw SQL in the migration) so the model
        // snapshot stays in sync with the database.
        builder.ToTable(
            "ScheduleEntries",
            t => t.HasCheckConstraint("CK_ScheduleEntries_TimeOrder", "\"EndTime\" > \"StartTime\"")
        );

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Kind).HasConversion<string>().HasMaxLength(16);

        builder.Property(x => x.MealKind).HasConversion<string>().HasMaxLength(16);

        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);

        builder.Property(x => x.Menu).HasMaxLength(2000);

        builder.Property(x => x.PrepNotes).HasMaxLength(2000);

        builder.Property(x => x.Location).HasMaxLength(200);

        builder
            .HasOne(x => x.Booking)
            .WithMany()
            .HasForeignKey(x => x.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        // Deleting a default must not delete the meals already generated from it.
        builder
            .HasOne(x => x.MealTimeDefault)
            .WithMany()
            .HasForeignKey(x => x.MealTimeDefaultId)
            .OnDelete(DeleteBehavior.SetNull);

        // The day timetable: every entry on one date, in time order. Suppressed
        // rows are rare, so they are filtered in the query rather than indexed out.
        builder.HasIndex(x => new { x.Date, x.StartTime });

        // A single group's programme.
        builder.HasIndex(x => new { x.BookingId, x.Date, x.StartTime });

        // Makes meal generation idempotent at the database level, not just via a
        // read-then-write check — P24 retries notifications, sometimes concurrently.
        builder
            .HasIndex(x => new
            {
                x.BookingId,
                x.Date,
                x.MealTimeDefaultId,
            })
            .IsUnique()
            .HasFilter("\"MealTimeDefaultId\" IS NOT NULL");

        // Optimistic concurrency via the PostgreSQL "xmin" system column.
        builder
            .Property(x => x.RowVersion)
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .ValueGeneratedOnAddOrUpdate()
            .IsConcurrencyToken();
    }
}
