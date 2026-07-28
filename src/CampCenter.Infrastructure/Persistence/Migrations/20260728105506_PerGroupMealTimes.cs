using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampCenter.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PerGroupMealTimes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TimesCustomized",
                table: "ScheduleEntries",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "BookingMealTimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookingId = table.Column<Guid>(type: "uuid", nullable: false),
                    MealTimeDefaultId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingMealTimes", x => x.Id);
                    table.CheckConstraint("CK_BookingMealTimes_TimeOrder", "\"EndTime\" > \"StartTime\"");
                    table.ForeignKey(
                        name: "FK_BookingMealTimes_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingMealTimes_MealTimeDefaults_MealTimeDefaultId",
                        column: x => x.MealTimeDefaultId,
                        principalTable: "MealTimeDefaults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingMealTimes_BookingId_MealTimeDefaultId",
                table: "BookingMealTimes",
                columns: new[] { "BookingId", "MealTimeDefaultId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingMealTimes_MealTimeDefaultId",
                table: "BookingMealTimes",
                column: "MealTimeDefaultId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingMealTimes");

            migrationBuilder.DropColumn(
                name: "TimesCustomized",
                table: "ScheduleEntries");
        }
    }
}
