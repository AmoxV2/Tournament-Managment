using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WWW_APP_PROJECT.Migrations
{
    public partial class SoloTournament : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedTournament_AspNetUsers_AppUserId",
                table: "SharedTournament");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedTournament_TeamTournaments_TeamTournamentId",
                table: "SharedTournament");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMatch_Teams_GuestTeamId",
                table: "TeamMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMatch_Teams_HostTeamId",
                table: "TeamMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMatch_TeamTournaments_TeamTournamentId",
                table: "TeamMatch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamMatch",
                table: "TeamMatch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedTournament",
                table: "SharedTournament");

            migrationBuilder.RenameTable(
                name: "TeamMatch",
                newName: "TeamMatches");

            migrationBuilder.RenameTable(
                name: "SharedTournament",
                newName: "SharedTournaments");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMatch_TeamTournamentId",
                table: "TeamMatches",
                newName: "IX_TeamMatches_TeamTournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMatch_HostTeamId",
                table: "TeamMatches",
                newName: "IX_TeamMatches_HostTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMatch_GuestTeamId",
                table: "TeamMatches",
                newName: "IX_TeamMatches_GuestTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_SharedTournament_TeamTournamentId",
                table: "SharedTournaments",
                newName: "IX_SharedTournaments_TeamTournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_SharedTournament_AppUserId",
                table: "SharedTournaments",
                newName: "IX_SharedTournaments_AppUserId");

            migrationBuilder.AddColumn<int>(
                name: "SoloTournamentId",
                table: "SharedTournaments",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamMatches",
                table: "TeamMatches",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedTournaments",
                table: "SharedTournaments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SoloPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SoloSportDiscipline = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoloPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoloPlayers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SoloTournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TournamentType = table.Column<int>(type: "int", nullable: false),
                    NumberOfPlayers = table.Column<int>(type: "int", nullable: false),
                    TeamSportDiscipline = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    WinnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoloTournaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoloTournaments_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoloTournaments_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SoloTournaments_SoloPlayers_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "SoloPlayers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlayerToTournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoloPlayerId = table.Column<int>(type: "int", nullable: false),
                    SoloTournamentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerToTournaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerToTournaments_SoloPlayers_SoloPlayerId",
                        column: x => x.SoloPlayerId,
                        principalTable: "SoloPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerToTournaments_SoloTournaments_SoloTournamentId",
                        column: x => x.SoloTournamentId,
                        principalTable: "SoloTournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoloMatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HostPlayerId = table.Column<int>(type: "int", nullable: false),
                    GuestPlayerId = table.Column<int>(type: "int", nullable: false),
                    HostScore = table.Column<int>(type: "int", nullable: true),
                    GuestScore = table.Column<int>(type: "int", nullable: true),
                    MatchResult = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Stage = table.Column<int>(type: "int", nullable: false),
                    SoloTournamentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoloMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoloMatches_SoloPlayers_GuestPlayerId",
                        column: x => x.GuestPlayerId,
                        principalTable: "SoloPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SoloMatches_SoloPlayers_HostPlayerId",
                        column: x => x.HostPlayerId,
                        principalTable: "SoloPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SoloMatches_SoloTournaments_SoloTournamentId",
                        column: x => x.SoloTournamentId,
                        principalTable: "SoloTournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SharedTournaments_SoloTournamentId",
                table: "SharedTournaments",
                column: "SoloTournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerToTournaments_SoloPlayerId",
                table: "PlayerToTournaments",
                column: "SoloPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerToTournaments_SoloTournamentId",
                table: "PlayerToTournaments",
                column: "SoloTournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_SoloMatches_GuestPlayerId",
                table: "SoloMatches",
                column: "GuestPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_SoloMatches_HostPlayerId",
                table: "SoloMatches",
                column: "HostPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_SoloMatches_SoloTournamentId",
                table: "SoloMatches",
                column: "SoloTournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_SoloPlayers_AppUserId",
                table: "SoloPlayers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SoloTournaments_AddressId",
                table: "SoloTournaments",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_SoloTournaments_AppUserId",
                table: "SoloTournaments",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SoloTournaments_WinnerId",
                table: "SoloTournaments",
                column: "WinnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedTournaments_AspNetUsers_AppUserId",
                table: "SharedTournaments",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedTournaments_SoloTournaments_SoloTournamentId",
                table: "SharedTournaments",
                column: "SoloTournamentId",
                principalTable: "SoloTournaments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedTournaments_TeamTournaments_TeamTournamentId",
                table: "SharedTournaments",
                column: "TeamTournamentId",
                principalTable: "TeamTournaments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMatches_Teams_GuestTeamId",
                table: "TeamMatches",
                column: "GuestTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMatches_Teams_HostTeamId",
                table: "TeamMatches",
                column: "HostTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMatches_TeamTournaments_TeamTournamentId",
                table: "TeamMatches",
                column: "TeamTournamentId",
                principalTable: "TeamTournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedTournaments_AspNetUsers_AppUserId",
                table: "SharedTournaments");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedTournaments_SoloTournaments_SoloTournamentId",
                table: "SharedTournaments");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedTournaments_TeamTournaments_TeamTournamentId",
                table: "SharedTournaments");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMatches_Teams_GuestTeamId",
                table: "TeamMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMatches_Teams_HostTeamId",
                table: "TeamMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMatches_TeamTournaments_TeamTournamentId",
                table: "TeamMatches");

            migrationBuilder.DropTable(
                name: "PlayerToTournaments");

            migrationBuilder.DropTable(
                name: "SoloMatches");

            migrationBuilder.DropTable(
                name: "SoloTournaments");

            migrationBuilder.DropTable(
                name: "SoloPlayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamMatches",
                table: "TeamMatches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedTournaments",
                table: "SharedTournaments");

            migrationBuilder.DropIndex(
                name: "IX_SharedTournaments_SoloTournamentId",
                table: "SharedTournaments");

            migrationBuilder.DropColumn(
                name: "SoloTournamentId",
                table: "SharedTournaments");

            migrationBuilder.RenameTable(
                name: "TeamMatches",
                newName: "TeamMatch");

            migrationBuilder.RenameTable(
                name: "SharedTournaments",
                newName: "SharedTournament");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMatches_TeamTournamentId",
                table: "TeamMatch",
                newName: "IX_TeamMatch_TeamTournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMatches_HostTeamId",
                table: "TeamMatch",
                newName: "IX_TeamMatch_HostTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMatches_GuestTeamId",
                table: "TeamMatch",
                newName: "IX_TeamMatch_GuestTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_SharedTournaments_TeamTournamentId",
                table: "SharedTournament",
                newName: "IX_SharedTournament_TeamTournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_SharedTournaments_AppUserId",
                table: "SharedTournament",
                newName: "IX_SharedTournament_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamMatch",
                table: "TeamMatch",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedTournament",
                table: "SharedTournament",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedTournament_AspNetUsers_AppUserId",
                table: "SharedTournament",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedTournament_TeamTournaments_TeamTournamentId",
                table: "SharedTournament",
                column: "TeamTournamentId",
                principalTable: "TeamTournaments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMatch_Teams_GuestTeamId",
                table: "TeamMatch",
                column: "GuestTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMatch_Teams_HostTeamId",
                table: "TeamMatch",
                column: "HostTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMatch_TeamTournaments_TeamTournamentId",
                table: "TeamMatch",
                column: "TeamTournamentId",
                principalTable: "TeamTournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
