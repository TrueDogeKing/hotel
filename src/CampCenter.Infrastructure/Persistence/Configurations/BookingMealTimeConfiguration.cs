using CampCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampCenter.Infrastructure.Persistence.Configurations;

public class BookingMealTimeConfiguration : IEntityTypeConfiguration<BookingMealTime>
{
    public void Configure(EntityTypeBuilder<BookingMealTime> builder)
    {
        builder.ToTable(
            "BookingMealTimes",
            t => t.HasCheckConstraint("CK_BookingMealTimes_TimeOrder", "\"EndTime\" > \"StartTime\"")
        );

        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Booking)
            .WithMany()
            .HasForeignKey(x => x.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        // Deleting the center slot removes the override with it — unlike a generated
        // ScheduleEntry, an override has no meaning once its slot is gone.
        builder
            .HasOne(x => x.MealTimeDefault)
            .WithMany()
            .HasForeignKey(x => x.MealTimeDefaultId)
            .OnDelete(DeleteBehavior.Cascade);

        // At most one override per group per slot.
        builder.HasIndex(x => new { x.BookingId, x.MealTimeDefaultId }).IsUnique();

        // Optimistic concurrency via the PostgreSQL "xmin" system column.
        builder
            .Property(x => x.RowVersion)
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .ValueGeneratedOnAddOrUpdate()
            .IsConcurrencyToken();
    }
}
