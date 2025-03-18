using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Parduotuve.Services
{
    public class  DBAccounts_service : DbContext
    {
        public DbSet<Accounts> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=Store.db"); 
        public class Accounts
        {
            [Key]
            public int ID { get; set; }
            [Required]
            public string Username { get; set; }
            [Required]
            public string Password { get; set; }
            [Required]
            public string Email { get; set; }
            public Accounts() { }
            public Accounts(int id, string username, string password, string email)
            {
                ID = id;
                Username = username;
                Password = password;
                Email = email;
            }
        }
        public void SeedAdmin()
        {
            if (!Users.Any()) 
            {
                var admin = new Accounts("Username,Password,admin@example.com".GetHashCode(), "Username", "Password", "admin@example.com");
                Users.Add(admin);
                SaveChanges();
            }
        }

        public bool ValidateUser(string username, string password)
        {
            var user = Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            return user != null ? true : false;
        }
        public bool RegisterUser(string username, string password, string email)
        {
            if (Users.Any(u => u.Username == username))
            {
                return false;
            }
            Accounts user = new Accounts($"{username}{password}{email}".GetHashCode(),username, password,email);
            Users.Add(user);
            SaveChanges();  
            return true;
        }
    }
}
