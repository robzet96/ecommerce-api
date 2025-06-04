using ecommerceAPI.Enums;

namespace ecommerceAPI.Models
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
    public class User : Person
    {
        public string Address { get; set; }
        public List<Order> Orders { get; set; } = new();
    }
    public class Admin : Person { }
}
