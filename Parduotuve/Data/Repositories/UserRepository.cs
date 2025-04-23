using Microsoft.EntityFrameworkCore;
using Parduotuve.Data.Entities;
using Parduotuve.Helpers.Wrappers;

namespace Parduotuve.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreDataContext _context;

        public UserRepository(StoreDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.Where(user => user.Username.Equals(username)).FirstAsync();
        }

        public async Task<string?> GetNewSessionAsync(User user)
        {
            string newId = Guid.NewGuid().ToString();

            await _context.Sessions.AddAsync(new Session() { Id = newId, User = user });
            await _context.SaveChangesAsync();
            
            return newId;
        }

        public async Task DeleteSessionAsync(string sessionId)
        {
            Session? session = await _context.Sessions.FindAsync(sessionId);

            _context.Sessions.Remove(session);
            
            await _context.SaveChangesAsync();
        }

        public async Task ClearSessionsAsync(User user)
        {
            List<Session?> toRemove = await _context.Sessions.Where(a => a.User.Equals(user)).ToListAsync();
            
            _context.Sessions.RemoveRange(toRemove);
            await _context.SaveChangesAsync();
        }
        public async Task<User?> GetBySessionIdAsync(string id)
        {
            return (await _context.Sessions.Include(session => session!.User).FirstAsync())?.User;
        }

        public async Task<Result> AddAsync(User user)
        {
            if (await _context.Users.Where(other => other.Username.Equals(user.Username)).AnyAsync())
            {
                return Result.Err("User already exists");
            }
            
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Result.Ok();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
