    using ecommerceAPI.Data;
    using ecommerceAPI.Models;
    using Microsoft.EntityFrameworkCore;

    namespace ecommerceAPI.Repositories
    {
        public class PersonRepository : IPersonRepository
        {
            private readonly AppDbContext _dbContext;
            public PersonRepository(AppDbContext context)
            {
                _dbContext = context;
            }
            public async Task<Person> AddAsync(Person entity)
            {
                _dbContext.People.Add(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }

            public async Task<bool> DeleteAsync(int Id)
            {
                var person = await _dbContext.People.FindAsync(Id);
                if (person == null)
                {
                    return false;
                }
                _dbContext.People.Remove(person);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            public async Task<IEnumerable<Person>> GetAllAsync()
            {
                return await _dbContext.People.AsNoTracking().ToListAsync();
            }

            public async Task<Person?> GetByIdAsync(int Id)
            {
                return await _dbContext.People.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
            }

            public async Task<IEnumerable<Person>> SearchByEmailAsync(string Email)
            {
                return await _dbContext.People.AsNoTracking().Where(p => p.Email == Email).ToListAsync();
            }

            public async Task<IEnumerable<Person>> SearchByLastnameAsync(string Lastname)
            {
                return await _dbContext.People.AsNoTracking().Where(p => p.LastName == Lastname).ToListAsync();
            }

            public async Task<IEnumerable<Person>> SearchByNameAndLastnameAsync(string Name, string Lastname)
            {
                return await _dbContext.People.AsNoTracking().Where(p => p.LastName == Lastname && p.Name == Name).ToListAsync();
            }

            public async Task<IEnumerable<Person>> SearchByNameAsync(string Name)
            {
                return await _dbContext.People.AsNoTracking().Where(p => p.Name == Name).ToListAsync();
            }

            public async Task<Person> UpdateAsync(Person entity)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return entity;
            }
        }
    }
