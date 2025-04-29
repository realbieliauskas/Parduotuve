using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using Parduotuve.Data;
using Parduotuve.Data.Entities;
using Parduotuve.Data.Repositories;
using Xunit;

namespace Parduotuve.Tests.Repositories;

public class ChromaRepoTests : IDisposable
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<StoreDataContext> _contextOptions;
    private List<Chroma> chromaList;
    private List<Skin> skinList;

    public ChromaRepoTests()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        _contextOptions = new DbContextOptionsBuilder<StoreDataContext>()
            .UseSqlite(_connection)
            .Options;

        using var context = new StoreDataContext(_contextOptions);

        context.Database.EnsureCreated();

        // Create two skin objects
        var ahriSkin = new Skin
        {
            Id = 1,
            Name = "Spirit Blossom Ahri",
            ChampionName = "Ahri",
            SplashUrl = "https://example.com/spirit-blossom-ahri.jpg",
            CinematicSplashUrl = "https://example.com/spirit-blossom-ahri-cinematic.jpg",
            Price = 1350.0,
            Quote = "Your spirit calls to me.",
            ChromaList = new List<Chroma>()
        };

        var kaynSkin = new Skin
        {
            Id = 2,
            Name = "Odyssey Kayn",
            ChampionName = "Kayn",
            SplashUrl = "https://example.com/odyssey-kayn.jpg",
            CinematicSplashUrl = "https://example.com/odyssey-kayn-cinematic.jpg",
            Price = 1820.0,
            Quote = "The stars themselves obey me.",
            ChromaList = new List<Chroma>()
        };

        // Create chromas for Ahri (2 chromas)
        var ahriChroma1 = new Chroma
        {
            Id = 1,
            Name = "Rose Quartz",
            Url = "https://example.com/ahri-rose-quartz.jpg",
            Price = "290",
            Skin = ahriSkin
        };

        var ahriChroma2 = new Chroma
        {
            Id = 2,
            Name = "Aquamarine",
            Url = "https://example.com/ahri-aquamarine.jpg",
            Price = "290",
            Skin = ahriSkin
        };

        // Create chromas for Kayn (3 chromas)
        var kaynChroma1 = new Chroma
        {
            Id = 3,
            Name = "Ruby",
            Url = "https://example.com/kayn-ruby.jpg",
            Price = "290",
            Skin = kaynSkin
        };

        var kaynChroma2 = new Chroma
        {
            Id = 4,
            Name = "Sapphire",
            Url = "https://example.com/kayn-sapphire.jpg",
            Price = "290",
            Skin = kaynSkin
        };

        var kaynChroma3 = new Chroma
        {
            Id = 5,
            Name = "Obsidian",
            Url = "https://example.com/kayn-obsidian.jpg",
            Price = "290",
            Skin = kaynSkin
        };

        // Add chromas to their respective skins' ChromaList
        ahriSkin.ChromaList.Add(ahriChroma1);
        ahriSkin.ChromaList.Add(ahriChroma2);

        kaynSkin.ChromaList.Add(kaynChroma1);
        kaynSkin.ChromaList.Add(kaynChroma2);
        kaynSkin.ChromaList.Add(kaynChroma3);

        // Add skins to the context
        context.Skins.Add(ahriSkin);
        context.Skins.Add(kaynSkin);

        chromaList =
        [
            ahriChroma1,
            ahriChroma2,
            kaynChroma1,
            kaynChroma2,
            kaynChroma3,
        ];
        skinList = 
        [
            ahriSkin,
            kaynSkin,
        ];
        context.SaveChanges();
    }

    StoreDataContext CreateContext() => new StoreDataContext(_contextOptions);

    public void Dispose()
    {
        _connection.Dispose();
    }

    // MethodName_StateUnderTest_ExpectedBehavior
    // example: isAdult_AgeLessThan18_False
    [Fact]
    public async Task GetByIdAsync_PassedId1_ReturnAhriChroma1()
    {
        using var context = CreateContext();
        var repo = new ChromaRepository(context);
        Chroma expected = new Chroma(1, "Rose Quartz", "https://example.com/ahri-rose-quartz.jpg", "290", skinList.First());

        Chroma actual = await repo.GetByIdAsync(1);
       
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetAllAsync_Called_ReturnAll5Chromas()
    {
        using var context = CreateContext();
        var repo = new ChromaRepository(context);

        List<Chroma> actual = (await repo.GetAllAsync()).ToList();

        Assert.Equal(chromaList, actual);
    }

    [Fact]
    public async Task AddAsync_AddAhriChroma_ReturnNewAhriChromaById()
    {
        using var context = CreateContext();
        var repo = new ChromaRepository(context);
        var existingSkin = await context.Skins.FindAsync(1);
        Chroma expected = new Chroma(6, "Sunshine", "image.png", "Bundle Exclusive", existingSkin);

        await repo.AddAsync(expected);
        Chroma actual = await repo.GetByIdAsync(6);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task DeleteAsync_RemoveRoseQuartzAhriChroma_ReturnNullWhenAccessingDeletedChroma()
    {
        using var context = CreateContext();
        var repo = new ChromaRepository(context);

        await repo.DeleteAsync(1);
        Chroma actual = await repo.GetByIdAsync(1);

        Assert.Null(actual);
    }

    [Fact]
    public async Task DeleteAsync_RemoveNonExistentChroma_ReturnChromaCount5()
    {
        using var context = CreateContext();
        var repo = new ChromaRepository(context);
        int expected = 5;
        await repo.DeleteAsync(8);
        int actual = (await repo.GetAllAsync()).Count();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetLast_Call_GetObsydianKayn()
    {
        using var context = CreateContext();
        var repo = new ChromaRepository(context);
        Chroma expected = chromaList.Last();

        Chroma actual = await repo.GetLast();

        Assert.Equal(expected, actual);
    }
}