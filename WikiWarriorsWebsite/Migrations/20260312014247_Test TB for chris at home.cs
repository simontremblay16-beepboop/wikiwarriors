using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WikiWarriorsWebsite.Migrations
{
    /// <inheritdoc />
    public partial class TestTBforchrisathome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FightHistory_Fighter_Fighter1Id",
                table: "FightHistory");

            migrationBuilder.AlterColumn<int>(
                name: "Fighter1Id",
                table: "FightHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_FightHistory_Fighter_Fighter1Id",
                table: "FightHistory",
                column: "Fighter1Id",
                principalTable: "Fighter",
                principalColumn: "FighterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FightHistory_Fighter_Fighter1Id",
                table: "FightHistory");

            migrationBuilder.AlterColumn<int>(
                name: "Fighter1Id",
                table: "FightHistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FightHistory_Fighter_Fighter1Id",
                table: "FightHistory",
                column: "Fighter1Id",
                principalTable: "Fighter",
                principalColumn: "FighterId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
