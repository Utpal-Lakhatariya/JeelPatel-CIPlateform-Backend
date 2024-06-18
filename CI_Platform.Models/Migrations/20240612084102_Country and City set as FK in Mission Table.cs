using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CI_Platform.Models.Migrations
{
    /// <inheritdoc />
    public partial class CountryandCitysetasFKinMissionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Mission_CityId",
                table: "Mission",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Mission_CountryId",
                table: "Mission",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mission_City_CityId",
                table: "Mission",
                column: "CityId",
                principalTable: "City",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mission_Country_CountryId",
                table: "Mission",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mission_City_CityId",
                table: "Mission");

            migrationBuilder.DropForeignKey(
                name: "FK_Mission_Country_CountryId",
                table: "Mission");

            migrationBuilder.DropIndex(
                name: "IX_Mission_CityId",
                table: "Mission");

            migrationBuilder.DropIndex(
                name: "IX_Mission_CountryId",
                table: "Mission");
        }
    }
}
