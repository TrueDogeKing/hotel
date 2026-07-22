---
source_file: "src/CampCenter.Infrastructure/Persistence/Migrations/20260719143540_CoreDomain.Designer.cs"
type: "code"
community: "EF Core Migrations"
location: "L14"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/EF_Core_Migrations
---

# CoreDomain

## Context

_Source: `src/CampCenter.Infrastructure/Persistence/Migrations/20260719143540_CoreDomain.Designer.cs` (defined near L14; showing L12–L59 of 488)._

```csharp
namespace CampCenter.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20260719143540_CoreDomain")]
    partial class CoreDomain
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

```

## Connections
- [[.BuildTargetModel()_1]] - `method` [EXTRACTED]
- [[20260719143540_CoreDomain.Designer.cs]] - `contains` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/EF_Core_Migrations