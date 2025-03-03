using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parduotuve.Migrations
{
    /// <inheritdoc />
    public partial class betterimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 1,
                column: "SplashUrl",
                value: "https://motionbgs.com/media/6951/spirit-blossom-ahri.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 1,
                column: "SplashUrl",
                value: "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/d/de/Skin_Loading_Screen_Spirit_Blossom_Ahri.jpg");
        }
    }
}
