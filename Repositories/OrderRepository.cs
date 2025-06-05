using ecommerceAPI.Data;
using ecommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerceAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbContext;
        public OrderRepository(AppDbContext context)
        {
            _dbContext = context;
        }
        public async Task<Order> AddAsync(Order entity)
        {
            _dbContext.Orders.Add(entity);
            await _dbContext.SaveChangesAsync();

            return await _dbContext.Orders
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .FirstAsync(o => o.Id == entity.Id);
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var order = await _dbContext.Orders.FindAsync(Id);
            if (order == null) return false;

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _dbContext.Orders
            .AsNoTracking()
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
            .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int Id)
        {
            return await _dbContext.Orders
            .AsNoTracking()
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .FirstOrDefaultAsync(o => o.Id == Id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByDateAsync(DateTime date)
        {
            return await _dbContext.Orders
            .AsNoTracking()
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
            .Where(o => o.OrderDate.Date == date.Date)
            .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbContext.Orders
            .AsNoTracking()
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
            .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
            .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersContainingProductAsync(int productId)
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .Where(o => o.OrderProducts.Any(op => op.ProductId == productId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersContainingProductByNameAsync(string productName)
        {
            return await _dbContext.Orders
            .AsNoTracking()
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .Where(o => o.OrderProducts.Any(op =>
                op.Product.Name.Contains(productName)))
            .ToListAsync();
        }

        public async Task<Order> UpdateAsync(Order entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _dbContext.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
            .ToListAsync();
        }
    }
}
