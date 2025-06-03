﻿namespace ecommerceAPI.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsFragile { get; set; }
        public bool IsAvailable { get; set; }
    }
}
