using ecommerceAPI.DTO;

namespace ecommerceAPI.Services
{
    public interface IOrderService : ICrudService<OrderDto, CreateOrderDto, UpdateOrderDto>
    {
        Task<OrderDto> CreateOrderAsync(CreateOrderDto dto);
        Task<OrderDto?> UpdateOrderAsync(int Id,UpdateOrderDto dto);

        Task<IEnumerable<OrderDto>> GetOrdersByDateAsync(DateTime date);
        Task<IEnumerable<OrderDto>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<OrderDto>> GetOrdersContainingProductAsync(int productId);
        Task<IEnumerable<OrderDto>> GetOrdersContainingProductByNameAsync(string productName);
        Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(int userId);
    }
}
