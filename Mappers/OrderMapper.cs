using ecommerceAPI.DTO;
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
                Status = order.Status.ToString(),
                Products = order.OrderProducts.Select(op => new ProductWithQuantityDto
                {
                    ProductId = op.ProductId,
                    ProductName = op.Product.Name,
                    Quantity = op.Quantity
                }).ToList()
            };
        }
    }
}
