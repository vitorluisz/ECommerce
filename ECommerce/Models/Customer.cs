﻿using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class Customer
    {
        public string? Id { get; set; }
        public string ?Email { get; set; }
        public string ?Password { get; set; }
        public string ?FirstName { get; set; }
        public string ?LastName { get; set; }
        public Boolean IsAdmin { get; set; }
    }
}