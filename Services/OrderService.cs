using ecommerceAPI.DTO;
using ecommerceAPI.Mappers;
using ecommerceAPI.Models;
using ecommerceAPI.Repositories;

namespace ecommerceAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }
        public async Task<OrderDto> AddAsync(CreateOrderDto dto)
        {
            var order = new Order
            {
                UserId = dto.UserId,
                OrderDate = DateTime.UtcNow,
                Status = Enums.OrderStatus.Pending,
            };

            var result = await _orderRepository.AddAsync(order);
            return OrderMapper.ToDto(result);
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
        {
            var order = new Order
            {
                UserId = dto.UserId,
                OrderDate = DateTime.UtcNow,
                Status = Enums.OrderStatus.Pending,
                OrderProducts = new List<OrderProduct>()
            };

            foreach (var item in dto.Products)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                    throw new Exception($"Product with ID {item.ProductId} not found.");

                order.OrderProducts.Add(new OrderProduct
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity
                });
            }

            var created = await _orderRepository.AddAsync(order);
            return OrderMapper.ToDto(created);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _orderRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders.Select(OrderMapper.ToDto);
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return order == null ? null : OrderMapper.ToDto(order);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByDateAsync(DateTime date)
        {
            var orders = await _orderRepository.GetOrdersByDateAsync(date);
            return orders.Select(OrderMapper.ToDto);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var orders = await _orderRepository.GetOrdersByDateRangeAsync(startDate,endDate);
            return orders.Select(OrderMapper.ToDto);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersContainingProductAsync(int productId)
        {
            var orders = await _orderRepository.GetOrdersContainingProductAsync(productId);
            return orders.Select(OrderMapper.ToDto);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersContainingProductByNameAsync(string productName)
        {
            var orders = await _orderRepository.GetOrdersContainingProductByNameAsync(productName);
            return orders.Select(OrderMapper.ToDto);
        }

        public async Task<OrderDto> UpdateAsync(int id, UpdateOrderDto dto)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if(order == null)
            {
                throw new Exception("Order not found.");
            }

            order.Status = dto.Status;

            if (dto.Products != null && dto.Products.Any())
            {
                order.OrderProducts.Clear();

                foreach (var item in dto.Products)
                {
                    order.OrderProducts.Add(new OrderProduct
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    });
                }
            }
            var updated = await _orderRepository.UpdateAsync(order);
            return OrderMapper.ToDto(updated);
        }

        public async Task<OrderDto?> UpdateOrderAsync(int Id, UpdateOrderDto dto)
        {
            var order = await _orderRepository.GetByIdAsync(Id);
            if (order == null)
                return null;

            order.Status = dto.Status;

            if (dto.Products != null && dto.Products.Any())
            {
                order.OrderProducts.Clear();

                foreach (var item in dto.Products)
                {
                    order.OrderProducts.Add(new OrderProduct
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    });
                }
            }

            var updated = await _orderRepository.UpdateAsync(order);
            return OrderMapper.ToDto(updated);
        }
    }
}
