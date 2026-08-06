using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampCenter.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SupervisorCountsAndRates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_PricingDefaults_Amounts",
                table: "PricingDefaults");

            migrationBuilder.AddColumn<long>(
                name: "SupervisorPricePerPersonPerNightGrosze",
                table: "PricingDefaults",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "SupervisorCount",
                table: "Bookings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "SupervisorPricePerPersonPerNightGrosze",
                table: "Bookings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsSupervisorRoom",
                table: "BookingRoomAssignments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            // Everyone was charged one rate until now, so the supervisors inherit
            // it: no existing group's total changes, and no centre suddenly starts
            // housing its kadra for nothing. This has to run before the check
            // constraint below, which would otherwise reject the zero default
            // against a non-zero deposit.
            migrationBuilder.Sql(
                """
                UPDATE "PricingDefaults"
                SET "SupervisorPricePerPersonPerNightGrosze" = "PricePerPersonPerNightGrosze";
                """
            );

            migrationBuilder.Sql(
                """
                UPDATE "Bookings"
                SET "SupervisorPricePerPersonPerNightGrosze" = "PricePerPersonPerNightGrosze";
                """
            );

            migrationBuilder.AddCheckConstraint(
                name: "CK_PricingDefaults_Amounts",
                table: "PricingDefaults",
                sql: "\"PricePerPersonPerNightGrosze\" >= 0 AND \"SupervisorPricePerPersonPerNightGrosze\" >= 0 AND \"DepositPerPersonPerNightGrosze\" BETWEEN 0 AND LEAST(\"PricePerPersonPerNightGrosze\", \"SupervisorPricePerPersonPerNightGrosze\")");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Bookings_SupervisorCount",
                table: "Bookings",
                sql: "\"SupervisorCount\" >= 0 AND \"SupervisorCount\" <= \"Headcount\"");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_PricingDefaults_Amounts",
                table: "PricingDefaults");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Bookings_SupervisorCount",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "SupervisorPricePerPersonPerNightGrosze",
                table: "PricingDefaults");

            migrationBuilder.DropColumn(
                name: "SupervisorCount",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "SupervisorPricePerPersonPerNightGrosze",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "IsSupervisorRoom",
                table: "BookingRoomAssignments");

            migrationBuilder.AddCheckConstraint(
                name: "CK_PricingDefaults_Amounts",
                table: "PricingDefaults",
                sql: "\"PricePerPersonPerNightGrosze\" >= 0 AND \"DepositPerPersonPerNightGrosze\" BETWEEN 0 AND \"PricePerPersonPerNightGrosze\"");
        }
    }
}
