using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using static ECommerce.Controllers.BasketController;

namespace ECommerce.Controllers
{
    public class FinalizeController : Controller
    {
        readonly private FinalizeModel fm;
        readonly private ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FinalizeController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor, IOptions<EmailSettings> emailSettings)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            fm = new FinalizeModel(db, httpContextAccessor, emailSettings);
        }

        public IActionResult ending(List<ProductQuantity> productQuantities)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            List<Product> products = new List<Product>();

            foreach (var prod in productQuantities)
            {
                Product product = _db.Produtos.SingleOrDefault(p => p.Id == prod.ProductId);
                product.Quantity = prod.Quantity;
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
