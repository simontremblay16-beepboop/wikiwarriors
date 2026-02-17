using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WikiWarriorsWebsite.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Fighter",
                newName: "FighterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FighterId",
                table: "Fighter",
                newName: "Id");
        }
    }
}
