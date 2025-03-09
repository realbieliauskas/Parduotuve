using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parduotuve.Migrations
{
    /// <inheritdoc />
    public partial class added_chroma_list_to_skin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SkinId",
                table: "Chromas",
                type: "INTEGER",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chromas_Skins_SkinId",
                table: "Chromas");

            migrationBuilder.DropIndex(
                name: "IX_Chromas_SkinId",
                table: "Chromas");

            migrationBuilder.DropColumn(
                name: "SkinId",
                table: "Chromas");
        }
    }
}
