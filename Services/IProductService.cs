using ecommerceAPI.DTO;

namespace ecommerceAPI.Services
{
    public interface IProductService : ICrudService<ProductDto, CreateProductDto, UpdateProductDto>
    {
        Task<IEnumerable<ProductDto>> SearchByNameAsync(string name);
        Task<IEnumerable<ProductDto>> SearchByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<ProductDto>> GetAvailableProductsAsync();
    }
}
