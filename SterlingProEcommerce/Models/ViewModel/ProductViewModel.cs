﻿namespace SterlingProEcommerce.Models.ViewModel
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;        
        public string ProductImageUrl { get; set; } = string.Empty;        
        public decimal Price { get; set; }
       
    }
}
