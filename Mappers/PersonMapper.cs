using ecommerceAPI.DTO;
using ecommerceAPI.Models;

namespace ecommerceAPI.Mappers
{
    public static class PersonMapper
    {
        public static PersonDto ToDto(this Person person)
        {
            return new PersonDto()
            {
                Id = person.Id,
                Name = person.Name,
                LastName = person.LastName,
                Email = person.Email,
                Address = person is User user ? user.Address : null,
            };
        }
    }
}
