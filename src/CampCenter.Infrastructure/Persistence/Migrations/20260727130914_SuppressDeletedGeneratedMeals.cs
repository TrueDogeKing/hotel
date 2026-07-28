using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampCenter.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SuppressDeletedGeneratedMeals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSuppressed",
                table: "ScheduleEntries",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSuppressed",
                table: "ScheduleEntries");
        }
    }
}
