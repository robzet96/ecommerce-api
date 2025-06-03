namespace ecommerceAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsFragile { get; set; }
        public bool IsAvailable { get; set; }
        public List<OrderProduct> OrderProducts { get; set; } = new();
    }
}
