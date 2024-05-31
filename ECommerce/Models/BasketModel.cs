using ECommerce.Controllers;
using ECommerce.Data;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace ECommerce.Models
{
    public class BasketModel
    {
        readonly private ApplicationDbContext _db;

        public BasketModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Product> LoadBasket()
        {
            var productIds = _db.Basket.Select(b => b.ProductId).Distinct().ToList();
            var baskets = new List<Basket>();

            foreach (var productId in productIds)
            {
                var product = _db.Basket
                    .Where(b => b.ProductId == productId)
                    .Include(b => b.Product).ToList()
                    .FirstOrDefault();

                if (product != null)
                {
                    baskets.Add(product);
                }
            }

            List<Product> products = new List<Product>();

            foreach (var basket in baskets)
            {
                Product product = _db.Produtos.SingleOrDefault(p => p.Id == basket.ProductId);
                product.Quantity = _db.Basket.Where(b => b.ProductId == product.Id).Sum(b => b.Quantity);
                products.Add(product);
            }

            return products;
        }

        public void AddToBasket(int productId, int qnt, Product product)
        {
            _db.Basket.Add(new Basket
            {
                ProductId = product.Id,
                Quantity = qnt
            });

            _db.SaveChanges();
        }

        public void DeleteBasket (List<Basket> basket)
        {
            _db.RemoveRange(basket);
            _db.SaveChanges();
        }
    }
}
