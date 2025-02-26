using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parduotuve.Migrations
{
    /// <inheritdoc />
    public partial class added_chromas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Chromas",
                table: "Skins",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Chromas", "CinematicSplashUrl" },
                values: new object[] { null, "https://lol.fandom.com/wiki/Spirit_Blossom_Ahri?file=Skin_Splash_Spirit_Blossom_Ahri.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chromas",
                table: "Skins");

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 1,
                column: "CinematicSplashUrl",
                value: null);
        }
    }
}
