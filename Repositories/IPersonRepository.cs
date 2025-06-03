using ecommerceAPI.Models;

namespace ecommerceAPI.Repositories
{
    public interface IPersonRepository : ICrudRepository<Person>
    {
        Task<IEnumerable<Person>> SearchByNameAsync(string Name);
        Task<IEnumerable<Person>> SearchByLastnameAsync(string Lastname);
        Task<IEnumerable<Person>> SearchByEmailAsync(string Email);
        Task<IEnumerable<Person>> SearchByNameAndLastnameAsync(string Name, string Lastname);
    }
}
