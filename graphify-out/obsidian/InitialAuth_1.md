---
source_file: "src/CampCenter.Infrastructure/Persistence/Migrations/20260719142059_InitialAuth.cs"
type: "code"
community: "EF Core Migrations"
location: "L9"
tags:
  - graphify/code
  - graphify/EXTRACTED
  - community/EF_Core_Migrations
---

# InitialAuth

## Context

_Source: `src/CampCenter.Infrastructure/Persistence/Migrations/20260719142059_InitialAuth.cs` (defined near L9; showing L7–L54 of 80)._

```csharp
{
    /// <inheritdoc />
    public partial class InitialAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AdminUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TokenHash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ExpiresAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevokedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReplacedByTokenHash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AdminUsers_AdminUserId",
                        column: x => x.AdminUserId,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_Login",
                table: "AdminUsers",
```

## Connections
- [[.Down()]] - `method` [EXTRACTED]
- [[.Up()]] - `method` [EXTRACTED]
- [[20260719142059_InitialAuth.cs]] - `contains` [EXTRACTED]
- [[Migration]] - `inherits` [EXTRACTED]

#graphify/code #graphify/EXTRACTED #community/EF_Core_Migrations