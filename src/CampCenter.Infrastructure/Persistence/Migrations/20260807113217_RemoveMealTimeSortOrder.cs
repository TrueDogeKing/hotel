using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampCenter.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMealTimeSortOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MealTimeDefaults_SortOrder_StartTime",
                table: "MealTimeDefaults");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "MealTimeDefaults");

            migrationBuilder.CreateIndex(
                name: "IX_MealTimeDefaults_StartTime",
                table: "MealTimeDefaults",
                column: "StartTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MealTimeDefaults_StartTime",
                table: "MealTimeDefaults");

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "MealTimeDefaults",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MealTimeDefaults_SortOrder_StartTime",
                table: "MealTimeDefaults",
                columns: new[] { "SortOrder", "StartTime" });
        }
    }
}
