using Microsoft.EntityFrameworkCore;
using Parduotuve.Data.Entities;

namespace Parduotuve.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly StoreDataContext _context;

    public OrderRepository(StoreDataContext context)
    {
        _context = context;
    }

    public async Task AddOrder(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task AddOrderItem(OrderItem item)
    {
        IEnumerable<OrderItem>? allItems = await GetAllOrderItems();
        int newId = 0;
        if (allItems.Count() != 0) newId = allItems.Select(i => i.Id).Max() + 1;
        item.Id = newId;
        await _context.OrderItems.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrder(string id)
    {
        Order? order = await _context.Orders.FindAsync(id);
        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteOrderItem(int id)
    {
        OrderItem? item = await _context.OrderItems.FindAsync(id);
        if (item != null)
        {
            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<OrderItem>> GetAllOrderItems()
    {
        return await _context.OrderItems.Include(item => item.Skin).ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<Order?> GetOrderById(string id)
    {
        return await _context.Orders.Include(order => order.User).Where(order => order.Id.Equals(id))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<OrderItem?>> GetOrderItemsByOrderId(string id)
    {
        return await _context.OrderItems.Include(item => item.Skin).Where(a => a.OrderId.Equals(id)).ToListAsync();
    }

    public async Task<OrderItem?> GetOrderItemById(int id)
    {
        return await _context.OrderItems.Include(item => item.Skin).Where(item => item.Id == id).FirstAsync();
    }

    public async Task UpdateOrder(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateOrderItem(OrderItem item)
    {
        _context.OrderItems.Update(item);
        await _context.SaveChangesAsync();
    }
}