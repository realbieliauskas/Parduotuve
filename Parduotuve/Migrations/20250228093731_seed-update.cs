using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parduotuve.Migrations
{
    /// <inheritdoc />
    public partial class seedupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ChromaPrices", "ChromaURLs", "Chromas", "Quote" },
                values: new object[] { "290;290;Loot Exclusive;290;290;Bundle Exclusive", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/8/82/Spirit_Blossom_Ahri_Ahri-versary.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/f/f1/Spirit_Blossom_Ahri_Aquamarine.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/b/b3/Spirit_Blossom_Ahri_Night_Blossom.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/b/bf/Spirit_Blossom_Ahri_Obsidian.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/b/be/Spirit_Blossom_Ahri_Pearl.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/7/76/Spirit_Blossom_Ahri_Rose_Quartz.png", "Ahri-versary;Aquamarine;Night Blossom;Obsidian;Pearl;Rose Quartz", "The famed Spirit of Salvation, and the fox all mortals are beckoned towards when their souls arrive to the spirit realm. A capricious,whimsical spirit who sees the fate of the living as a game of chase, she offers the chance for souls to find their final rest… but will not intervene if they stray from the path." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ChromaPrices", "ChromaURLs", "Chromas", "Quote" },
                values: new object[] { null, null, null, null });
        }
    }
}
