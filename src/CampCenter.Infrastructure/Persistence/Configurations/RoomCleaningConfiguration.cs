using CampCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampCenter.Infrastructure.Persistence.Configurations;

public class RoomCleaningConfiguration : IEntityTypeConfiguration<RoomCleaning>
{
    public void Configure(EntityTypeBuilder<RoomCleaning> builder)
    {
        builder.ToTable("RoomCleanings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Kind).HasConversion<string>().HasMaxLength(16);

        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(16);

        builder.Property(x => x.Note).HasMaxLength(1000);

        // One room is cleaned once a day: this is what makes marking a room done an
        // upsert rather than a growing pile of rows for the same job.
        builder.HasIndex(x => new { x.RoomId, x.Date }).IsUnique();

        // The day's list reads every room's progress for one date.
        builder.HasIndex(x => x.Date);

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
