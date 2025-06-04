using ecommerceAPI.DTO;
using ecommerceAPI.Models;

namespace ecommerceAPI.Services
{
    public interface IPersonService : ICrudService<PersonDto, CreatePersonDto, UpdatePersonDto>
    {
        Task<string?> LoginAsync(LoginRequestDto dto);
        Task<PersonDto> RegisterAsync(CreatePersonDto dto);
        Task<PersonDto?> UpdateOwnDataAsync(int Id,UpdatePersonDto dto);
        Task<IEnumerable<PersonDto>> SearchByNameAsync(string name);
        Task<IEnumerable<PersonDto>> SearchByLastnameAsync(string lastname);
        Task<IEnumerable<PersonDto>> SearchByEmailAsync(string email);
        Task<IEnumerable<PersonDto>> SearchByNameAndLastnameAsync(string name, string lastname);
    }
}
