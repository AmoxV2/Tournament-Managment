using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WWW_APP_PROJECT.Migrations
{
    public partial class SportDiscipline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SportDisciplineId",
                table: "Tournaments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SportsDisciplines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisciplineName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportsDisciplines", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_SportDisciplineId",
                table: "Tournaments",
                column: "SportDisciplineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_SportsDisciplines_SportDisciplineId",
                table: "Tournaments",
                column: "SportDisciplineId",
                principalTable: "SportsDisciplines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_SportsDisciplines_SportDisciplineId",
                table: "Tournaments");

            migrationBuilder.DropTable(
                name: "SportsDisciplines");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_SportDisciplineId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "SportDisciplineId",
                table: "Tournaments");
        }
    }
}
