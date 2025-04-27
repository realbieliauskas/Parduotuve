using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Bunit;
using Bunit.TestDoubles;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using MudBlazor.Services;
using Parduotuve.Components.Pages.Checkout;
using Parduotuve.Data.Entities;
using Parduotuve.Data.Repositories;
using Parduotuve.Services;
using Stripe.Checkout;
using Xunit;

namespace Parduotuve.Tests.Pages;

[TestSubject(typeof(Cart))]
public class CartPageTests : TestContext
    {
        private readonly Mock<IOrderRepository> _orderRepoMock;
        private readonly Mock<ISkinRepository> _skinRepoMock;
        private readonly ShoppingCartService _shoppingCart;
        private readonly NavigationManager _navManager;
        private readonly Mock<IStripeService> _stripeServiceMock;

        public CartPageTests()
        {
            _orderRepoMock = new Mock<IOrderRepository>();
            _skinRepoMock = new Mock<ISkinRepository>();
            _stripeServiceMock = new Mock<IStripeService>();

            _shoppingCart = new ShoppingCartService();
            
            Mock<IUserRepository> userRepoMock = new();

            ProtectedLocalStorage storage = new(new Mock<IJSRuntime>().Object, new Mock<IDataProtectionProvider>().Object);
            AuthService authService = new(userRepoMock.Object, storage);
            
            Services.AddSingleton(_orderRepoMock.Object);
            Services.AddSingleton(_skinRepoMock.Object);
            Services.AddSingleton(authService);
            Services.AddSingleton(_shoppingCart);
            Services.AddSingleton(_stripeServiceMock.Object);
            Services.AddMudServices();

            Services.AddScoped<NavigationManager, FakeNavigationManager>();

            _navManager = Services.GetRequiredService<NavigationManager>();

            _stripeServiceMock.Setup(x => x.CreateCheckoutSessionAsync(It.IsAny<IEnumerable<SessionLineItemOptions>>(),
                It.IsAny<Dictionary<string, string>>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("http://localhost");
        }

        [Fact]
        public void When_Cart_Is_Empty_Should_Show_EmptyCartMessage()
        {
            // Arrange
            _skinRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Skin>());

            // Act
            IRenderedComponent<Cart> cut = RenderComponent<Cart>();

            // Assert
            Assert.Contains("Your shopping cart is empty", cut.Markup);
        }

        [Fact]
        public void When_Cart_Has_Items_Should_Display_Items()
        {
            // Arrange
            _shoppingCart.Add(1);
            _skinRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Skin>
            {
                new() { Id = 1, Name = "Skin", Price = 9.99 }
            });

            // Act
            IRenderedComponent<Cart> cut = RenderComponent<Cart>();

            // Assert
            Assert.Contains("Skin", cut.Markup);
            Assert.Contains("9.99 €", cut.Markup);
        }

        [Theory]
        [InlineData(new []{3.7,44.3,73.35})]
        [InlineData(new [] {1.0,1.99,928.10,10,5})]
        [InlineData(new [] {6,15,8.10,13,5,1436,12,6,232,765.3})]
        public void When_Cart_Has_Several_Total_Should_Be_Equal_To_Sum(double[] skinPrices)
        {
            // Arrange
            List<Skin> skins = new();
            for(int x = 0; x < skinPrices.Length; x++)
            {
                skins.Add(new() {Id = x+1, Name = "Skin"+(x+1), Price = skinPrices[x]});
                _shoppingCart.Add(x+1);
            }
            _skinRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(skins);

            double total = Math.Round(skins.Select(skin => skin.Price).Sum() ?? 0,2);
            
            // Act
            IRenderedComponent<Cart> cut = RenderComponent<Cart>();

            // Assert
            Assert.Contains(total+" €", cut.Markup);
        }
        
        [Fact]
        public async Task IncreaseQuantity_Button_Should_Increase_Item_Quantity()
        {
            // Arrange
            _shoppingCart.Add(1);
            _skinRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Skin>
            {
                new() { Id = 1, Name = "Cool Skin", Price = 9.99 }
            });

            IRenderedComponent<Cart> cut = RenderComponent<Cart>();

            // Act
            IElement increaseButton = cut.Find("button:contains('+')");
            await cut.InvokeAsync(() => increaseButton.Click());

            // Assert
            Assert.Contains(_shoppingCart, item => item.Key == 1 && item.Value == 2);
        }

        [Fact]
        public async Task DecreaseQuantity_Button_Should_Decrease_Item_Quantity()
        {
            // Arrange
            _shoppingCart.Add(1);
            _shoppingCart.Add(1);
            _skinRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Skin>
            {
                new() { Id = 1, Name = "Skin", Price = 9.99 }
            });

            IRenderedComponent<Cart> cut = RenderComponent<Cart>();

            // Act
            IElement decreaseButton = cut.Find("button:contains('-')");
            await cut.InvokeAsync(() => decreaseButton.Click());

            // Assert
            Assert.Contains(_shoppingCart, item => item.Key == 1 && item.Value == 1);
        }

        [Fact]
        public async Task HandleCheckout_Should_ClearCartAndNavigate()
        {
            // Arrange
            _shoppingCart.Add(1);
            _skinRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Skin>
            {
                new() { Id = 1, Name = "Skin", Price = 9.99 }
            });
            _skinRepoMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(new Skin
            {
                Id = 1,
                Name = "Skin",
                Price = 9.99
            });

            IRenderedComponent<Cart> cut = RenderComponent<Cart>();

            IElement checkoutButton = cut.Find(".mud-button-filled-primary");
            
            // Act
            await cut.InvokeAsync(() => checkoutButton.Click());
            // Assert
            Assert.True(_shoppingCart.IsEmpty());
            Assert.DoesNotContain(_navManager.Uri, "Cart");
        }
    }