using ecommerceAPI.DTO;
using ecommerceAPI.Mappers;
using ecommerceAPI.Models;
using ecommerceAPI.Repositories;

namespace ecommerceAPI.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly JwtService _jwtService;
        public PersonService(IPersonRepository personRepository, JwtService jwtService)
        {
            _personRepository = personRepository;
            _jwtService = jwtService;
        }
        public async Task<PersonDto> AddAsync(CreatePersonDto dto)
        {
            if (dto.Address != null)
            {
                var user = new User
                {
                    Name = dto.Name,
                    LastName = dto.Lastname,
                    Email = dto.Email,
                    Password = _jwtService.HashPassword(dto.Password),
                    Address = dto.Address,
                    Role = dto.Role
                };
                await _personRepository.AddAsync(user);
                return PersonMapper.ToDto(user);
            }
            else
            {
                var admin = new Admin
                {
                    Name = dto.Name,
                    LastName = dto.Lastname,
                    Email = dto.Email,
                    Password = _jwtService.HashPassword(dto.Password),
                    Role = dto.Role
                };
                await _personRepository.AddAsync(admin);
                return PersonMapper.ToDto(admin);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null) return false;
            return await _personRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PersonDto>> GetAllAsync()
        {
            var people = await _personRepository.GetAllAsync();
            return people.Select(PersonMapper.ToDto);
        }

        public async Task<PersonDto?> GetByIdAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            return person == null ? null : PersonMapper.ToDto(person);
        }

        public async Task<string?> LoginAsync(LoginRequestDto dto)
        {
            var person = (await _personRepository.SearchByEmailAsync(dto.Email)).FirstOrDefault();
            if (person == null || !_jwtService.VerifyPassword(dto.Password, person.Password)) return null;

            return _jwtService.GenerateToken(person);
        }

        public async Task<PersonDto> RegisterAsync(CreatePersonDto dto)
        {
            var person = (await _personRepository.SearchByEmailAsync(dto.Email)).FirstOrDefault();
            if (person != null)
            {
                throw new Exception("Email already in use.");
            }

            var user = new User
            {
                Name = dto.Name,
                LastName = dto.Lastname,
                Email = dto.Email,
                Password = _jwtService.HashPassword(dto.Password),
                Address = dto.Address,
                Role = "User"
            };
            await _personRepository.AddAsync(user);
            return PersonMapper.ToDto(user);
        }

        public async Task<IEnumerable<PersonDto>> SearchByEmailAsync(string email)
        {
            var people = await _personRepository.SearchByEmailAsync(email);
            return people.Select(PersonMapper.ToDto);
        }

        public async Task<IEnumerable<PersonDto>> SearchByLastnameAsync(string lastname)
        {
            var people = await _personRepository.SearchByLastnameAsync(lastname);
            return people.Select(PersonMapper.ToDto);
        }

        public async Task<IEnumerable<PersonDto>> SearchByNameAndLastnameAsync(string name, string lastname)
        {
            var people = await _personRepository.SearchByNameAndLastnameAsync(name, lastname);
            return people.Select(PersonMapper.ToDto);
        }

        public async Task<IEnumerable<PersonDto>> SearchByNameAsync(string name)
        {
            var people = await _personRepository.SearchByNameAsync(name);
            return people.Select(PersonMapper.ToDto);
        }

        public async Task<PersonDto> UpdateAsync(int id, UpdatePersonDto dto)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null) return null;

            if (!string.IsNullOrWhiteSpace(dto.Name))
                person.Name = dto.Name;

            if (!string.IsNullOrWhiteSpace(dto.Lastname))
                person.LastName = dto.Lastname;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                person.Email = dto.Email;
            if (!string.IsNullOrWhiteSpace(dto.Role))
                person.Role = dto.Role;


            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                person.Password = _jwtService.HashPassword(dto.Password);
            }

            if (person is User user && !string.IsNullOrWhiteSpace(dto.Address))
            {
                user.Address = dto.Address;
            }
            await _personRepository.UpdateAsync(person);
            return PersonMapper.ToDto(person);
        }

        public async Task<PersonDto?> UpdateOwnDataAsync(int Id, UpdatePersonDto dto)
        {
            var person = await _personRepository.GetByIdAsync(Id);
            if (person == null) return null;

            if (!string.IsNullOrWhiteSpace(dto.Name))
                person.Name = dto.Name;

            if (!string.IsNullOrWhiteSpace(dto.Lastname))
                person.LastName = dto.Lastname;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                person.Email = dto.Email;

            if (!string.IsNullOrWhiteSpace(dto.Password))
                person.Password = _jwtService.HashPassword(dto.Password);

            if (person is User user && !string.IsNullOrWhiteSpace(dto.Address))
                user.Address = dto.Address;

            await _personRepository.UpdateAsync(person);
            return PersonMapper.ToDto(person);
        }
    }
}
