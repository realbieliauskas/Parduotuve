// using System;
// using System.Collections.Generic;
// using System.Data.Common;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.Data.Sqlite;
// using Microsoft.EntityFrameworkCore;
// using Parduotuve.Data;
// using Parduotuve.Data.Entities;
// using Parduotuve.Data.Repositories;
// using Xunit;
//
// namespace Parduotuve.Tests.Repositories
// {
//     public class SkinRepoTests : IDisposable
//     {
//         private readonly DbConnection _connection;
//         private readonly DbContextOptions<StoreDataContext> _contextOptions;
//         private List<Chroma> chromaList;
//         private List<Skin> skinList;
//
//         public SkinRepoTests()
//         {
//             _connection = new SqliteConnection("Filename=:memory:");
//             _connection.Open();
//
//             _contextOptions = new DbContextOptionsBuilder<StoreDataContext>()
//                 .UseSqlite(_connection)
//                 .Options;
//
//             using var context = new StoreDataContext(_contextOptions);
//
//             context.Database.EnsureCreated();
//
//             // Create two skin objects
//             var ahriSkin = new Skin
//             {
//                 Id = 1,
//                 Name = "Spirit Blossom Ahri",
//                 ChampionName = "Ahri",
//                 SplashUrl = "https://example.com/spirit-blossom-ahri.jpg",
//                 CinematicSplashUrl = "https://example.com/spirit-blossom-ahri-cinematic.jpg",
//                 Price = 1350.0,
//                 Quote = "Your spirit calls to me.",
//                 ChromaList = new List<Chroma>()
//             };
//
//             var kaynSkin = new Skin
//             {
//                 Id = 2,
//                 Name = "Odyssey Kayn",
//                 ChampionName = "Kayn",
//                 SplashUrl = "https://example.com/odyssey-kayn.jpg",
//                 CinematicSplashUrl = "https://example.com/odyssey-kayn-cinematic.jpg",
//                 Price = 1820.0,
//                 Quote = "The stars themselves obey me.",
//                 ChromaList = new List<Chroma>()
//             };
//
//             // Create chromas for Ahri
//             var ahriChroma1 = new Chroma
//             {
//                 Id = 1,
//                 Name = "Rose Quartz",
//                 Url = "https://example.com/ahri-rose-quartz.jpg",
//                 Price = "290",
//                 Skin = ahriSkin
//             };
//
//             var ahriChroma2 = new Chroma
//             {
//                 Id = 2,
//                 Name = "Aquamarine",
//                 Url = "https://example.com/ahri-aquamarine.jpg",
//                 Price = "290",
//                 Skin = ahriSkin
//             };
//
//             // Create chromas for Kayn
//             var kaynChroma1 = new Chroma
//             {
//                 Id = 3,
//                 Name = "Ruby",
//                 Url = "https://example.com/kayn-ruby.jpg",
//                 Price = "290",
//                 Skin = kaynSkin
//             };
//
//             var kaynChroma2 = new Chroma
//             {
//                 Id = 4,
//                 Name = "Sapphire",
//                 Url = "https://example.com/kayn-sapphire.jpg",
//                 Price = "290",
//                 Skin = kaynSkin
//             };
//
//             var kaynChroma3 = new Chroma
//             {
//                 Id = 5,
//                 Name = "Obsidian",
//                 Url = "https://example.com/kayn-obsidian.jpg",
//                 Price = "290",
//                 Skin = kaynSkin
//             };
//
//             // Add chromas to their respective skins
//             ahriSkin.ChromaList.Add(ahriChroma1);
//             ahriSkin.ChromaList.Add(ahriChroma2);
//
//             kaynSkin.ChromaList.Add(kaynChroma1);
//             kaynSkin.ChromaList.Add(kaynChroma2);
//             kaynSkin.ChromaList.Add(kaynChroma3);
//
//             // Add skins to the context
//             context.Skins.Add(ahriSkin);
//             context.Skins.Add(kaynSkin);
//
//             chromaList = new List<Chroma>
//             {
//                 ahriChroma1,
//                 ahriChroma2,
//                 kaynChroma1,
//                 kaynChroma2,
//                 kaynChroma3
//             };
//
//             skinList = new List<Skin>
//             {
//                 ahriSkin,
//                 kaynSkin
//             };
//
//             context.SaveChanges();
//         }
//
//         private StoreDataContext CreateContext() => new StoreDataContext(_contextOptions);
//
//         public void Dispose()
//         {
//             _connection.Dispose();
//         }
//
//         [Fact]
//         public async Task GetAllAsync_ShouldReturnAllSkins()
//         {
//             // Arrange
//             using var context = CreateContext();
//             var repository = new SkinRepository(context);
//
//             // Act
//             var skins = await repository.GetAllAsync();
//
//             // Assert
//             Assert.Equal(2, skins.Count());
//             Assert.All(skins, skin => Assert.NotNull(skin.ChromaList));
//             Assert.Contains(skins, s => s.ChampionName == "Ahri");
//             Assert.Contains(skins, s => s.ChampionName == "Kayn");
//         }
//
//         [Fact]
//         public async Task GetByIdAsync_WithValidId_ShouldReturnSkin()
//         {
//             // Arrange
//             using var context = CreateContext();
//             var repository = new SkinRepository(context);
//             int skinId = 1;
//
//             // Act
//             var skin = await repository.GetByIdAsync(skinId);
//
//             // Assert
//             Assert.NotNull(skin);
//             Assert.Equal("Spirit Blossom Ahri", skin.Name);
//             Assert.Equal("Ahri", skin.ChampionName);
//             Assert.Equal(2, skin.ChromaList.Count);
//         }
//
//         [Fact]
//         public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
//         {
//             // Arrange
//             using var context = CreateContext();
//             var repository = new SkinRepository(context);
//             int invalidSkinId = 999;
//
//             // Act
//             var skin = await repository.GetByIdAsync(invalidSkinId);
//
//             // Assert
//             Assert.Null(skin);
//         }
//
//         [Fact]
//         public async Task AddAsync_ShouldAddNewSkin()
//         {
//             // Arrange
//             using var context = CreateContext();
//             var repository = new SkinRepository(context);
//             var newSkin = new Skin
//             {
//                 Name = "Star Guardian Jinx",
//                 ChampionName = "Jinx",
//                 SplashUrl = "https://example.com/star-guardian-jinx.jpg",
//                 CinematicSplashUrl = "https://example.com/star-guardian-jinx-cinematic.jpg",
//                 Price = 1350.0,
//                 Quote = "Let's light up the sky!",
//                 ChromaList = new List<Chroma>()
//             };
//
//             // Act
//             await repository.AddAsync(newSkin);
//
//             // Assert
//             var skins = await context.Skins.ToListAsync();
//             Assert.Equal(3, skins.Count);
//             Assert.Contains(skins, s => s.Name == "Star Guardian Jinx");
//         }
//
//         [Fact]
//         public async Task UpdateAsync_ShouldUpdateExistingSkin()
//         {
//             // Arrange
//             using var context = CreateContext();
//             var repository = new SkinRepository(context);
//
//             // First, get the skin to update
//             var skinToUpdate = await context.Skins.FirstAsync(s => s.Id == 1);
//
//             // Update its properties
//             skinToUpdate.Name = "Updated Ahri Skin";
//             skinToUpdate.Price = 1450.0;
//
//             // Act
//             await repository.UpdateAsync(skinToUpdate);
//
//             // Assert
//             var updatedSkin = await context.Skins.FindAsync(1);
//             Assert.Equal("Updated Ahri Skin", updatedSkin.Name);
//             Assert.Equal(1450.0, updatedSkin.Price);
//         }
//
//         [Fact]
//         public async Task DeleteAsync_WithValidId_ShouldRemoveSkin()
//         {
//             // Arrange
//             using var context = CreateContext();
//             var repository = new SkinRepository(context);
//             int skinIdToDelete = 2;
//
//             // Act
//             await repository.DeleteAsync(skinIdToDelete);
//
//             // Assert
//             var remainingSkins = await context.Skins.ToListAsync();
//             Assert.Single(remainingSkins);
//             Assert.DoesNotContain(remainingSkins, s => s.Id == skinIdToDelete);
//         }
//
//         [Fact]
//         public async Task DeleteAsync_WithInvalidId_ShouldNotThrowException()
//         {
//             // Arrange
//             using var context = CreateContext();
//             var repository = new SkinRepository(context);
//             int invalidSkinId = 999;
//
//             // Act & Assert
//             await repository.DeleteAsync(invalidSkinId); // Should not throw
//             var skins = await context.Skins.ToListAsync();
//             Assert.Equal(2, skins.Count); // Should still have 2 skins
//         }
//
//         [Fact]
//         public async Task GetSortedSkinsAsync_ByChampionName_ShouldReturnSortedList()
//         {
//             // Arrange
//             using var context = CreateContext();
//             var repository = new SkinRepository(context);
//
//             // Act
//             var sortedSkins = await repository.GetSortedSkinsAsync("ChampionName");
//
//             // Assert
//             var skinsArray = sortedSkins.ToArray();
//             Assert.Equal(2, skinsArray.Length);
//             Assert.Equal("Ahri", skinsArray[0].ChampionName); // Ahri comes before Kayn alphabetically
//             Assert.Equal("Kayn", skinsArray[1].ChampionName);
//         }
//
//         [Fact]
//         public async Task GetSortedSkinsAsync_ByPrice_ShouldReturnSortedList()
//         {
//             // Arrange
//             using var context = CreateContext();
//             var repository = new SkinRepository(context);
//
//             // Act
//             var sortedSkins = await repository.GetSortedSkinsAsync("Price");
//
//             // Assert
//             var skinsArray = sortedSkins.ToArray();
//             Assert.Equal(2, skinsArray.Length);
//             Assert.Equal(1350.0, skinsArray[0].Price); // Lower price comes first
//             Assert.Equal(1820.0, skinsArray[1].Price);
//         }
//
//         [Fact]
//         public async Task GetSortedSkinsAsync_ByName_ShouldReturnSortedList()
//         {
//             // Arrange
//             using var context = CreateContext();
//             var repository = new SkinRepository(context);
//
//             // Act
//             var sortedSkins = await repository.GetSortedSkinsAsync("Name");
//
//             // Assert
//             var skinsArray = sortedSkins.ToArray();
//             Assert.Equal(2, skinsArray.Length);
//             Assert.Equal("Odyssey Kayn", skinsArray[0].Name); // Odyssey comes before Spirit alphabetically
//             Assert.Equal("Spirit Blossom Ahri", skinsArray[1].Name);
//         }
//
//         [Fact]
//         public async Task GetSortedSkinsAsync_WithInvalidSortParameter_ShouldDefaultToChampionName()
//         {
//             // Arrange
//             using var context = CreateContext();
//             var repository = new SkinRepository(context);
//
//             // Act
//             var sortedSkins = await repository.GetSortedSkinsAsync("InvalidParameter");
//
//             // Assert
//             var skinsArray = sortedSkins.ToArray();
//             Assert.Equal(2, skinsArray.Length);
//             Assert.Equal("Ahri", skinsArray[0].ChampionName); // Default sort is by ChampionName
//             Assert.Equal("Kayn", skinsArray[1].ChampionName);
//         }
//     }
// }