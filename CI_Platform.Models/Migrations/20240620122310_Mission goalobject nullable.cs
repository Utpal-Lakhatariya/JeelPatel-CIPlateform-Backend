using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CI_Platform.Models.Migrations
{
    /// <inheritdoc />
    public partial class Missiongoalobjectnullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GoalObject",
                table: "Mission",
                type: "character varying(2048)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GoalObject",
                table: "Mission",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
