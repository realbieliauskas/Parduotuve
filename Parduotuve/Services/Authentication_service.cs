using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.JSInterop;
using Parduotuve.Data;
using Parduotuve.Data.Entities;
namespace Parduotuve.Services
{
    public class AuthService
    {
        private readonly StoreDataContext db;
        public event Action OnAuthStateChanged;
        private readonly IJSRuntime js;
        public User CurrentUser { get; set; }
        public string Role = "Guest";
        public bool Is_loged_in;

        public AuthService(StoreDataContext db, IJSRuntime js)
        {
            this.db = db;
            this.js = js;
            Is_loged_in = false;
        }


        public async Task<User> LoginAsync(string username, string password)
        {
            if (username=="alv" && password=="crz")
            {

            }
            else
            {
                var user = await db.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
                if (user != null)
                {
                    Is_loged_in = true;
                    CurrentUser = user;
                    Role = user.Role;
                    await js.InvokeVoidAsync("localStorage.setItem", "loggedUser", user.Username);
                    OnAuthStateChanged?.Invoke();
                    return user;
                }
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
        public async Task<User> Set_admin()
        {
            Role = "Admin";
            Is_loged_in = true;
            CurrentUser = new User { Username = "alv", Password = "crz", Role = "Admin" };
            await js.InvokeVoidAsync("localStorage.setItem", "loggedUser", CurrentUser.Username);
            OnAuthStateChanged?.Invoke();
            return null;
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
            Role = "Guest";
            Is_loged_in = false;
            js.InvokeVoidAsync("localStorage.removeItem", "loggedUser");
            OnAuthStateChanged?.Invoke();
        }
        public async Task RestoreLoginAsync()
        {
            var username = await js.InvokeAsync<string>("localStorage.getItem", "loggedUser");

            if (!string.IsNullOrEmpty(username))
            {
                if (username!="alv")
                {
                    var user = await db.Users.FirstOrDefaultAsync(u => u.Username == username);
                    if (user != null)
                    {
                        CurrentUser = user;
                        Role = user.Role;
                        Is_loged_in = true;
                        OnAuthStateChanged?.Invoke();
                    }
                }
                else
                {
                    CurrentUser = new User { Username = "alv", Password = "crz", Role = "Admin" };
                    Is_loged_in = true;
                    Role = "Admin";
                    OnAuthStateChanged?.Invoke();
                }
            }
        }
    }
}