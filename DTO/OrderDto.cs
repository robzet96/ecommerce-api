namespace ecommerceAPI.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<ProductWithQuantityDto> Products { get; set; } = new();
    }
}
