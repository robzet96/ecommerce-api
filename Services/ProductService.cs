using ecommerceAPI.DTO;
using ecommerceAPI.Mappers;
using ecommerceAPI.Models;
using ecommerceAPI.Repositories;

namespace ecommerceAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductDto> AddAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                IsFragile = dto.IsFragile,
                IsAvailable = dto.IsAvailable,
            };
            var created = await _productRepository.AddAsync(product);
            return ProductMapper.ToDto(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(ProductMapper.ToDto);
        }

        public async Task<IEnumerable<ProductDto>> GetAvailableProductsAsync()
        {
            var products = await _productRepository.GetAvailableProductsAsync();
            return products.Select(ProductMapper.ToDto);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product == null ? null : ProductMapper.ToDto(product);
        }

        public async Task<IEnumerable<ProductDto>> SearchByNameAsync(string name)
        {
            var products = await _productRepository.SearchByNameAsync(name);
            return products.Select(ProductMapper.ToDto);
        }

        public async Task<IEnumerable<ProductDto>> SearchByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var products = await _productRepository.GetProductsByPriceRangeAsync(minPrice, maxPrice);
            return products.Select(ProductMapper.ToDto);
        }

        public async Task<ProductDto> UpdateAsync(int id, UpdateProductDto dto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            if (!string.IsNullOrWhiteSpace(dto.Name))
                product.Name = dto.Name;

            if (!string.IsNullOrWhiteSpace(dto.Description))
                product.Description = dto.Description;

            if (dto.Price.HasValue)
                product.Price = dto.Price.Value;

            if (dto.IsFragile.HasValue)
                product.IsFragile = dto.IsFragile.Value;

            if (dto.IsAvailable.HasValue)
                product.IsAvailable = dto.IsAvailable.Value;

            var updated = await _productRepository.UpdateAsync(product);
            return ProductMapper.ToDto(updated);
        }
    }
}