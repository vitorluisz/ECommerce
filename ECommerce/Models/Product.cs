﻿using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ?Name { get; set; }
        public string ?Description { get; set; }
        public decimal Price { get; set; }
        public string ?ImageUrl { get; set; }
        public int Quantity { get; set; }
        public string ?Category { get; set; }
    }
}
