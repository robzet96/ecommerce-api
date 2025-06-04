using ecommerceAPI.DTO;
using ecommerceAPI.Enums;
using ecommerceAPI.Models;

namespace ecommerceAPI.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToDto(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                Status = order.Status,
                Products = order.OrderProducts.Select(op => new ProductWithQuantityDto
                {
                    ProductId = op.ProductId,
                    ProductName = op.Product.Name,
                    Quantity = op.Quantity
                }).ToList()
            };
        }
        public static Order ToEntity(this CreateOrderDto dto)
        {
            return new Order
            {
                UserId = dto.UserId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                OrderProducts = dto.Products.Select(p => new OrderProduct
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                }).ToList()
            };
        }
    }
}
