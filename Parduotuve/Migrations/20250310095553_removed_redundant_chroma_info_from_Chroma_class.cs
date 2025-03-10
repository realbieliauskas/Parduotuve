using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parduotuve.Migrations
{
    /// <inheritdoc />
    public partial class removed_redundant_chroma_info_from_Chroma_class : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChromaPrices",
                table: "Skins");

            migrationBuilder.DropColumn(
                name: "ChromaURLs",
                table: "Skins");

            migrationBuilder.DropColumn(
                name: "Chromas",
                table: "Skins");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChromaPrices",
                table: "Skins",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChromaURLs",
                table: "Skins",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Chromas",
                table: "Skins",
                type: "TEXT",
                nullable: true);
        }
    }
}
