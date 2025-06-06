﻿using ecommerceAPI.Enums;

namespace ecommerceAPI.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public List<ProductWithQuantityDto> Products { get; set; } = new();
    }
}
