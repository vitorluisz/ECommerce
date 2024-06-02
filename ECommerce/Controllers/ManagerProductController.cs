using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    
    public class ManagerProductController : Controller
    {
        readonly private ProductModel pm;
        readonly private ApplicationDbContext _db;

        public ManagerProductController(ApplicationDbContext db)
        {
            _db = db;
            pm = new ProductModel(db);
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
                var IsNameValid = pm.IsNameValid(product);
                var IsDescriptionValid = pm.IsDescriptionValid(product);
                var IsPriceValid = pm.IsPriceValid(product);
                var IsCategoryValid = pm.IsCategoryValid(product);
                var IsImageValid = pm.IsImageValid(product);

                if (IsNameValid != string.Empty)
                {
                    TempData["ProductErro"] = IsNameValid;
                }
                else if (IsDescriptionValid != string.Empty)
                {
                    TempData["ProductErro"] = IsDescriptionValid;
                }
                else if (IsPriceValid != string.Empty)
                {
                    TempData["ProductErro"] = IsPriceValid;
                }
                else if (IsCategoryValid != string.Empty)
                {
                    TempData["ProductErro"] = IsCategoryValid;
                }
                else if (IsImageValid != string.Empty)
                {
                    TempData["ProductErro"] = IsImageValid;
                }
                else
                {
                    _db.Add(product);
                    _db.SaveChanges();
                    TempData["MenuSuccess"] = "Produto adicionado com sucesso!";
                    return RedirectToAction("Menu", "Product");
                }
            }
            else
            {
                TempData["MenuError"] = "Erro ao adicionar produto.";
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult ManagerDelProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                var productid = _db.Produtos.Where(p => p.Id == product.Id).FirstOrDefault();
                if (productid != null)
                {
                    _db.Remove(product);
                    _db.SaveChanges();
                    TempData["MenuSuccess"] = "Produto deletado com sucesso!";
                    return RedirectToAction("Menu", "Product");
                }
                else
                {
                    TempData["ProductErro"] = "Produto não encontrado.";
                    return RedirectToAction("AddProduct", "ManagerProduct");
                }
                
            }
            else
            {
                TempData["ProductErro"] = "Erro ao deletar produto.";
                return RedirectToAction("AddProduct", "ManagerProduct");
            }
        }
    }
}
