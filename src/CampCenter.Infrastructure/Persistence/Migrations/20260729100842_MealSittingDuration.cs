using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampCenter.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MealSittingDuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurationMinutes",
                table: "MealTimeDefaults",
                type: "integer",
                nullable: false,
                defaultValue: 60);

            // Existing slots served every group across the whole window, so seed the
            // duration from the window length. Anything outside the check constraint's
            // range is clamped, and the new 60-minute default only applies to slots
            // created from here on.
            migrationBuilder.Sql(
                """
                UPDATE "MealTimeDefaults"
                SET "DurationMinutes" = LEAST(
                    480,
                    GREATEST(5, EXTRACT(EPOCH FROM ("EndTime" - "StartTime")) / 60)::int
                );
                """
            );

            migrationBuilder.AddCheckConstraint(
                name: "CK_MealTimeDefaults_Duration",
                table: "MealTimeDefaults",
                sql: "\"DurationMinutes\" BETWEEN 5 AND 480");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_MealTimeDefaults_Duration",
                table: "MealTimeDefaults");

            migrationBuilder.DropColumn(
                name: "DurationMinutes",
                table: "MealTimeDefaults");
        }
    }
}
