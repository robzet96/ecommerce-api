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
    }
}
