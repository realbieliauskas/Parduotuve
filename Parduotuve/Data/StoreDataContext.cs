using Microsoft.EntityFrameworkCore;
using Parduotuve.Data.Entities;
using Parduotuve.Data.Enums;

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
        public DbSet<Chroma> Chromas
        {
            get;
            set;
        }
        public DbSet<Order> Orders
        {
            get;
            set;
        }
        public DbSet<OrderItem> OrderItems
        {
            get;
            set;
        }
        public DbSet<Session?> Sessions { get; set; }

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
            modelBuilder.Entity<User>()
                .ToTable("Users");
            modelBuilder.Entity<Chroma>()
                .ToTable("Chromas");
            modelBuilder.Entity<Order>()
                .ToTable("Orders");
            modelBuilder.Entity<OrderItem>()
                .ToTable("OrderItems");

            modelBuilder.Entity<User>()
                .HasData(new []{
                    new User
                    {
                        Id = 1,
                        Username = "name",
                        Password = "password",
                        Role = UserRole.User,
                    },
                    new User
                    {
                        Id = 2,
                        Username = "alv",
                        Password = "crz",
                        Role = UserRole.Admin
                    }
                    }
                );
        }
    }
}