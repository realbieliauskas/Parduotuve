using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parduotuve.Migrations
{
    /// <inheritdoc />
    public partial class touch_ups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chromas_Skins_SkinId",
                table: "Chromas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chromas",
                table: "Chromas");

            migrationBuilder.DropIndex(
                name: "IX_Chromas_SkinId",
                table: "Chromas");

            migrationBuilder.DropColumn(
                name: "SkinId",
                table: "Chromas");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Chromas",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Chromas",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "SkinId",
                table: "Chromas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chromas",
                table: "Chromas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Chromas_SkinId",
                table: "Chromas",
                column: "SkinId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chromas_Skins_SkinId",
                table: "Chromas",
                column: "SkinId",
                principalTable: "Skins",
                principalColumn: "Id");
        }
    }
}
