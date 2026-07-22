using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampCenter.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceSessionsWithClosures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Needed for a GiST exclusion constraint that mixes uuid equality with
            // a range overlap operator (the double-booking guard, added below).
            migrationBuilder.Sql("CREATE EXTENSION IF NOT EXISTS btree_gist;");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_CampSessions_CampSessionId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomTasks_CampSessions_CampSessionId",
                table: "RoomTasks");

            migrationBuilder.DropTable(
                name: "CampSessions");

            migrationBuilder.DropIndex(
                name: "IX_RoomTasks_CampSessionId",
                table: "RoomTasks");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_CampSessionId_Status",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_BookingRoomAssignments_CampSessionId_RoomId",
                table: "BookingRoomAssignments");

            migrationBuilder.DropIndex(
                name: "IX_BookingRoomAssignments_RoomId",
                table: "BookingRoomAssignments");

            migrationBuilder.DropColumn(
                name: "CampSessionId",
                table: "RoomTasks");

            migrationBuilder.DropColumn(
                name: "CampSessionId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CampSessionId",
                table: "BookingRoomAssignments");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "Bookings",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "Bookings",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "BookingRoomAssignments",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "BookingRoomAssignments",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateTable(
                name: "Closures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Reason = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Closures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Closures_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_StartDate_Status",
                table: "Bookings",
                columns: new[] { "StartDate", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_BookingRoomAssignments_RoomId_StartDate_EndDate",
                table: "BookingRoomAssignments",
                columns: new[] { "RoomId", "StartDate", "EndDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Closures_RoomId",
                table: "Closures",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Closures_StartDate_EndDate",
                table: "Closures",
                columns: new[] { "StartDate", "EndDate" });

            // THE double-booking guard: the same room cannot be assigned to two
            // bookings whose half-open [StartDate, EndDate) stays overlap. Cancelled
            // bookings have their assignment rows deleted, so no status predicate is
            // needed. Half-open ranges let back-to-back stays (checkout day ==
            // next check-in day) coexist.
            migrationBuilder.Sql(
                """
                ALTER TABLE "BookingRoomAssignments"
                ADD CONSTRAINT "EX_BookingRoomAssignments_NoOverlap"
                EXCLUDE USING gist (
                    "RoomId" WITH =,
                    daterange("StartDate", "EndDate", '[)') WITH &&
                );
                """
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE \"BookingRoomAssignments\" DROP CONSTRAINT \"EX_BookingRoomAssignments_NoOverlap\";"
            );

            migrationBuilder.DropTable(
                name: "Closures");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_StartDate_Status",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_BookingRoomAssignments_RoomId_StartDate_EndDate",
                table: "BookingRoomAssignments");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "BookingRoomAssignments");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "BookingRoomAssignments");

            migrationBuilder.AddColumn<Guid>(
                name: "CampSessionId",
                table: "RoomTasks",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CampSessionId",
                table: "Bookings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CampSessionId",
                table: "BookingRoomAssignments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CampSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DepositPerPersonGrosze = table.Column<long>(type: "bigint", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    PricePerPersonGrosze = table.Column<long>(type: "bigint", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampSessions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomTasks_CampSessionId",
                table: "RoomTasks",
                column: "CampSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CampSessionId_Status",
                table: "Bookings",
                columns: new[] { "CampSessionId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_BookingRoomAssignments_CampSessionId_RoomId",
                table: "BookingRoomAssignments",
                columns: new[] { "CampSessionId", "RoomId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingRoomAssignments_RoomId",
                table: "BookingRoomAssignments",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_CampSessions_Status_StartDate",
                table: "CampSessions",
                columns: new[] { "Status", "StartDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_CampSessions_CampSessionId",
                table: "Bookings",
                column: "CampSessionId",
                principalTable: "CampSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTasks_CampSessions_CampSessionId",
                table: "RoomTasks",
                column: "CampSessionId",
                principalTable: "CampSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
