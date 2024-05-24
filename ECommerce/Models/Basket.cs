using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class Basket
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public Product Product { get; set; }
    }
}
