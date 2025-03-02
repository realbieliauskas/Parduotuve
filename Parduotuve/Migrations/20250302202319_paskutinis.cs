using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Parduotuve.Migrations
{
    /// <inheritdoc />
    public partial class paskutinis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ChromaPrices", "CinematicSplashUrl", "Price", "Quote" },
                values: new object[] { null, "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/e/e9/Skin_Splash_Spirit_Blossom_Ahri.jpg", 25.989999999999998, "The famed Spirit of Salvation, and the fox all mortals are beckoned towards when their souls arrive to the spirit realm. A capricious, whimsical spirit who sees the fate of the living as a game of chase, she offers the chance for souls to find their final rest… but will not intervene if they stray from the path." });

            migrationBuilder.InsertData(
                table: "Skins",
                columns: new[] { "Id", "ChampionName", "ChromaPrices", "ChromaURLs", "Chromas", "CinematicSplashUrl", "Name", "Price", "Quote", "SplashUrl" },
                values: new object[,]
                {
                    { 2, "Vel'Koz", null, "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/4/40/Infernal_Vel%27Koz_Amethyst.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/e/e9/Infernal_Vel%27Koz_Catseye.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/9/99/Infernal_Vel%27Koz_Emerald.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/c/cf/Infernal_Vel%27Koz_Rainbow.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/0/0a/Infernal_Vel%27Koz_Rose_Quartz.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/4/48/Infernal_Vel%27Koz_Ruby.png", "Amethyst;Catseye;Emerald;Rainbow;Rose Quartz;Ruby", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/0/04/Skin_Splash_Infernal_Vel%27Koz.jpg", "Infernal Vel'Koz", 16.989999999999998, null, "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/5/53/Skin_Loading_Screen_Infernal_Vel%27Koz.jpg" },
                    { 3, "Yuumi", null, "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/b/b8/Battle_Principal_Yuumi_Citrine.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/6/65/Battle_Principal_Yuumi_Formal.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/9/9f/Battle_Principal_Yuumi_Granite.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/a/ae/Battle_Principal_Yuumi_Obsidian.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/5/5d/Battle_Principal_Yuumi_Pearl.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/e/ef/Battle_Principal_Yuumi_Rose_Quartz.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/c/c7/Battle_Principal_Yuumi_Ruby.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/5/5e/Battle_Principal_Yuumi_Sapphire.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/e/e4/Battle_Principal_Yuumi_Tanzanite.png", "Citrine;Formal;Granite;Obsidian;Pearl;Rose Quartz;Ruby;Sapphire;Tanzanite", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/c/c0/Skin_Splash_Battle_Principal_Yuumi.jpg", "Battle Principal Yuumi", 16.989999999999998, null, "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/8/83/Skin_Loading_Screen_Battle_Principal_Yuumi.jpg" },
                    { 4, "Lux", null, "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/d/d3/Lunar_Empress_Lux_Amethyst.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/e/ee/Lunar_Empress_Lux_Peridot.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/c/c6/Lunar_Empress_Lux_Rose_Quartz.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/9/9a/Lunar_Empress_Lux_Ruby.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/2/25/Lunar_Empress_Lux_Turquoise.png", "Ruby;Peridot;Rose Quartz;Amethyst;Turquoise", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/d/d2/Skin_Splash_Lunar_Empress_Lux.jpg", "Lunar Empress Lux", 16.989999999999998, null, "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/0/0a/Skin_Loading_Screen_Lunar_Empress_Lux.jpg" },
                    { 5, "Veigar", null, "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/c/cb/Omega_Squad_Veigar_Catseye.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/d/de/Omega_Squad_Veigar_Sapphire.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/b/b5/Omega_Squad_Veigar_Tanzanite.png", "Sapphire;Tanzanite;Catseye", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/e/ec/Skin_Splash_Omega_Squad_Veigar.jpg", "Omega Squad Veigar", 16.989999999999998, "A heavy artillery specialist with a serious chip on his shoulder, Veigar has a troubling habit of calling down huge amounts of explosive ordnance for even the most mundane problems. He thinks of it as “a fun quirk.”", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/5/5c/Skin_Loading_Screen_Omega_Squad_Veigar.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ChromaPrices", "CinematicSplashUrl", "Price", "Quote" },
                values: new object[] { "290;290;Loot Exclusive;290;290;Bundle Exclusive", "https://lol.fandom.com/wiki/Spirit_Blossom_Ahri?file=Skin_Splash_Spirit_Blossom_Ahri.jpg", 91.989999999999995, "The famed Spirit of Salvation, and the fox all mortals are beckoned towards when their souls arrive to the spirit realm. A capricious,whimsical spirit who sees the fate of the living as a game of chase, she offers the chance for souls to find their final rest… but will not intervene if they stray from the path." });
        }
    }
}
