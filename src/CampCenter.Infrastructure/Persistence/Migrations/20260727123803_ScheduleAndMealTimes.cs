using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampCenter.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ScheduleAndMealTimes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DietaryNotes",
                table: "Bookings",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MealTimeDefaults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MealKind = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Label = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTimeDefaults", x => x.Id);
                    table.CheckConstraint("CK_MealTimeDefaults_TimeOrder", "\"EndTime\" > \"StartTime\"");
                });

            migrationBuilder.CreateTable(
                name: "ScheduleEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookingId = table.Column<Guid>(type: "uuid", nullable: false),
                    Kind = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    MealKind = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    MealTimeDefaultId = table.Column<Guid>(type: "uuid", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Menu = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    PrepNotes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Location = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleEntries", x => x.Id);
                    table.CheckConstraint("CK_ScheduleEntries_TimeOrder", "\"EndTime\" > \"StartTime\"");
                    table.ForeignKey(
                        name: "FK_ScheduleEntries_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleEntries_MealTimeDefaults_MealTimeDefaultId",
                        column: x => x.MealTimeDefaultId,
                        principalTable: "MealTimeDefaults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealTimeDefaults_SortOrder_StartTime",
                table: "MealTimeDefaults",
                columns: new[] { "SortOrder", "StartTime" });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEntries_BookingId_Date_MealTimeDefaultId",
                table: "ScheduleEntries",
                columns: new[] { "BookingId", "Date", "MealTimeDefaultId" },
                unique: true,
                filter: "\"MealTimeDefaultId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEntries_BookingId_Date_StartTime",
                table: "ScheduleEntries",
                columns: new[] { "BookingId", "Date", "StartTime" });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEntries_Date_StartTime",
                table: "ScheduleEntries",
                columns: new[] { "Date", "StartTime" });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEntries_MealTimeDefaultId",
                table: "ScheduleEntries",
                column: "MealTimeDefaultId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleEntries");

            migrationBuilder.DropTable(
                name: "MealTimeDefaults");

            migrationBuilder.DropColumn(
                name: "DietaryNotes",
                table: "Bookings");
        }
    }
}
