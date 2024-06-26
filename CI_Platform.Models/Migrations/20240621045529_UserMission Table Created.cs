using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CI_Platform.Models.Migrations
{
    /// <inheritdoc />
    public partial class UserMissionTableCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionSkill_Mission_MissionId",
                table: "MissionSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionSkill_Skill_SkillId",
                table: "MissionSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionType_Mission_MissionId",
                table: "MissionType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MissionType",
                table: "MissionType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MissionSkill",
                table: "MissionSkill");

            migrationBuilder.RenameTable(
                name: "MissionType",
                newName: "MissionTypes");

            migrationBuilder.RenameTable(
                name: "MissionSkill",
                newName: "MissionSkills");

            migrationBuilder.RenameIndex(
                name: "IX_MissionType_MissionId",
                table: "MissionTypes",
                newName: "IX_MissionTypes_MissionId");

            migrationBuilder.RenameIndex(
                name: "IX_MissionSkill_SkillId",
                table: "MissionSkills",
                newName: "IX_MissionSkills_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_MissionSkill_MissionId",
                table: "MissionSkills",
                newName: "IX_MissionSkills_MissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MissionTypes",
                table: "MissionTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MissionSkills",
                table: "MissionSkills",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserMissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", maxLength: 20, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    MissionId = table.Column<int>(type: "integer", nullable: false),
                    Ratings = table.Column<int>(type: "int", maxLength: 10, nullable: true),
                    UserStatus = table.Column<int>(type: "integer", nullable: false),
                    Favourite = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMissions_Mission_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Mission",
                        principalColumn: "MissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMissions_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMissions_MissionId",
                table: "UserMissions",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMissions_UserId",
                table: "UserMissions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionSkills_Mission_MissionId",
                table: "MissionSkills",
                column: "MissionId",
                principalTable: "Mission",
                principalColumn: "MissionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionSkills_Skill_SkillId",
                table: "MissionSkills",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "SkillId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionTypes_Mission_MissionId",
                table: "MissionTypes",
                column: "MissionId",
                principalTable: "Mission",
                principalColumn: "MissionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionSkills_Mission_MissionId",
                table: "MissionSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionSkills_Skill_SkillId",
                table: "MissionSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionTypes_Mission_MissionId",
                table: "MissionTypes");

            migrationBuilder.DropTable(
                name: "UserMissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MissionTypes",
                table: "MissionTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MissionSkills",
                table: "MissionSkills");

            migrationBuilder.RenameTable(
                name: "MissionTypes",
                newName: "MissionType");

            migrationBuilder.RenameTable(
                name: "MissionSkills",
                newName: "MissionSkill");

            migrationBuilder.RenameIndex(
                name: "IX_MissionTypes_MissionId",
                table: "MissionType",
                newName: "IX_MissionType_MissionId");

            migrationBuilder.RenameIndex(
                name: "IX_MissionSkills_SkillId",
                table: "MissionSkill",
                newName: "IX_MissionSkill_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_MissionSkills_MissionId",
                table: "MissionSkill",
                newName: "IX_MissionSkill_MissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MissionType",
                table: "MissionType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MissionSkill",
                table: "MissionSkill",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionSkill_Mission_MissionId",
                table: "MissionSkill",
                column: "MissionId",
                principalTable: "Mission",
                principalColumn: "MissionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionSkill_Skill_SkillId",
                table: "MissionSkill",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "SkillId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionType_Mission_MissionId",
                table: "MissionType",
                column: "MissionId",
                principalTable: "Mission",
                principalColumn: "MissionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
