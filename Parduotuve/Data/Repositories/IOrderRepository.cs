using Parduotuve.Data.Entities;

namespace Parduotuve.Data.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<IEnumerable<OrderItem>> GetAllOrderItems();
        Task<Order?> GetOrderById(string id);
        Task<OrderItem?> GetOrderItemById(int id);
        Task<IEnumerable<OrderItem?>> GetOrderItemsByOrderId(string id);
        Task AddOrder(Order order);
        Task AddOrderItem(OrderItem item);
        Task UpdateOrder(Order order);
        Task UpdateOrderItem(OrderItem order);
        Task DeleteOrder(string id);
        Task DeleteOrderItem(int id);
    }
}
