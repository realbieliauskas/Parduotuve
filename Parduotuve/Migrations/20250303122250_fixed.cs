using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parduotuve.Migrations
{
    /// <inheritdoc />
    public partial class @fixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CinematicSplashUrl", "SplashUrl" },
                values: new object[] { "https://motionbgs.com/media/6951/spirit-blossom-ahri.jpg", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/d/de/Skin_Loading_Screen_Spirit_Blossom_Ahri.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CinematicSplashUrl", "SplashUrl" },
                values: new object[] { "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/e/e9/Skin_Splash_Spirit_Blossom_Ahri.jpg", "https://motionbgs.com/media/6951/spirit-blossom-ahri.jpg" });
        }
    }
}
