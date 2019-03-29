using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class addstate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StateSingleton",
                columns: table => new
                {
                    StateId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    currentElection = table.Column<int>(nullable: false),
                    ElectionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateSingleton", x => x.StateId);
                    table.ForeignKey(
                        name: "FK_StateSingleton_Elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "Elections",
                        principalColumn: "ElectionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StateSingleton_ElectionId",
                table: "StateSingleton",
                column: "ElectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StateSingleton");
        }
    }
}
