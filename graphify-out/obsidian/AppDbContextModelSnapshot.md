---
source_file: "src/CampCenter.Infrastructure/Persistence/Migrations/AppDbContextModelSnapshot.cs"
type: "code"
community: "EF Core Migrations"
location: "L13"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/EF_Core_Migrations
---

# AppDbContextModelSnapshot

## Context

_Source: `src/CampCenter.Infrastructure/Persistence/Migrations/AppDbContextModelSnapshot.cs` (defined near L13; showing L11–L58 of 485)._

```csharp
namespace CampCenter.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "10.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CampCenter.Domain.Entities.AdminUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<uint>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("AdminUsers", (string)null);
                });

            modelBuilder.Entity("CampCenter.Domain.Entities.Booking", b =>
                {
```

## Connections
- [[.BuildModel()]] - `method` [EXTRACTED]
- [[AppDbContextModelSnapshot.cs]] - `contains` [EXTRACTED]
- [[ModelSnapshot]] - `inherits` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/EF_Core_Migrations