using Microsoft.EntityFrameworkCore;
using Parduotuve.Data.Entities;

namespace Parduotuve.Data
{
    public class StoreDataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbSet<Skin> Skins { get; set; }
        public DbSet<User> Users { get; set; }
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
                        CinematicSplashUrl = "https://lol.fandom.com/wiki/Spirit_Blossom_Ahri?file=Skin_Splash_Spirit_Blossom_Ahri.jpg",
                        Price = 91.99,
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
