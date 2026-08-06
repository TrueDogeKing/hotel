using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampCenter.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ManualPricingAndPaymentState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentState",
                table: "Bookings",
                type: "character varying(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "Unpaid");

            migrationBuilder.AddColumn<long>(
                name: "PricePerPersonPerNightGrosze",
                table: "Bookings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "PricingDefaults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PricePerPersonPerNightGrosze = table.Column<long>(type: "bigint", nullable: false),
                    DepositPerPersonPerNightGrosze = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingDefaults", x => x.Id);
                    table.CheckConstraint("CK_PricingDefaults_Amounts", "\"PricePerPersonPerNightGrosze\" >= 0 AND \"DepositPerPersonPerNightGrosze\" BETWEEN 0 AND \"PricePerPersonPerNightGrosze\"");
                });

            // Existing bookings kept only their totals, so recover the rate they
            // were priced at rather than showing every historical group as 0 zł
            // per person. Zero-night or empty rows keep the 0 default.
            migrationBuilder.Sql(
                """
                UPDATE "Bookings"
                SET "PricePerPersonPerNightGrosze" =
                    "TotalGrosze" / ("Headcount" * ("EndDate" - "StartDate"))
                WHERE "Headcount" > 0 AND "EndDate" > "StartDate";
                """
            );

            // Deposits and final payments taken through Przelewy24 become the
            // owner-recorded state that replaces them.
            migrationBuilder.Sql(
                """
                UPDATE "Bookings" b
                SET "PaymentState" = CASE
                    WHEN EXISTS (
                        SELECT 1 FROM "Payments" p
                        WHERE p."BookingId" = b."Id"
                          AND p."Kind" = 'Final' AND p."Status" = 'Completed'
                    ) THEN 'Paid'
                    ELSE 'DepositPaid'
                END
                WHERE EXISTS (
                    SELECT 1 FROM "Payments" p
                    WHERE p."BookingId" = b."Id" AND p."Status" = 'Completed'
                );
                """
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PricingDefaults");

            migrationBuilder.DropColumn(
                name: "PaymentState",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PricePerPersonPerNightGrosze",
                table: "Bookings");
        }
    }
}
