using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Parduotuve.Data;
using Xunit;

namespace Parduotuve.Tests.Repositories;

public class ChromaRepoTests : IDisposable
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<StoreDataContext> _contextOptions;

    public ChromaRepoTests()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        _contextOptions = new DbContextOptionsBuilder<StoreDataContext>()
            .UseSqlite(_connection)
            .Options;

        using var context = new StoreDataContext(_contextOptions);

        context.Database.EnsureCreated();

        context.SaveChanges();
    }

    StoreDataContext CreateContext() => new StoreDataContext(_contextOptions);
    
    public void Dispose()
    {
        _connection.Dispose();
    }
}