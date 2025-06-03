namespace ecommerceAPI.Repositories
{
    public interface ICrudRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int Id);
        Task<T> UpdateAsync(T entity);
        Task<T> AddAsync(T entity);
        Task<bool> DeleteAsync(int Id);
    }
}
