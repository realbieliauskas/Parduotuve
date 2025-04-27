using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Parduotuve.Services;
using Xunit;

namespace Parduotuve.Tests.Services;

[TestSubject(typeof(ShoppingCartService))]
public class ShoppingCartServiceTests
{
    [Fact]
    public void Constructor_ShouldInitializeEmptyCart()
    {
        ShoppingCartService cart = new ShoppingCartService();
        Assert.True(cart.IsEmpty());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(99)]
    public void Add_NewItem_ShouldAddWithQuantityOne(int id)
    {
        ShoppingCartService cart = new ShoppingCartService();
        cart.Add(id);

        Assert.Contains(cart, item => item.Key == id && item.Value == 1);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(10)]
    [InlineData(50)]
    public void Add_ExistingItem_ShouldIncrementQuantity(int id)
    {
        ShoppingCartService cart = new ShoppingCartService();
        cart.Add(id);
        cart.Add(id);

        Assert.Contains(cart, item => item.Key == id && item.Value == 2);
    }

    [Fact]
    public void Clear_ShouldEmptyCart()
    {
        ShoppingCartService cart = new ShoppingCartService();
        cart.Add(1);
        cart.Add(2);

        cart.Clear();

        Assert.True(cart.IsEmpty());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(7)]
    public void Remove_ExistingItem_ShouldRemoveItem(int id)
    {
        ShoppingCartService cart = new ShoppingCartService();
        cart.Add(id);

        cart.Remove(id);

        Assert.False(cart.Any());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    public void Decrease_ItemQuantityGreaterThanOne_ShouldDecreaseQuantity(int id)
    {
        ShoppingCartService cart = new ShoppingCartService();
        cart.Add(id);
        cart.Add(id); // Quantity now 2

        cart.Decrease(id);

        Assert.Contains(cart, item => item.Key == id && item.Value == 1);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(8)]
    public void Decrease_ItemQuantityEqualToOne_ShouldRemoveItem(int id)
    {
        ShoppingCartService cart = new ShoppingCartService();
        cart.Add(id); // Quantity 1

        cart.Decrease(id);

        Assert.False(cart.Any());
    }

    [Theory]
    [InlineData(3)]
    [InlineData(6)]
    public void Increase_ExistingItem_ShouldIncreaseQuantity(int id)
    {
        ShoppingCartService cart = new ShoppingCartService();
        cart.Add(id);

        cart.Increase(id);

        Assert.Contains(cart, item => item.Key == id && item.Value == 2);
    }

    [Fact]
    public void IsEmpty_WhenCartIsEmpty_ShouldReturnTrue()
    {
        ShoppingCartService cart = new ShoppingCartService();
        Assert.True(cart.IsEmpty());
    }

    [Fact]
    public void IsEmpty_WhenCartHasItems_ShouldReturnFalse()
    {
        ShoppingCartService cart = new ShoppingCartService();
        cart.Add(1);

        Assert.False(cart.IsEmpty());
    }

    [Fact]
    public void GetEnumerator_ShouldEnumerateAllItems()
    {
        ShoppingCartService cart = new ShoppingCartService();
        cart.Add(1);
        cart.Add(2);

        List<KeyValuePair<int, int>> items = new List<KeyValuePair<int, int>>();
        foreach (KeyValuePair<int, int> item in cart)
        {
            items.Add(item);
        }

        Assert.Equal(2, items.Count);
        Assert.Contains(items, i => i.Key == 1);
        Assert.Contains(items, i => i.Key == 2);
    }
}