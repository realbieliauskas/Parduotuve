using Microsoft.EntityFrameworkCore;
using Parduotuve.Data.Entities;

namespace Parduotuve.Data
{
    public class StoreDataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbSet<Skin> Skins
        {
            get;
            set;
        }
        public DbSet<User> Users
        {
            get;
            set;
        }
        public StoreDataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Configuration.GetConnectionString("StoreDB"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skin>()
                .ToTable("Skins");


            modelBuilder.Entity<Skin>()
                .HasData(
                    new Skin
                    {
                        Id = 1,
                        Name = "Spirit Blossom Ahri",
                        ChampionName = "Ahri",
                        SplashUrl = "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/d/de/Skin_Loading_Screen_Spirit_Blossom_Ahri.jpg",
                        CinematicSplashUrl = "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/e/e9/Skin_Splash_Spirit_Blossom_Ahri.jpg",
                        Price = 25.99,
                        ChromaPrices = "290;290;Loot Exclusive;290;290;Bundle Exclusive",
                        Chromas = "Ahri-versary;Aquamarine;Night Blossom;Obsidian;Pearl;Rose Quartz",
                        ChromaURLs = "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/8/82/Spirit_Blossom_Ahri_Ahri-versary.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/f/f1/Spirit_Blossom_Ahri_Aquamarine.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/b/b3/Spirit_Blossom_Ahri_Night_Blossom.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/b/bf/Spirit_Blossom_Ahri_Obsidian.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/b/be/Spirit_Blossom_Ahri_Pearl.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/7/76/Spirit_Blossom_Ahri_Rose_Quartz.png",
                        Quote = "The famed Spirit of Salvation, and the fox all mortals are beckoned towards when their souls arrive to the spirit realm. " +
                            "A capricious, whimsical spirit who sees the fate of the living as a game of chase, she " +
                            "offers the chance for souls to find their final rest… but will not intervene if they stray from the path."
                    },
                    new Skin
                    {
                        Id = 2,
                        Name = "Infernal Vel'Koz",
                        ChampionName = "Vel'Koz",
                        SplashUrl = "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/5/53/Skin_Loading_Screen_Infernal_Vel%27Koz.jpg",
                        CinematicSplashUrl = "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/0/04/Skin_Splash_Infernal_Vel%27Koz.jpg",
                        Price = 16.99,
                        ChromaPrices = "290;290;Loot Exclusive;290;290;Bundle Exclusive",
                        Chromas = "Amethyst;Catseye;Emerald;Rainbow;Rose Quartz;Ruby",
                        ChromaURLs = "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/4/40/Infernal_Vel%27Koz_Amethyst.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/e/e9/Infernal_Vel%27Koz_Catseye.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/9/99/Infernal_Vel%27Koz_Emerald.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/c/cf/Infernal_Vel%27Koz_Rainbow.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/0/0a/Infernal_Vel%27Koz_Rose_Quartz.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/4/48/Infernal_Vel%27Koz_Ruby.png",
                        Quote = "Infernal Vel'Koz is a terrifying creature of pure elemental energy, a being of immense power and destruction. " +
                            "He is a force of nature, a being of pure chaos and destruction, who seeks only to consume and destroy all that stands in his way."
                    },
                    new Skin
                    {
                        Id = 3,
                        Name = "Battle Principal Yuumi",
                        ChampionName = "Yuumi",
                        SplashUrl = "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/8/83/Skin_Loading_Screen_Battle_Principal_Yuumi.jpg",
                        CinematicSplashUrl = "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/c/c0/Skin_Splash_Battle_Principal_Yuumi.jpg",
                        Price = 16.99,
                        ChromaPrices = "290;290;Loot Exclusive;290;290;Bundle Exclusive;290;290;290",
                        Chromas = "Citrine;Formal;Granite;Obsidian;Pearl;Rose Quartz;Ruby;Sapphire;Tanzanite",
                        ChromaURLs = "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/b/b8/Battle_Principal_Yuumi_Citrine.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/6/65/Battle_Principal_Yuumi_Formal.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/9/9f/Battle_Principal_Yuumi_Granite.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/a/ae/Battle_Principal_Yuumi_Obsidian.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/5/5d/Battle_Principal_Yuumi_Pearl.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/e/ef/Battle_Principal_Yuumi_Rose_Quartz.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/c/c7/Battle_Principal_Yuumi_Ruby.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/5/5e/Battle_Principal_Yuumi_Sapphire.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/e/e4/Battle_Principal_Yuumi_Tanzanite.png",
                        Quote = "Battle Principal Yuumi is the headmistress of the prestigious Prancetown School for Magical Cats, where she teaches young yordles the arcane arts. " +
                            "She is a stern but caring teacher, who is always willing to go the extra mile to help her students succeed."
                    },
                    new Skin
                    {
                        Id = 4,
                        Name = "Lunar Empress Lux",
                        ChampionName = "Lux",
                        SplashUrl = "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/0/0a/Skin_Loading_Screen_Lunar_Empress_Lux.jpg",
                        CinematicSplashUrl = "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/d/d2/Skin_Splash_Lunar_Empress_Lux.jpg",
                        Price = 16.99,
                        ChromaPrices = "290;290;Loot Exclusive;290;Bundle Exclusive",
                        Chromas = "Ruby;Peridot;Rose Quartz;Amethyst;Turquoise",
                        ChromaURLs = "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/d/d3/Lunar_Empress_Lux_Amethyst.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/e/ee/Lunar_Empress_Lux_Peridot.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/c/c6/Lunar_Empress_Lux_Rose_Quartz.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/9/9a/Lunar_Empress_Lux_Ruby.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/2/25/Lunar_Empress_Lux_Turquoise.png",
                        Quote = "The Lunar Empress is a figure of myth and legend, a celestial being who descends from the heavens to protect the world from the forces of darkness. " +
                            "She is a beacon of hope, a symbol of the power of the moon, and a guardian of the stars."
                    },
                    new Skin
                    {
                        Id = 5,
                        Name = "Omega Squad Veigar",
                        ChampionName = "Veigar",
                        SplashUrl = "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/5/5c/Skin_Loading_Screen_Omega_Squad_Veigar.jpg",
                        CinematicSplashUrl = "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/e/ec/Skin_Splash_Omega_Squad_Veigar.jpg",
                        Price = 16.99,
                        ChromaPrices = "290;290;Loot Exclusive",
                        Chromas = "Sapphire;Tanzanite;Catseye",
                        ChromaURLs = "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/c/cb/Omega_Squad_Veigar_Catseye.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/d/de/Omega_Squad_Veigar_Sapphire.png[]" +
                            "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/b/b5/Omega_Squad_Veigar_Tanzanite.png",
                        Quote = "A heavy artillery specialist with a serious chip on his shoulder, Veigar has a troubling habit of calling down huge amounts of explosive ordnance for even the most mundane problems. He thinks of it as “a fun quirk.”"
                    }
                );

            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = 1,
                        Name = "guy",
                    }
                );
        }
    }
}