﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CI_Platform.Models.Migrations
{
    /// <inheritdoc />
    public partial class DatabasePostgre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    AdminId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    LastName = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    Email = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    AdminAvatar = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Country = table.Column<string>(type: "character varying (16)", nullable: false),
                    City = table.Column<string>(type: "character varying (16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "CMSPrivacyPolicy",
                columns: table => new
                {
                    CMSId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PageTitle = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PageDescription = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    Slug = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CMSPrivacyPolicy", x => x.CMSId);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Country = table.Column<string>(type: "character varying (16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "LoginCarousel",
                columns: table => new
                {
                    CarouselId = table.Column<int>(type: "integer", maxLength: 10, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CarouselImage = table.Column<byte[]>(type: "bytea", nullable: false),
                    CarouselHead = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CarouselText = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginCarousel", x => x.CarouselId);
                });

            migrationBuilder.CreateTable(
                name: "Mission",
                columns: table => new
                {
                    MissionId = table.Column<int>(type: "integer", maxLength: 20, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MissionTitle = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    MissionShortDescription = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    MissionDescription = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    CountryId = table.Column<long>(type: "bigint", maxLength: 50, nullable: false),
                    CityId = table.Column<long>(type: "bigint", maxLength: 50, nullable: false),
                    MissionOrganisationName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MissionOrganisationDetail = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    MissionStartDate = table.Column<DateTime>(type: "date", nullable: false),
                    MissionEndDate = table.Column<DateTime>(type: "date", nullable: false),
                    MissionType = table.Column<int>(type: "integer", maxLength: 50, nullable: false),
                    TotalSeats = table.Column<int>(type: "integer", maxLength: 50, nullable: true),
                    MissionRating = table.Column<int>(type: "integer", maxLength: 20, nullable: true),
                    MissionRatingCount = table.Column<int>(type: "integer", maxLength: 50, nullable: true),
                    MissionRegistrationDeadline = table.Column<DateTime>(type: "date", nullable: false),
                    MissionTheme = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    MissionSkills = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    MissionAvailability = table.Column<int>(type: "integer", nullable: false),
                    MissionVideo = table.Column<byte[]>(type: "bytea", maxLength: 2048, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mission", x => x.MissionId);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "integer", maxLength: 10, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Skills = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.SkillId);
                });

            migrationBuilder.CreateTable(
                name: "Theme",
                columns: table => new
                {
                    ThemeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    theme = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theme", x => x.ThemeId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    LastName = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    Email = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    Avatar = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    Skills = table.Column<string>(type: "text", nullable: true),
                    WhyIVolunteer = table.Column<string>(type: "text", nullable: true),
                    EmployeeId = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    Department = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    CityId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    CountryId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    ProfileText = table.Column<string>(type: "text", nullable: true),
                    LinkedInUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_DATE"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MissionMedia",
                columns: table => new
                {
                    MediaId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Image = table.Column<byte[]>(type: "bytea", nullable: true),
                    Document = table.Column<byte[]>(type: "bytea", nullable: true),
                    MissionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionMedia", x => x.MediaId);
                    table.ForeignKey(
                        name: "FK_MissionMedia_Mission_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Mission",
                        principalColumn: "MissionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoryMedia",
                columns: table => new
                {
                    MediaId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Image = table.Column<byte[]>(type: "bytea", nullable: false),
                    Document = table.Column<byte[]>(type: "bytea", nullable: false),
                    MissionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryMedia", x => x.MediaId);
                    table.ForeignKey(
                        name: "FK_StoryMedia_Mission_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Mission",
                        principalColumn: "MissionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VolunteeringTimesheet",
                columns: table => new
                {
                    VolunteeringId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MissionId = table.Column<int>(type: "integer", nullable: false),
                    MissionTitle = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Hours = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Minutes = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Action = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteeringTimesheet", x => x.VolunteeringId);
                    table.ForeignKey(
                        name: "FK_VolunteeringTimesheet_Mission_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Mission",
                        principalColumn: "MissionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "integer", maxLength: 50, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MissionTitle = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false),
                    UserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Comments = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MissionApplication",
                columns: table => new
                {
                    ApplicationId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    MissionId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionApplication", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_MissionApplication_Mission_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Mission",
                        principalColumn: "MissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MissionApplication_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecentVolunteer",
                columns: table => new
                {
                    VolunteerId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    MissionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecentVolunteer", x => x.VolunteerId);
                    table.ForeignKey(
                        name: "FK_RecentVolunteer_Mission_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Mission",
                        principalColumn: "MissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecentVolunteer_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Story",
                columns: table => new
                {
                    StoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StoryTitle = table.Column<string>(type: "text", nullable: false),
                    MissionTitle = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    StoryDescription = table.Column<string>(type: "text", nullable: false),
                    Publish = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Story", x => x.StoryId);
                    table.ForeignKey(
                        name: "FK_Story_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionApplication_MissionId",
                table: "MissionApplication",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionApplication_UserId",
                table: "MissionApplication",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionMedia_MissionId",
                table: "MissionMedia",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecentVolunteer_MissionId",
                table: "RecentVolunteer",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecentVolunteer_UserId",
                table: "RecentVolunteer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Story_UserId",
                table: "Story",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryMedia_MissionId",
                table: "StoryMedia",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CityId",
                table: "User",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CountryId",
                table: "User",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteeringTimesheet_MissionId",
                table: "VolunteeringTimesheet",
                column: "MissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "CMSPrivacyPolicy");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "LoginCarousel");

            migrationBuilder.DropTable(
                name: "MissionApplication");

            migrationBuilder.DropTable(
                name: "MissionMedia");

            migrationBuilder.DropTable(
                name: "RecentVolunteer");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "Story");

            migrationBuilder.DropTable(
                name: "StoryMedia");

            migrationBuilder.DropTable(
                name: "Theme");

            migrationBuilder.DropTable(
                name: "VolunteeringTimesheet");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Mission");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
