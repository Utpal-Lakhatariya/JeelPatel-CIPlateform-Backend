using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CI_Platform.Models.Migrations
{
    /// <inheritdoc />
    public partial class MissionSkillTableCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MissionSkill",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", maxLength: 20, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SkillId = table.Column<int>(type: "integer", maxLength: 10, nullable: false),
                    MissionId = table.Column<int>(type: "integer", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MissionSkill_Mission_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Mission",
                        principalColumn: "MissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MissionSkill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MissionSkill_MissionId",
                table: "MissionSkill",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionSkill_SkillId",
                table: "MissionSkill",
                column: "SkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MissionSkill");
        }
    }
}
