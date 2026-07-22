---
source_file: "src/CampCenter.Infrastructure/Persistence/Configurations/RefreshTokenConfiguration.cs"
type: "code"
community: "Refresh Token EF Config"
location: "L8"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Refresh_Token_EF_Config
---

# RefreshTokenConfiguration

## Context

_Source: `src/CampCenter.Infrastructure/Persistence/Configurations/RefreshTokenConfiguration.cs` (defined near L8; showing L6–L39 of 39)._

```csharp

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
```

## Connections
- [[.Configure()_5]] - `method` [EXTRACTED]
- [[IEntityTypeConfiguration]] - `implements` [EXTRACTED]
- [[RefreshToken]] - `references` [EXTRACTED]
- [[RefreshTokenConfiguration.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Refresh_Token_EF_Config