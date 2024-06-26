using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CI_Platform.Models.Migrations
{
    /// <inheritdoc />
    public partial class MultipleTablerelationCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Publish",
                table: "Story",
                newName: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Mission_ThemeId",
                table: "Mission",
                column: "ThemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mission_Theme_ThemeId",
                table: "Mission",
                column: "ThemeId",
                principalTable: "Theme",
                principalColumn: "ThemeId");
                
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mission_Theme_ThemeId",
                table: "Mission");

            migrationBuilder.DropIndex(
                name: "IX_Mission_ThemeId",
                table: "Mission");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Story",
                newName: "Publish");
        }
    }
}
