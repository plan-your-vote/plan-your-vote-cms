using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class M1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Elections",
                columns: table => new
                {
                    ElectionId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ElectionName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elections", x => x.ElectionId);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ThemeName = table.Column<string>(nullable: false),
                    Placement = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: false),
                    Format = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OpenGraph",
                columns: table => new
                {
                    OpenGraphId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Determiner = table.Column<string>(nullable: true),
                    Locale = table.Column<string>(nullable: true),
                    SiteName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenGraph", x => x.OpenGraphId);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrganizationId);
                });

            migrationBuilder.CreateTable(
                name: "Steps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ElectionId = table.Column<int>(nullable: false),
                    StepNumber = table.Column<int>(nullable: false),
                    StepTitle = table.Column<string>(nullable: true),
                    StepDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    ThemeName = table.Column<string>(nullable: false),
                    Selected = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.ThemeName);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BallotIssues",
                columns: table => new
                {
                    BallotIssueId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ElectionId = table.Column<int>(nullable: false),
                    BallotIssueTitle = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BallotIssues", x => x.BallotIssueId);
                    table.ForeignKey(
                        name: "FK_BallotIssues_Elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "Elections",
                        principalColumn: "ElectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PollingPlaces",
                columns: table => new
                {
                    PollingPlaceId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ElectionId = table.Column<int>(nullable: false),
                    PollingPlaceName = table.Column<string>(nullable: true),
                    PollingStationName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    WheelchairInfo = table.Column<string>(nullable: true),
                    ParkingInfo = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    AdvanceOnly = table.Column<bool>(nullable: false),
                    LocalArea = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollingPlaces", x => x.PollingPlaceId);
                    table.ForeignKey(
                        name: "FK_PollingPlaces_Elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "Elections",
                        principalColumn: "ElectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    RaceId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ElectionId = table.Column<int>(nullable: false),
                    BallotOrder = table.Column<int>(nullable: false),
                    PositionName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    NumberNeeded = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.RaceId);
                    table.ForeignKey(
                        name: "FK_Races_Elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "Elections",
                        principalColumn: "ElectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SocialMedias",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ElectionId = table.Column<int>(nullable: false),
                    MediaName = table.Column<string>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedias", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SocialMedias_Elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "Elections",
                        principalColumn: "ElectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StateSingleton",
                columns: table => new
                {
                    StateId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RunningElectionID = table.Column<int>(nullable: false),
                    ManagedElectionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateSingleton", x => x.StateId);
                    table.ForeignKey(
                        name: "FK_StateSingleton_Elections_ManagedElectionID",
                        column: x => x.ManagedElectionID,
                        principalTable: "Elections",
                        principalColumn: "ElectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StateSingleton_Elections_RunningElectionID",
                        column: x => x.RunningElectionID,
                        principalTable: "Elections",
                        principalColumn: "ElectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OGAudio",
                columns: table => new
                {
                    OGAudioID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(nullable: true),
                    SecureURL = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OpenGraphId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OGAudio", x => x.OGAudioID);
                    table.ForeignKey(
                        name: "FK_OGAudio_OpenGraph_OpenGraphId",
                        column: x => x.OpenGraphId,
                        principalTable: "OpenGraph",
                        principalColumn: "OpenGraphId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OGImage",
                columns: table => new
                {
                    OGImageId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(nullable: true),
                    SecureURL = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Alt = table.Column<string>(nullable: true),
                    OpenGraphId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OGImage", x => x.OGImageId);
                    table.ForeignKey(
                        name: "FK_OGImage_OpenGraph_OpenGraphId",
                        column: x => x.OpenGraphId,
                        principalTable: "OpenGraph",
                        principalColumn: "OpenGraphId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OGVideo",
                columns: table => new
                {
                    OGVideoID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(nullable: true),
                    SecureURL = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    OpenGraphId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OGVideo", x => x.OGVideoID);
                    table.ForeignKey(
                        name: "FK_OGVideo_OpenGraph_OpenGraphId",
                        column: x => x.OpenGraphId,
                        principalTable: "OpenGraph",
                        principalColumn: "OpenGraphId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    CandidateId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ElectionId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Picture = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.CandidateId);
                    table.ForeignKey(
                        name: "FK_Candidates_Elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "Elections",
                        principalColumn: "ElectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidates_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueOptions",
                columns: table => new
                {
                    IssueOptionId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IssueOptionInfo = table.Column<string>(nullable: true),
                    BallotIssueId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueOptions", x => x.IssueOptionId);
                    table.ForeignKey(
                        name: "FK_IssueOptions_BallotIssues_BallotIssueId",
                        column: x => x.BallotIssueId,
                        principalTable: "BallotIssues",
                        principalColumn: "BallotIssueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PollingPlaceDates",
                columns: table => new
                {
                    PollingDateId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PollingPlaceId = table.Column<int>(nullable: false),
                    PollingDate = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollingPlaceDates", x => x.PollingDateId);
                    table.ForeignKey(
                        name: "FK_PollingPlaceDates_PollingPlaces_PollingPlaceId",
                        column: x => x.PollingPlaceId,
                        principalTable: "PollingPlaces",
                        principalColumn: "PollingPlaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateDetails",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CandidateId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Format = table.Column<int>(nullable: false),
                    Lang = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CandidateDetails_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "CandidateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateRaces",
                columns: table => new
                {
                    CandidateRaceId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CandidateId = table.Column<int>(nullable: false),
                    RaceId = table.Column<int>(nullable: false),
                    BallotOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateRaces", x => x.CandidateRaceId);
                    table.ForeignKey(
                        name: "FK_CandidateRaces_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "CandidateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateRaces_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContactMethod = table.Column<int>(nullable: false),
                    ContactValue = table.Column<string>(nullable: true),
                    CandidateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                    table.ForeignKey(
                        name: "FK_Contacts_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "CandidateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BallotIssues_ElectionId",
                table: "BallotIssues",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDetails_CandidateId",
                table: "CandidateDetails",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateRaces_CandidateId",
                table: "CandidateRaces",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateRaces_RaceId",
                table: "CandidateRaces",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_ElectionId",
                table: "Candidates",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_OrganizationId",
                table: "Candidates",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CandidateId",
                table: "Contacts",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueOptions_BallotIssueId",
                table: "IssueOptions",
                column: "BallotIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_OGAudio_OpenGraphId",
                table: "OGAudio",
                column: "OpenGraphId");

            migrationBuilder.CreateIndex(
                name: "IX_OGImage_OpenGraphId",
                table: "OGImage",
                column: "OpenGraphId");

            migrationBuilder.CreateIndex(
                name: "IX_OGVideo_OpenGraphId",
                table: "OGVideo",
                column: "OpenGraphId");

            migrationBuilder.CreateIndex(
                name: "IX_PollingPlaceDates_PollingPlaceId",
                table: "PollingPlaceDates",
                column: "PollingPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_PollingPlaces_ElectionId",
                table: "PollingPlaces",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_ElectionId",
                table: "Races",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedias_ElectionId",
                table: "SocialMedias",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_StateSingleton_ManagedElectionID",
                table: "StateSingleton",
                column: "ManagedElectionID");

            migrationBuilder.CreateIndex(
                name: "IX_StateSingleton_RunningElectionID",
                table: "StateSingleton",
                column: "RunningElectionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CandidateDetails");

            migrationBuilder.DropTable(
                name: "CandidateRaces");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "IssueOptions");

            migrationBuilder.DropTable(
                name: "OGAudio");

            migrationBuilder.DropTable(
                name: "OGImage");

            migrationBuilder.DropTable(
                name: "OGVideo");

            migrationBuilder.DropTable(
                name: "PollingPlaceDates");

            migrationBuilder.DropTable(
                name: "SocialMedias");

            migrationBuilder.DropTable(
                name: "StateSingleton");

            migrationBuilder.DropTable(
                name: "Steps");

            migrationBuilder.DropTable(
                name: "Themes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "BallotIssues");

            migrationBuilder.DropTable(
                name: "OpenGraph");

            migrationBuilder.DropTable(
                name: "PollingPlaces");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Elections");
        }
    }
}
