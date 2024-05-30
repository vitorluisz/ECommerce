using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    
    public class ManagerProductController : Controller
    {
        readonly private ApplicationDbContext _db;

        public ManagerProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddProduct()
        {
            return View("~/Views/Product/AddProduct.cshtml");
        }

        [HttpPost]
        public IActionResult ManagerAddProduct(Product product)
        {
            product.Quantity = 1;
            if (ModelState.IsValid)
            {
                product.Quantity = 1;
                _db.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Menu", "Product");
            }

            return View(product);
        }
    }
}
