using CampCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampCenter.Infrastructure.Persistence.Configurations;

/// EF Core configuration for <see cref="RefreshToken"/> entity.
public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.TokenHash).IsRequired().HasMaxLength(128);

        // Unique index for token hash lookup.
        builder.HasIndex(t => t.TokenHash).IsUnique();

        builder.Property(t => t.ExpiresAtUtc).IsRequired();

        builder.Property(t => t.CreatedAtUtc).IsRequired();

        builder.Property(t => t.ReplacedByTokenHash).HasMaxLength(128);

        // Index for revoking all active tokens by admin.
        builder.HasIndex(t => t.AdminUserId);

        builder
            .HasOne(t => t.AdminUser)
            .WithMany()
            .HasForeignKey(t => t.AdminUserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Computed property – not mapped to a column.
        builder.Ignore(t => t.IsActive);
    }
}
