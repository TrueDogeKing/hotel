using CampCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampCenter.Infrastructure.Persistence.Configurations;

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
