using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ECommerce.Controllers
{
    public class BasketController : Controller
    {
        readonly private BasketModel bm;
        readonly private ApplicationDbContext _db;

        public BasketController(ApplicationDbContext db)
        {
            _db = db;
            bm = new BasketModel(db);
        }

         

        public IActionResult Basket()
        {
            using (var db = _db)
            {
                return View(bm.LoadBasket());
            }
        }

        public IActionResult AddBasket(int productId, Product productbasket)
        {
            int qnt = productbasket.Quantity;
            using (var db = _db)
            {
                // Obtenha o produto a partir do repositório usando o productId
                var product = _db.Produtos.Where(p => p.Id == productId).FirstOrDefault();

                if (product != null)
                {
                    bm.AddToBasket(productId, qnt, product);

                    return RedirectToAction("Basket");
                }
                else
                {
                    // Produto não encontrado
                    return NotFound();
                }
            }

        }

        public IActionResult DelBasket()
        {
            var basket = _db.Basket.ToList();

            if (basket.Any())
            {
                bm.DeleteBasket(basket);

                return RedirectToAction("Basket");
            }
            else
            {
                // Produto não encontrado
                return NotFound();
            }

        }

        public IActionResult DelProductBasket(int productId)
        {

            var basket = _db.Basket
                    .Where(b => b.ProductId == productId)
                    .Include(b => b.Product).ToList();

            if (basket.Any())
            {
                bm.DeleteBasket(basket);

                // Redirecione para a página do carrinho de compras
                return RedirectToAction("Basket");
            }
            else
            {
                // Produto não encontrado
                return NotFound();
            }

        }
    }
}
