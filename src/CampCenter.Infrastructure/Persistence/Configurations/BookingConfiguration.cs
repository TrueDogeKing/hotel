using CampCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampCenter.Infrastructure.Persistence.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.OrganizationName).IsRequired().HasMaxLength(256);

        builder.Property(x => x.ContactName).IsRequired().HasMaxLength(128);

        builder.Property(x => x.Email).IsRequired().HasMaxLength(256);

        builder.Property(x => x.Phone).IsRequired().HasMaxLength(32);

        builder.Property(x => x.Notes).HasMaxLength(2000);

        builder.Property(x => x.DietaryNotes).HasMaxLength(2000);

        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(16);

        builder.Property(x => x.CancelReason).HasConversion<string>().HasMaxLength(16);

        builder
            .Property(x => x.PaymentState)
            .HasConversion<string>()
            .HasMaxLength(16)
            .HasDefaultValue(BookingPaymentState.Unpaid);

        builder.Property(x => x.ManageTokenHash).IsRequired().HasMaxLength(128);

        // Lookup of a booking from its manage-link token.
        builder.HasIndex(x => x.ManageTokenHash).IsUnique();

        builder.Property(x => x.RequestedRoomCounts).IsRequired().HasColumnType("jsonb");

        builder.Property(x => x.Language).IsRequired().HasMaxLength(2);

        // Admin overview and availability filter/sort by stay dates and status.
        builder.HasIndex(x => new { x.StartDate, x.Status });

        // Ignore the computed nights count — derived from the date range.
        builder.Ignore(x => x.Nights);

        // Optimistic concurrency via the PostgreSQL "xmin" system column.
        builder
            .Property(x => x.RowVersion)
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .ValueGeneratedOnAddOrUpdate()
            .IsConcurrencyToken();
    }
}
