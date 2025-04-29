using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Parduotuve.Data;
using Parduotuve.Data.Entities;
using Parduotuve.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Parduotuve.Tests.Repositories
{
    public class OrderRepoTests : IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<StoreDataContext> _contextOptions;

        public OrderRepoTests()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _contextOptions = new DbContextOptionsBuilder<StoreDataContext>()
                .UseSqlite(_connection)
                .Options;

            using var context = new StoreDataContext(_contextOptions);
            context.Database.EnsureCreated();
        }

        private StoreDataContext CreateContext() => new StoreDataContext(_contextOptions);

        public void Dispose()
        {
            _connection.Dispose();
        }

        [Fact]
        public async Task AddOrder_ShouldAddNewOrder()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new OrderRepository(context);
            var order = new Order { Id = "order1", IsCompleted = false };

            // Act
            await repository.AddOrder(order);

            // Assert
            var savedOrder = await repository.GetOrderById("order1");
            Assert.NotNull(savedOrder);
            Assert.Equal("order1", savedOrder.Id);
            Assert.False(savedOrder.IsCompleted);
        }

        [Fact]
        public async Task GetOrderById_ShouldReturnOrder()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new OrderRepository(context);
            var order = new Order { Id = "order1", IsCompleted = false };
            await repository.AddOrder(order);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetOrderById("order1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("order1", result.Id);
        }

        [Fact]
        public async Task GetOrderById_ShouldReturnNullForNonExistentOrder()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new OrderRepository(context);

            // Act
            var result = await repository.GetOrderById("nonexistent");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllOrders_ShouldReturnAllOrders()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new OrderRepository(context);
            await context.Orders.AddRangeAsync(
                new Order { Id = "order1", IsCompleted = false },
                new Order { Id = "order2", IsCompleted = true }
            );
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetAllOrders();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, o => o.Id == "order1");
            Assert.Contains(result, o => o.Id == "order2");
        }

        [Fact]
        public async Task DeleteOrder_ShouldRemoveOrder()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new OrderRepository(context);
            var order = new Order { Id = "order1", IsCompleted = false };
            await repository.AddOrder(order);
            await context.SaveChangesAsync();

            // Act
            await repository.DeleteOrder("order1");

            // Assert
            Assert.Null(await context.Orders.FindAsync("order1"));
        }

        [Fact]
        public async Task DeleteOrder_ShouldNotThrowForNonExistentOrder()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new OrderRepository(context);

            // Act & Assert
            await repository.DeleteOrder("nonexistent");
        }

        [Fact]
        public async Task UpdateOrder_ShouldModifyExistingOrder()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new OrderRepository(context);
            var order = new Order { Id = "order1", IsCompleted = false };
            await repository.AddOrder(order);
            await context.SaveChangesAsync();

            // Act
            order.IsCompleted = true;
            await repository.UpdateOrder(order);

            // Assert
            var updatedOrder = await context.Orders.FindAsync("order1");
            Assert.True(updatedOrder.IsCompleted);
        }

        [Fact]
        public async Task AddOrderItem_ShouldAddNewItemWithAutoIncrementedId()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new OrderRepository(context);
            var order = new Order { Id = "order1" };
            var skin = new Skin { Id = 1, Name = "Test Skin" };
            await repository.AddOrder(order);
            await context.Skins.AddAsync(skin);
            await context.SaveChangesAsync();

            var item1 = new OrderItem { OrderId = "order1", Skin = skin, Amount = 1 };
            var item2 = new OrderItem { OrderId = "order1", Skin = skin, Amount = 2 };

            // Act
            await repository.AddOrderItem(item1);
            await repository.AddOrderItem(item2);

            // Assert
            var items = await repository.GetAllOrderItems();
            Assert.Equal(2, items.Count());
            Assert.Equal(1, items.First().Id);
            Assert.Equal(2, items.Last().Id);
        }

        [Fact]
        public async Task GetOrderItemsByOrderId_ShouldReturnCorrectItems()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new OrderRepository(context);
            var order1 = new Order { Id = "order1" };
            var order2 = new Order { Id = "order2" };
            var skin = new Skin { Id = 1, Name = "Test Skin" };
            await context.Orders.AddRangeAsync(order1, order2);
            await context.Skins.AddAsync(skin);
            await context.SaveChangesAsync();

            await repository.AddOrderItem(new OrderItem { OrderId = "order1", Skin = skin, Amount = 1 });
            await repository.AddOrderItem(new OrderItem { OrderId = "order1", Skin = skin, Amount = 2 });
            await repository.AddOrderItem(new OrderItem { OrderId = "order2", Skin = skin, Amount = 3 });

            // Act
            var result = await repository.GetOrderItemsByOrderId("order1");

            // Assert
            Assert.Equal(2, result.Count());
            Assert.All(result, item => Assert.Equal("order1", item.OrderId));
        }

        [Fact]
        public async Task GetOrderItemById_ShouldReturnCorrectItem()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new OrderRepository(context);
            var order = new Order { Id = "order1" };
            var skin = new Skin { Id = 1, Name = "Test Skin" };
            await repository.AddOrder(order);
            await context.Skins.AddAsync(skin);
            await context.SaveChangesAsync();

            var item = new OrderItem { OrderId = "order1", Skin = skin, Amount = 1 };
            await repository.AddOrderItem(item);

            // Act
            var result = await repository.GetOrderItemById(item.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(item.Id, result.Id);
            Assert.Equal("order1", result.OrderId);
            Assert.Equal(1, result.Amount);
        }

        [Fact]
        public async Task DeleteOrderItem_ShouldRemoveItem()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new OrderRepository(context);
            var order = new Order { Id = "order1" };
            var skin = new Skin { Id = 1, Name = "Test Skin" };
            await repository.AddOrder(order);
            await context.Skins.AddAsync(skin);
            await context.SaveChangesAsync();

            var item = new OrderItem { OrderId = "order1", Skin = skin, Amount = 1 };
            await repository.AddOrderItem(item);

            // Act
            await repository.DeleteOrderItem(item.Id);

            // Assert
            Assert.Null(await context.OrderItems.FindAsync(item.Id));
        }

        [Fact]
        public async Task UpdateOrderItem_ShouldModifyExistingItem()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new OrderRepository(context);
            var order = new Order { Id = "order1" };
            var skin = new Skin { Id = 1, Name = "Test Skin" };
            await repository.AddOrder(order);
            await context.Skins.AddAsync(skin);
            await context.SaveChangesAsync();

            var item = new OrderItem { OrderId = "order1", Skin = skin, Amount = 1 };
            await repository.AddOrderItem(item);

            // Act
            item.Amount = 5;
            await repository.UpdateOrderItem(item);

            // Assert
            var updatedItem = await context.OrderItems.FindAsync(item.Id);
            Assert.Equal(5, updatedItem.Amount);
        }
    }
}