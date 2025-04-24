using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Parduotuve.Data.Entities;
using Parduotuve.Data.Enums;
using Parduotuve.Data.Repositories;
using Parduotuve.Helpers.Wrappers;

namespace Parduotuve.Services
{
    public class AuthService
    {
        private const string SessionIdKey = "SessionId";
        private readonly IUserRepository _userRepo;
        private readonly ProtectedLocalStorage _localStorage;
        public User? CurrentUser { get; private set; }
        public UserRole Role => CurrentUser?.Role ?? UserRole.Guest;
        public bool IsLoggedIn => CurrentUser != null;
        public bool IsAdmin => Role.Equals(UserRole.Admin);
        public bool IsInitialized { get; private set; }
        
        public AuthService(IUserRepository userRepository, ProtectedLocalStorage localStorage)
        {
            _userRepo = userRepository;
            _localStorage = localStorage;
        }

        public async Task InitializeAsync()
        {
            if (!IsInitialized)
            {
                CurrentUser = await FetchUser();
                IsInitialized = true;
            }
        }
        
        private async Task<User?> FetchUser()
        {
            ProtectedBrowserStorageResult<string> res = await _localStorage.GetAsync<string>(SessionIdKey);
            
            if (!res.Success || string.IsNullOrEmpty(res.Value))
            {
                return null;
            }
            User? user = _userRepo.GetBySessionIdAsync(res.Value).Result;
            
            return user;
        }

        private async Task<Result> InjectSessionId(User user)
        {
            string? id = await _userRepo.GetNewSessionAsync(user);
            if (id == null)
            {
                return Result.Err($"Session ID creation failed for user {user.Username}");
            }

            await _localStorage.SetAsync(SessionIdKey, id);
            return Result.Ok();
        }

        private async Task DestroySessionId()
        {
            ProtectedBrowserStorageResult<string> res = await _localStorage.GetAsync<string>(SessionIdKey);
            if (res.Success)
            {
                await _userRepo.DeleteSessionAsync(res.Value ?? "");
            }
            await _localStorage.DeleteAsync(SessionIdKey);
        }
        public async Task<Result<User>> LoginAsync(string username, string password)
        {
            User? user = await _userRepo.GetByUsernameAsync(username);
            if (user == null)
            {
                return Result<User>.Err($"Invalid username: {username}");
            }

            if (user.Password != password)
            {
                return Result<User>.Err($"Invalid password for user: {username}");
            }

            Result injectResult = await InjectSessionId(user);

            if (!injectResult)
            {
                return Result<User>.Err(injectResult.Message);
            }
            
            CurrentUser = user;
            return Result<User>.Ok(user);
        }


        public async Task<Result<User>> RegisterAsync(string email, string username, string password, UserRole role)
        {
            User newUser = new User { Username = username, Password = password, Role = role, Email = email };

            Result addResult = await _userRepo.AddAsync(newUser);

            return addResult ? Result<User>.Ok(newUser) : Result<User>.Err(addResult.Message);
        }
        public async Task Logout()
        {
            CurrentUser = null;
            await DestroySessionId();
        }
    }
}