using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PingPong.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Wins = table.Column<int>(nullable: false),
                    Losses = table.Column<int>(nullable: false),
                    Total = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    IsWinner = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerResult_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Player1ResultId = table.Column<int>(nullable: false),
                    Player2ResultId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_PlayerResult_Player1ResultId",
                        column: x => x.Player1ResultId,
                        principalTable: "PlayerResult",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Games_PlayerResult_Player2ResultId",
                        column: x => x.Player2ResultId,
                        principalTable: "PlayerResult",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_Player1ResultId",
                table: "Games",
                column: "Player1ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Player2ResultId",
                table: "Games",
                column: "Player2ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerResult_PlayerId",
                table: "PlayerResult",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "PlayerResult");

            migrationBuilder.DropTable(
                name: "Player");
        }
    }
}
