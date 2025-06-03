using ecommerceAPI.DTO;
using ecommerceAPI.Enums;
using ecommerceAPI.Models;

namespace ecommerceAPI.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsFragile = product.IsFragile,
                IsAvailable = product.IsAvailable
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
