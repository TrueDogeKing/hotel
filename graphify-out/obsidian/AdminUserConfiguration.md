---
source_file: "src/CampCenter.Infrastructure/Persistence/Configurations/AdminUserConfiguration.cs"
type: "code"
community: "Admin User & Token Config"
location: "L8"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/Admin_User__Token_Config
---

# AdminUserConfiguration

## Context

_Source: `src/CampCenter.Infrastructure/Persistence/Configurations/AdminUserConfiguration.cs` (defined near L8; showing L6–L33 of 33)._

```csharp

/// <summary>Konfiguracja EF Core dla encji <see cref="AdminUser"/>.</summary>
public class AdminUserConfiguration : IEntityTypeConfiguration<AdminUser>
{
    public void Configure(EntityTypeBuilder<AdminUser> builder)
    {
        builder.ToTable("AdminUsers");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Login).IsRequired().HasMaxLength(32);

        // Unikalny indeks na login (przechowywany małymi literami).
        builder.HasIndex(u => u.Login).IsUnique();

        builder.Property(u => u.PasswordHash).IsRequired();

        builder.Property(u => u.CreatedAt).IsRequired();

        // Optimistic concurrency oparte o systemową kolumnę PostgreSQL "xmin".
        builder
            .Property(u => u.RowVersion)
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .ValueGeneratedOnAddOrUpdate()
            .IsConcurrencyToken();
    }
}
```

## Connections
- [[.Configure()]] - `method` [EXTRACTED]
- [[AdminUser]] - `references` [EXTRACTED]
- [[AdminUserConfiguration.cs]] - `contains` [EXTRACTED]
- [[IEntityTypeConfiguration]] - `implements` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/Admin_User__Token_Config