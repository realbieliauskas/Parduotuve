using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Parduotuve.Migrations
{
    /// <inheritdoc />
    public partial class changeseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: 1);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Skins",
                columns: new[] { "Id", "ChampionName", "ChromaPrices", "ChromaURLs", "Chromas", "CinematicSplashUrl", "Name", "Price", "Quote", "SplashUrl" },
                values: new object[,]
                {
                    { 1, "Ahri", "290;290;Loot Exclusive;290;290;Bundle Exclusive", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/8/82/Spirit_Blossom_Ahri_Ahri-versary.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/f/f1/Spirit_Blossom_Ahri_Aquamarine.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/b/b3/Spirit_Blossom_Ahri_Night_Blossom.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/b/bf/Spirit_Blossom_Ahri_Obsidian.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/b/be/Spirit_Blossom_Ahri_Pearl.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/7/76/Spirit_Blossom_Ahri_Rose_Quartz.png", "Ahri-versary;Aquamarine;Night Blossom;Obsidian;Pearl;Rose Quartz", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/e/e9/Skin_Splash_Spirit_Blossom_Ahri.jpg", "Spirit Blossom Ahri", 25.989999999999998, "The famed Spirit of Salvation, and the fox all mortals are beckoned towards when their souls arrive to the spirit realm. A capricious, whimsical spirit who sees the fate of the living as a game of chase, she offers the chance for souls to find their final rest… but will not intervene if they stray from the path.", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/d/de/Skin_Loading_Screen_Spirit_Blossom_Ahri.jpg" },
                    { 2, "Vel'Koz", "290;290;Loot Exclusive;290;290;Bundle Exclusive", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/4/40/Infernal_Vel%27Koz_Amethyst.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/e/e9/Infernal_Vel%27Koz_Catseye.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/9/99/Infernal_Vel%27Koz_Emerald.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/c/cf/Infernal_Vel%27Koz_Rainbow.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/0/0a/Infernal_Vel%27Koz_Rose_Quartz.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/4/48/Infernal_Vel%27Koz_Ruby.png", "Amethyst;Catseye;Emerald;Rainbow;Rose Quartz;Ruby", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/0/04/Skin_Splash_Infernal_Vel%27Koz.jpg", "Infernal Vel'Koz", 16.989999999999998, "Infernal Vel'Koz is a terrifying creature of pure elemental energy, a being of immense power and destruction. He is a force of nature, a being of pure chaos and destruction, who seeks only to consume and destroy all that stands in his way.", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/5/53/Skin_Loading_Screen_Infernal_Vel%27Koz.jpg" },
                    { 3, "Yuumi", "290;290;Loot Exclusive;290;290;Bundle Exclusive;290;290;290", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/b/b8/Battle_Principal_Yuumi_Citrine.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/6/65/Battle_Principal_Yuumi_Formal.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/9/9f/Battle_Principal_Yuumi_Granite.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/a/ae/Battle_Principal_Yuumi_Obsidian.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/5/5d/Battle_Principal_Yuumi_Pearl.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/e/ef/Battle_Principal_Yuumi_Rose_Quartz.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/c/c7/Battle_Principal_Yuumi_Ruby.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/5/5e/Battle_Principal_Yuumi_Sapphire.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/e/e4/Battle_Principal_Yuumi_Tanzanite.png", "Citrine;Formal;Granite;Obsidian;Pearl;Rose Quartz;Ruby;Sapphire;Tanzanite", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/c/c0/Skin_Splash_Battle_Principal_Yuumi.jpg", "Battle Principal Yuumi", 16.989999999999998, "Battle Principal Yuumi is the headmistress of the prestigious Prancetown School for Magical Cats, where she teaches young yordles the arcane arts. She is a stern but caring teacher, who is always willing to go the extra mile to help her students succeed.", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/8/83/Skin_Loading_Screen_Battle_Principal_Yuumi.jpg" },
                    { 4, "Lux", "290;290;Loot Exclusive;290;Bundle Exclusive", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/d/d3/Lunar_Empress_Lux_Amethyst.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/e/ee/Lunar_Empress_Lux_Peridot.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/c/c6/Lunar_Empress_Lux_Rose_Quartz.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/9/9a/Lunar_Empress_Lux_Ruby.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/2/25/Lunar_Empress_Lux_Turquoise.png", "Ruby;Peridot;Rose Quartz;Amethyst;Turquoise", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/d/d2/Skin_Splash_Lunar_Empress_Lux.jpg", "Lunar Empress Lux", 16.989999999999998, "The Lunar Empress is a figure of myth and legend, a celestial being who descends from the heavens to protect the world from the forces of darkness. She is a beacon of hope, a symbol of the power of the moon, and a guardian of the stars.", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/0/0a/Skin_Loading_Screen_Lunar_Empress_Lux.jpg" },
                    { 5, "Veigar", "290;290;Loot Exclusive", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/c/cb/Omega_Squad_Veigar_Catseye.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/d/de/Omega_Squad_Veigar_Sapphire.png[]https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/b/b5/Omega_Squad_Veigar_Tanzanite.png", "Sapphire;Tanzanite;Catseye", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/e/ec/Skin_Splash_Omega_Squad_Veigar.jpg", "Omega Squad Veigar", 16.989999999999998, "A heavy artillery specialist with a serious chip on his shoulder, Veigar has a troubling habit of calling down huge amounts of explosive ordnance for even the most mundane problems. He thinks of it as “a fun quirk.”", "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/5/5c/Skin_Loading_Screen_Omega_Squad_Veigar.jpg" }
                });
        }
    }
}
