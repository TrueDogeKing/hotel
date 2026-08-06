using CampCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampCenter.Infrastructure.Persistence.Configurations;

public class PricingDefaultsConfiguration : IEntityTypeConfiguration<PricingDefaults>
{
    public void Configure(EntityTypeBuilder<PricingDefaults> builder)
    {
        builder.ToTable(
            "PricingDefaults",
            t =>
            {
                t.HasCheckConstraint(
                    "CK_PricingDefaults_Amounts",
                    "\"PricePerPersonPerNightGrosze\" >= 0 "
                        + "AND \"DepositPerPersonPerNightGrosze\" BETWEEN 0 AND \"PricePerPersonPerNightGrosze\""
                );
            }
        );

        builder.HasKey(x => x.Id);

        // Optimistic concurrency via the PostgreSQL "xmin" system column.
        builder
            .Property(x => x.RowVersion)
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .ValueGeneratedOnAddOrUpdate()
            .IsConcurrencyToken();
    }
}
