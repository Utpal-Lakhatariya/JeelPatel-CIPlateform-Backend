using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CI_Platform.Models.Migrations
{
    /// <inheritdoc />
    public partial class Missionfieldschanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AchievedGoal",
                table: "Mission",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoalObject",
                table: "Mission",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OccupiedSeats",
                table: "Mission",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AchievedGoal",
                table: "Mission");

            migrationBuilder.DropColumn(
                name: "GoalObject",
                table: "Mission");

            migrationBuilder.DropColumn(
                name: "OccupiedSeats",
                table: "Mission");
        }
    }
}
