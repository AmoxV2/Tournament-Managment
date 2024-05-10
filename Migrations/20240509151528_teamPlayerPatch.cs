using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WWW_APP_PROJECT.Migrations
{
    public partial class teamPlayerPatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamPlayers_AspNetUsers_AppUserId",
                table: "TeamPlayers");

            migrationBuilder.DropIndex(
                name: "IX_TeamPlayers_AppUserId",
                table: "TeamPlayers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "TeamPlayers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "TeamPlayers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayers_AppUserId",
                table: "TeamPlayers",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamPlayers_AspNetUsers_AppUserId",
                table: "TeamPlayers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
