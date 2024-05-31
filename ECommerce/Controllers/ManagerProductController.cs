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
                _db.Add(product);
                _db.SaveChanges();
                TempData["MensagemSucesso"] = "Produto adicionado com sucesso!";
                return RedirectToAction("Menu", "Product");
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult ManagerDelProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Remove(product);
                _db.SaveChanges();
                TempData["MensagemSucesso"] = "Produto deletado com sucesso!";
                return RedirectToAction("Menu", "Product");
            }

            return View(product);
        }
    }
}
