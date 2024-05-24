using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {
        readonly private ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            return View(_db.Produtos);
        }
        

        public IActionResult AddProduct()
        {
            return View();
        }

        public IActionResult Menu()
        {
            IEnumerable<Product> products = _db.Produtos;
            return View(products);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            product.Quantity = 1;
            //if (ModelState.IsValid)
            {
                product.Quantity = 1;
                _db.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Menu");
            }

            //return View(product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
