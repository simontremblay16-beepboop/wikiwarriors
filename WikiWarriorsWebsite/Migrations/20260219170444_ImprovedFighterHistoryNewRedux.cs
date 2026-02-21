using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WikiWarriorsWebsite.Migrations
{
    /// <inheritdoc />
    public partial class ImprovedFighterHistoryNewRedux : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FightHistory",
                columns: table => new
                {
                    FightHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fighter1Id = table.Column<int>(type: "int", nullable: false),
                    Fighter2Id = table.Column<int>(type: "int", nullable: true),
                    WinnerId = table.Column<int>(type: "int", nullable: true),
                    FightDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DailyFight = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FightHistory", x => x.FightHistoryId);
                    table.ForeignKey(
                        name: "FK_FightHistory_Fighter_Fighter1Id",
                        column: x => x.Fighter1Id,
                        principalTable: "Fighter",
                        principalColumn: "FighterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FightHistory_Fighter_Fighter2Id",
                        column: x => x.Fighter2Id,
                        principalTable: "Fighter",
                        principalColumn: "FighterId");
                    table.ForeignKey(
                        name: "FK_FightHistory_Fighter_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Fighter",
                        principalColumn: "FighterId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FightHistory_Fighter1Id",
                table: "FightHistory",
                column: "Fighter1Id");

            migrationBuilder.CreateIndex(
                name: "IX_FightHistory_Fighter2Id",
                table: "FightHistory",
                column: "Fighter2Id");

            migrationBuilder.CreateIndex(
                name: "IX_FightHistory_WinnerId",
                table: "FightHistory",
                column: "WinnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FightHistory");
        }
    }
}
