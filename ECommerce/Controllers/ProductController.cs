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

        public IActionResult Menu(IEnumerable<Product> filter)
        {
            if (filter.Any())
            {
                return View(filter);
            }
            else
            {
                IEnumerable<Product> products = _db.Produtos;
                return View(products);
            }

        }

        public IActionResult Filter(string category, string search)
        {
            if (category != "Todos" && string.IsNullOrEmpty(search))
            {
                IEnumerable<Product> productsCategory = _db.Produtos.Where(p => p.Category == category);

                return View("Menu", productsCategory);
            }else

            if (!string.IsNullOrEmpty(search))
            {
                IEnumerable<Product> productsSearch = _db.Produtos.Where(p => p.Name.Contains(search) || p.Description.Contains(search));

                return View("Menu", productsSearch);
            }
            else
            {
                return View("Menu", _db.Produtos);
            }
        }
    }
}
