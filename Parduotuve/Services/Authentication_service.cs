using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Parduotuve.Data;
using Parduotuve.Data.Entities;
namespace Parduotuve.Services
{
    public class AuthService
    {
        private readonly StoreDataContext db;
        public event Action OnAuthStateChanged;
        public User CurrentUser { get; set; }
        public string Role = "Guest";
        public bool Is_loged_in = false;

        public AuthService(StoreDataContext db)
        {
            this.db = db;
        }


        public async Task<User> LoginAsync(string username, string password)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                Is_loged_in = true;
                return user;
            }
            return null;
        }


        public async Task<bool> RegisterAsync(string name, string surname, string email, string username, string password, string role)
        {
            var existingUser = await db.Users.AnyAsync(u => u.Username == username);
            if (existingUser)
            {
                return false;
            }
            var newUser = new User { Username = username, Password = password, Role = role };
            db.Users.Add(newUser);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<User> Check_if_user_exists(string username)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
        public bool IsAdmin()
        {
            return Role == "Admin";
        }
        public bool IsLoggedIn()
        {
            return Is_loged_in;
        }
        public void Set_admin()
        {
            Role = "Admin";
            Is_loged_in = true;
            OnAuthStateChanged?.Invoke();
        }
        public void Set_Guest()
        {
            Role = "Guest";
            Is_loged_in = true;
            OnAuthStateChanged?.Invoke();
        }
        public void Logout()
        {
            CurrentUser = null;
            OnAuthStateChanged?.Invoke();
        }
    }
}