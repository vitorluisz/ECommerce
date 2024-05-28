using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Produtos { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<Customer> Customer { get; set; }

    }
}
