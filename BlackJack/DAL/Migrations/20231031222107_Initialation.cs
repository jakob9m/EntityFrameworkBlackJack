using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initialation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Games_GId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Games_GameId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Players_PId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Players_PlayerId",
                table: "Results");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Results",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_GId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_PlayerId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "PId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "GId",
                table: "Results");

            migrationBuilder.AlterColumn<string>(
                name: "PlayerId",
                table: "Results",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<string>(
                name: "GameId",
                table: "Results",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1);

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_Results",
                table: "Results",
                columns: new[] { "PlayerId", "GameId" });

            migrationBuilder.CreateIndex(
                name: "IX_Results_GameId1",
                table: "Results",
                column: "GameId1");

            migrationBuilder.CreateIndex(
                name: "IX_Results_PlayerId1",
                table: "Results",
                column: "PlayerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Games_GameId",
                table: "Results",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Games_GameId1",
                table: "Results",
                column: "GameId1",
                principalTable: "Games",
                principalColumn: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Players_PlayerId",
                table: "Results",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Players_PlayerId1",
                table: "Results",
                column: "PlayerId1",
                principalTable: "Players",
                principalColumn: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Games_GameId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Games_GameId1",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Players_PlayerId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Players_PlayerId1",
                table: "Results");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Results",
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

            migrationBuilder.AlterColumn<string>(
                name: "GameId",
                table: "Results",
                type: "nvarchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "PlayerId",
                table: "Results",
                type: "nvarchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255)
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<string>(
                name: "PId",
                table: "Results",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<string>(
                name: "GId",
                table: "Results",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Results",
                table: "Results",
                columns: new[] { "PId", "GId" });

            migrationBuilder.CreateIndex(
                name: "IX_Results_GId",
                table: "Results",
                column: "GId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_PlayerId",
                table: "Results",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Games_GId",
                table: "Results",
                column: "GId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Games_GameId",
                table: "Results",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Players_PId",
                table: "Results",
                column: "PId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Players_PlayerId",
                table: "Results",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId");
        }
    }
}
