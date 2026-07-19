using CampCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampCenter.Infrastructure.Persistence.Configurations;

public class CampSessionConfiguration : IEntityTypeConfiguration<CampSession>
{
    public void Configure(EntityTypeBuilder<CampSession> builder)
    {
        builder.ToTable("CampSessions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(128);

        // Enum stored as text for readable rows and safe reordering of members.
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(16);

        builder.HasIndex(x => new { x.Status, x.StartDate });

        // Optimistic concurrency via the PostgreSQL "xmin" system column.
        builder
            .Property(x => x.RowVersion)
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .ValueGeneratedOnAddOrUpdate()
            .IsConcurrencyToken();
    }
}
