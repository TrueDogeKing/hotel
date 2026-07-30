using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampCenter.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ParticipantCountOnScheduleEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParticipantCount",
                table: "ScheduleEntries",
                type: "integer",
                nullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_ScheduleEntries_ParticipantCount",
                table: "ScheduleEntries",
                sql: "\"ParticipantCount\" IS NULL OR \"ParticipantCount\" > 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_ScheduleEntries_ParticipantCount",
                table: "ScheduleEntries");

            migrationBuilder.DropColumn(
                name: "ParticipantCount",
                table: "ScheduleEntries");
        }
    }
}
