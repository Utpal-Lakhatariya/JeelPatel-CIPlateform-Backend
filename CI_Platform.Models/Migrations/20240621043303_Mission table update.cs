using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CI_Platform.Models.Migrations
{
    /// <inheritdoc />
    public partial class Missiontableupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MissionSkills",
                table: "Mission");

            migrationBuilder.DropColumn(
                name: "MissionTheme",
                table: "Mission");

            migrationBuilder.AddColumn<int>(
                name: "ThemeId",
                table: "Mission",
                type: "integer",
                maxLength: 20,
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "Mission");

            migrationBuilder.AddColumn<string>(
                name: "MissionSkills",
                table: "Mission",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MissionTheme",
                table: "Mission",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
