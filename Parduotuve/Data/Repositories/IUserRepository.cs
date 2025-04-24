using Parduotuve.Data.Entities;
using Parduotuve.Helpers.Wrappers;

namespace Parduotuve.Data.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<Result> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task<User?> GetByUsernameAsync(string username);
    Task<string?> GetNewSessionAsync(User user);
    Task ClearSessionsAsync(User user);
    Task DeleteSessionAsync(string sessionId);
    Task<User?> GetBySessionIdAsync(string sessionId);
}