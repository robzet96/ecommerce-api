using ecommerceAPI.Enums;

namespace ecommerceAPI.DTO
{
    public class UpdateOrderDto
    {
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public List<ProductWithQuantityDto> Products { get; set; } = new();
    }
}
