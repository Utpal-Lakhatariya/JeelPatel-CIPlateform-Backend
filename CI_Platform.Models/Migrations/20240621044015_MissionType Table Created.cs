using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CI_Platform.Models.Migrations
{
    /// <inheritdoc />
    public partial class MissionTypeTableCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MissionType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", maxLength: 20, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MissionId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MissionType_Mission_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Mission",
                        principalColumn: "MissionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MissionType_MissionId",
                table: "MissionType",
                column: "MissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MissionType");
        }
    }
}
