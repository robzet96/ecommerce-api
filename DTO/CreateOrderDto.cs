namespace ecommerceAPI.DTO
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public List<ProductWithQuantityDto> Products { get; set; } = new();
    }
}
