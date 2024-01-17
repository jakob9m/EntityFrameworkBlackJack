using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class MigrateBlackJack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Games_GameId1",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Players_PlayerId1",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_GameId1",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_PlayerId1",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "GameId1",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "PlayerId1",
                table: "Results");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameId1",
                table: "Results",
                type: "nvarchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlayerId1",
                table: "Results",
                type: "nvarchar(255)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Results_GameId1",
                table: "Results",
                column: "GameId1");

            migrationBuilder.CreateIndex(
                name: "IX_Results_PlayerId1",
                table: "Results",
                column: "PlayerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Games_GameId1",
                table: "Results",
                column: "GameId1",
                principalTable: "Games",
                principalColumn: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Players_PlayerId1",
                table: "Results",
                column: "PlayerId1",
                principalTable: "Players",
                principalColumn: "PlayerId");
        }
    }
}
