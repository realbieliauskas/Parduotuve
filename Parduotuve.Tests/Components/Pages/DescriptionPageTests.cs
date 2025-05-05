using System.Collections.Generic;
using System.Threading.Tasks;
using Parduotuve.Components.Pages;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using MudBlazor;
using Parduotuve.Data.Entities;
using Parduotuve.Data.Repositories;
using Parduotuve.Services;
using Xunit;

namespace Parduotuve.Tests.Pages;

public class DescriptionPageTests : TestContext
{
    private readonly Mock<ISkinRepository> _skinRepoMock = new();
    private readonly Mock<ISnackbar> _snackbarMock = new();

    public DescriptionPageTests()
    {
        Services.AddSingleton<ISkinRepository>(_skinRepoMock.Object);
        Services.AddSingleton<ShoppingCartService>(new ShoppingCartService());
        Services.AddSingleton<ISnackbar>(_snackbarMock.Object);
    }

    [Fact]
    public async Task Loads_Skin_Successfully()
    {
        // Arrange
        Skin skin = new Skin
        {
            Id = 1,
            Name = "Test Skin",
            CinematicSplashUrl = "https://example.com/test.jpg",
            Quote = "A sample quote",
            ChromaList = new List<Chroma>()
        };
        _skinRepoMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(skin);

        // Act
        IRenderedComponent<SkinDescriptionPage> cut = RenderComponent<SkinDescriptionPage>(parameters => parameters.Add(p => p.SkinId, "1"));

        // Assert
        Assert.Contains("Test Skin",cut.Markup);
        Assert.Contains("https://example.com/test.jpg",cut.Markup);
    }

    [Fact]
    public async Task Handles_InvalidSkinId_Gracefully()
    {
        // Arrange
        IRenderedComponent<SkinDescriptionPage> cut = RenderComponent<SkinDescriptionPage>(parameters => parameters.Add(p => p.SkinId, "abc")); // Non-int input

        // Assert (whether an exception was not thrown)
        Assert.True(true); 
    }

    [Fact]
    public async Task AddToCart_Adds_Item_And_Shows_Snackbar()
    {
        // Arrange
        Skin skin = new Skin { Id = 2, Name = "Cart Skin" };
        _skinRepoMock.Setup(x => x.GetByIdAsync(2)).ReturnsAsync(skin);

        ShoppingCartService cartService = Services.GetRequiredService<ShoppingCartService>();

        IRenderedComponent<SkinDescriptionPage> cut = RenderComponent<SkinDescriptionPage>(parameters => parameters.Add(p => p.SkinId, "2"));

        // Act
        await cut.Instance.AddToCart(2);

        // Assert
        Assert.Contains(new KeyValuePair<int, int>(2,1), cartService);
        _snackbarMock.Verify(x => x.Add("Cart Skin added to cart!", Severity.Success,null,null), Times.Once);
    }

    [Fact]
    public async Task Chromas_Are_Displayed_Correctly()
    {
        // Arrange
        Skin skin = new Skin
        {
            Id = 3,
            Name = "Chroma Skin",
            CinematicSplashUrl = "https://example.com/chroma.jpg",
            ChromaList = new List<Chroma>
            {
                new Chroma { Name = "Red Chroma", Url = "https://example.com/red.jpg", Price = "290" },
                new Chroma { Name = "Blue Chroma", Url = "https://example.com/blue.jpg", Price = "290" }
            }
        };

        _skinRepoMock.Setup(x => x.GetByIdAsync(3)).ReturnsAsync(skin);

        // Act
        IRenderedComponent<SkinDescriptionPage> cut = RenderComponent<SkinDescriptionPage>(parameters => parameters.Add(p => p.SkinId, "3"));
        
        // Assert
        Assert.Contains("Red Chroma",cut.Markup);
        Assert.Contains("Blue Chroma",cut.Markup);
        Assert.Contains("290RP",cut.Markup);
    }
}
