using ecommerceAPI.Models;

namespace ecommerceAPI.Repositories
{
    public interface IOrderRepository : ICrudRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByDateAsync(DateTime date);
        Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Order>> GetOrdersContainingProductAsync(int productId);
        Task<IEnumerable<Order>> GetOrdersContainingProductByNameAsync(string productName);
    }
}
