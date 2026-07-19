using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampCenter.Infrastructure.Persistence.Migrations
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
                    OrganizationName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ContactName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Phone = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Headcount = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Status = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    CancelReason = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    ManageTokenHash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    HoldExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TotalGrosze = table.Column<long>(type: "bigint", nullable: false),
                    DepositGrosze = table.Column<long>(type: "bigint", nullable: false),
                    RequestedRoomCounts = table.Column<string>(type: "jsonb", nullable: false),
                    Language = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_CampSessions_CampSessionId",
                        column: x => x.CampSessionId,
                        principalTable: "CampSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingRoomAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookingId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    CampSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    PeopleCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingRoomAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingRoomAssignments_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingRoomAssignments_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookingId = table.Column<Guid>(type: "uuid", nullable: false),
                    Kind = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    AmountGrosze = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    P24SessionId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    P24Token = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    P24OrderId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    CampSessionId = table.Column<Guid>(type: "uuid", nullable: true),
                    BookingId = table.Column<Guid>(type: "uuid", nullable: true),
                    Text = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Status = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    CreatedByAdminUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DoneAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomTasks_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_RoomTasks_CampSessions_CampSessionId",
                        column: x => x.CampSessionId,
                        principalTable: "CampSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_RoomTasks_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingRoomAssignments_BookingId",
                table: "BookingRoomAssignments",
                column: "BookingId");

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
                name: "IX_Bookings_CampSessionId_Status",
                table: "Bookings",
                columns: new[] { "CampSessionId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ManageTokenHash",
                table: "Bookings",
                column: "ManageTokenHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CampSessions_Status_StartDate",
                table: "CampSessions",
                columns: new[] { "Status", "StartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId_Kind",
                table: "Payments",
                columns: new[] { "BookingId", "Kind" },
                unique: true,
                filter: "\"Status\" = 'Completed'");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_P24SessionId",
                table: "Payments",
                column: "P24SessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Number",
                table: "Rooms",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomTasks_BookingId",
                table: "RoomTasks",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTasks_CampSessionId",
                table: "RoomTasks",
                column: "CampSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTasks_RoomId",
                table: "RoomTasks",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTasks_Status",
                table: "RoomTasks",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingRoomAssignments");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "RoomTasks");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "CampSessions");
        }
    }
}
