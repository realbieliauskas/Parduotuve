using Microsoft.EntityFrameworkCore;
using Parduotuve.Data.Entities;
using Parduotuve.Data.Enums;

namespace Parduotuve.Data;

public class StoreDataContext : DbContext
{
    private readonly IConfiguration _configuration;

    public StoreDataContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Skin> Skins { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Chroma> Chromas { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<Session?> Sessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_configuration.GetConnectionString("StoreDB"));
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
            .HasData(new User
            {
                Id = 1,
                Username = "name",
                Password = "password",
                Email = "name@domain.com",
                Role = UserRole.User
            }, new User
            {
                Id = 2,
                Username = "alv",
                Password = "crz",
                Email = "alv@crz.com",
                Role = UserRole.Admin
            });
    }
}