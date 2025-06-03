using ecommerceAPI.Models;

namespace ecommerceAPI.Repositories
{
    public interface IProductRepository : ICrudRepository<Product>
    {
        Task<IEnumerable<Product>> SearchByNameAsync(string name);
        Task<IEnumerable<Product>> GetAvailableProductsAsync();
        Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    }
}
