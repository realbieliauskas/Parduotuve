using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Parduotuve.Data;
using Parduotuve.Data.Entities;
using Parduotuve.Data.Enums;
using Parduotuve.Data.Repositories;
using Parduotuve.Helpers.Wrappers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Parduotuve.Tests.Repositories
{
    public class UserRepoTests : IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<StoreDataContext> _contextOptions;

        public UserRepoTests()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _contextOptions = new DbContextOptionsBuilder<StoreDataContext>()
                .UseSqlite(_connection)
                .Options;

            using var context = new StoreDataContext(_contextOptions);
            context.Database.EnsureCreated();

            context.Users.AddRange(
                new User { Username = "user1", Password = "pass1", Email = "user1@example.com", Role = UserRole.Admin },
                new User { Username = "user2", Password = "pass2", Email = "user2@example.com", Role = UserRole.User },
                new User { Username = "user3", Password = "pass3", Email = "user3@example.com", Role = UserRole.User }
            );
            context.SaveChanges();
        }

        private StoreDataContext CreateContext() => new StoreDataContext(_contextOptions);

        [Fact]
        public async Task GetAllAsync_ReturnsAllUsers()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserRepository(context);
            int expected = 4; // alv is always added + 3 custom users

            // Act
            var users = await repository.GetAllAsync();

            // Assert
            Assert.Equal(expected, users.Count());
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ReturnsUser()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserRepository(context);

            // Act
            var user = await repository.GetByIdAsync(3);

            // Assert
            Assert.NotNull(user);
            Assert.Equal("user1", user.Username);
        }

        [Fact]
        public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserRepository(context);

            // Act
            var user = await repository.GetByIdAsync(999);

            // Assert
            Assert.Null(user);
        }

        [Fact]
        public async Task GetByUsernameAsync_WithValidUsername_ReturnsUser()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserRepository(context);

            // Act
            var user = await repository.GetByUsernameAsync("user2");

            // Assert
            Assert.NotNull(user);
            Assert.Equal("user2@example.com", user.Email);
        }

        [Fact]
        public async Task GetByUsernameAsync_WithInvalidUsername_ReturnsNull()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserRepository(context);


            // Act
            var user = await repository.GetByUsernameAsync("nonexistent");

            // Assert
            Assert.Null(user);
        }

        [Fact]
        public async Task GetNewSessionAsync_CreatesNewSession()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserRepository(context);

            var user = await context.Users.FindAsync(3);

            // Act
            var sessionId = await repository.GetNewSessionAsync(user);

            // Assert
            Assert.NotNull(sessionId);
            Assert.Single(context.Sessions);
            var session = await context.Sessions.FirstAsync();
            Assert.Equal(sessionId, session.Id);
            Assert.Equal(user.Id, session.User.Id);
        }

        [Fact]
        public async Task DeleteSessionAsync_RemovesSession()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserRepository(context);
            var user = await context.Users.FindAsync(3);
            var sessionId = await repository.GetNewSessionAsync(user);

            // Act
            await repository.DeleteSessionAsync(sessionId);

            // Assert
            Assert.Empty(context.Sessions);
        }

        [Fact]
        public async Task ClearSessionsAsync_RemovesAllUserSessions()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserRepository(context);

            var user1 = await context.Users.FindAsync(3);
            var user2 = await context.Users.FindAsync(4);

            // Create multiple sessions for different users
            await repository.GetNewSessionAsync(user1);
            await repository.GetNewSessionAsync(user1);
            await repository.GetNewSessionAsync(user2);

            // Act
            await repository.ClearSessionsAsync(user1);

            // Assert
            Assert.Single(context.Sessions); // Only user2's session should remain
            var remainingSession = await context.Sessions.Include(s => s.User).FirstAsync();
            Assert.Equal(user2.Id, remainingSession.User.Id);
        }

        [Fact]
        public async Task GetBySessionIdAsync_WithValidId_ReturnsUser()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserRepository(context);

            var initialUser = await context.Users.FindAsync(3);
            var sessionId = await repository.GetNewSessionAsync(initialUser);

            // Act
            var user = await repository.GetBySessionIdAsync(sessionId);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(initialUser.Id, user.Id);
            Assert.Equal(initialUser.Username, user.Username);
        }

        [Fact]
        public async Task AddAsync_WithNewUser_AddsUser()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserRepository(context);

            var user = new User
            {
                Username = "newuser",
                Password = "password",
                Email = "newuser@example.com",
                Role = UserRole.User
            };

            // Act
            var result = await repository.AddAsync(user);

            // Assert
            var savedUser = await repository.GetByIdAsync(6);
            Assert.Equal("newuser", savedUser.Username);
            Assert.Equal("newuser@example.com", savedUser.Email);
        }

        [Fact]
        public async Task AddAsync_WithExistingUsername_ReturnsError()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserRepository(context);
            int ogCount = (await repository.GetAllAsync()).Count();
            var duplicateUser = new User
            {
                Username = "user1", // Already exists
                Password = "password",
                Email = "different@example.com",
                Role = UserRole.User
            };

            // Act
            var result = await repository.AddAsync(duplicateUser);

            // Assert
            Assert.False(result.IsSuccess); // Verify the operation failed
            var users = await repository.GetAllAsync();
            Assert.Equal(ogCount, users.Count()); // Count remains the same
        }

        [Fact]
        public async Task UpdateAsync_UpdatesUser()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserRepository(context);

            var user = await context.Users.FindAsync(3);
            user.Email = "updated@example.com";
            user.Password = "newpassword";

            // Act
            await repository.UpdateAsync(user);

            // Assert
            var updatedUser = await context.Users.FindAsync(3);
            Assert.Equal("updated@example.com", updatedUser.Email);
            Assert.Equal("newpassword", updatedUser.Password);
        }

        [Fact]
        public async Task DeleteAsync_RemovesUser()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserRepository(context);

            // Act
            await repository.DeleteAsync(5);

            // Assert
            Assert.Equal(3, context.Users.Count());
            Assert.Null(await context.Users.FindAsync(5));
        }

        [Fact]
        public async Task DeleteAsync_WithNonExistentId_DoesNotThrowError()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserRepository(context);

            // Act & Assert
            await repository.DeleteAsync(999); // Should not throw
            Assert.Equal(4, context.Users.Count()); // Count remains the same
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}