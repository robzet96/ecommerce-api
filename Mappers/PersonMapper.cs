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
        public static Person ToPerson(CreatePersonDto dto)
        {
            if (dto.Address != null)
            {
                return new User
                {
                    Name = dto.Name,
                    LastName = dto.Lastname,
                    Email = dto.Email,
                    Address = dto.Address,
                    Password = dto.Password,
                };
            }
            else
            {
                return new Admin
                {
                    Name = dto.Name,
                    LastName = dto.Lastname,
                    Email = dto.Email,
                    Password = dto.Password,
                };
            }
        }
        public static void UpdatePersonFromDto(UpdatePersonDto dto, User person)
        {
            if (dto.Name is not null) person.Name = dto.Name;
            if (dto.Lastname is not null) person.LastName = dto.Lastname;
            if (dto.Address is not null) person.Address = dto.Address;
            if (dto.Email is not null) person.Email = dto.Email;
            if (dto.Password is not null) person.Password = dto.Password;
        }
    }
}
