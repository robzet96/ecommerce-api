using ecommerceAPI.Enums;

namespace ecommerceAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsPaid { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderProduct> OrderProducts { get; set; } = new();
    }
}
