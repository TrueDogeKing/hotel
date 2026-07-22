---
source_file: "src/CampCenter.Infrastructure/Persistence/Migrations/20260719143540_CoreDomain.cs"
type: "code"
community: "EF Core Migrations"
location: "L9"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/EF_Core_Migrations
---

# CoreDomain

## Context

_Source: `src/CampCenter.Infrastructure/Persistence/Migrations/20260719143540_CoreDomain.cs` (defined near L9; showing L7–L54 of 269)._

```csharp
{
    /// <inheritdoc />
    public partial class CoreDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CampSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PricePerPersonGrosze = table.Column<long>(type: "bigint", nullable: false),
                    DepositPerPersonGrosze = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CampSessionId = table.Column<Guid>(type: "uuid", nullable: false),
```

## Connections
- [[.Down()_1]] - `method` [EXTRACTED]
- [[.Up()_1]] - `method` [EXTRACTED]
- [[20260719143540_CoreDomain.cs]] - `contains` [EXTRACTED]
- [[Migration]] - `inherits` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/EF_Core_Migrations