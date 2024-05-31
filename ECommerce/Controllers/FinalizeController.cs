using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static ECommerce.Controllers.BasketController;

namespace ECommerce.Controllers
{
    public class FinalizeController : Controller
    {
        readonly private FinalizeModel fm;
        readonly private ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public class ProductQuantity
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }

        public FinalizeController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            fm = new FinalizeModel(db, httpContextAccessor);
        }

        public IActionResult ending(List<ProductQuantity> productQuantities)
        {
            List<Product> products = new List<Product>();

            foreach (var prod in productQuantities)
            {
                Product product = _db.Produtos.SingleOrDefault(p => p.Id == prod.ProductId);
                product.Quantity = _db.Basket.Where(b => b.ProductId == product.Id).Sum(b => b.Quantity);
                products.Add(product);
            }

            var user = _httpContextAccessor.HttpContext.User;
            var email = user.FindFirstValue(ClaimTypes.Email);

            fm.SendEmail(products, email);

            BasketController bc = new BasketController(_db);
            bc.DelBasket();

            return View(products);
        }
    }
}
