using CampCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampCenter.Infrastructure.Persistence.Configurations;

public class MealTimeDefaultConfiguration : IEntityTypeConfiguration<MealTimeDefault>
{
    public void Configure(EntityTypeBuilder<MealTimeDefault> builder)
    {
        builder.ToTable(
            "MealTimeDefaults",
            t =>
            {
                t.HasCheckConstraint(
                    "CK_MealTimeDefaults_TimeOrder",
                    "\"EndTime\" > \"StartTime\""
                );
                t.HasCheckConstraint(
                    "CK_MealTimeDefaults_Duration",
                    "\"DurationMinutes\" BETWEEN 5 AND 480"
                );
            }
        );

        builder.HasKey(x => x.Id);

        builder.Property(x => x.MealKind).HasConversion<string>().HasMaxLength(16);

        builder.Property(x => x.Label).IsRequired().HasMaxLength(128);

        builder.Property(x => x.DurationMinutes).HasDefaultValue(60);

        builder.HasIndex(x => new { x.SortOrder, x.StartTime });

        // Optimistic concurrency via the PostgreSQL "xmin" system column.
        builder
            .Property(x => x.RowVersion)
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .ValueGeneratedOnAddOrUpdate()
            .IsConcurrencyToken();
    }
}
