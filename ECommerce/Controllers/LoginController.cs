using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace ECommerce.Controllers
{
    public class LoginController : Controller
    {
        readonly private CustomerModel cm;
        readonly private ApplicationDbContext _db;

        public LoginController(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            cm = new CustomerModel(db, configuration);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> LogoutAsync()
        {
            BasketController bc = new BasketController(_db);
            bc.DelBasket();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Menu", "Product");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Customer model)
        {
            if (ModelState.IsValid)
            {
                string query = "SELECT TOP 1 * FROM Customer WHERE Email = @email AND Password = @password";
                var customer = cm.DatabaseCustomer(model, query);

                if (customer != null)
                {
                    BasketController bc = new BasketController(_db);
                    bc.DelBasket();
                    await SignInAsync(customer);
                    return RedirectToAction("Menu", "Product");
                }
                else
                {
                    TempData["MensagemErro"] = "Login ou senha incorretos.";
                    return View();
                }
            }

            return RedirectToAction("Menu", "Product");
        }

        private async Task<IActionResult> SignInAsync(Customer customer)
        {
            var claims = new[] {
                new Claim(ClaimTypes.Name, customer.FirstName),
                new Claim(ClaimTypes.Role, customer.IsAdmin ? "Admin" : "Usuario"),
                new Claim(ClaimTypes.Email, customer.Email)
            };

            var identity = new ClaimsIdentity(claims, "Password");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);

            if (customer.IsAdmin)
            {
                return RedirectToAction("AdminDashboard", "Admin");
            }
            else
            {
                return RedirectToAction("Menu", "Product");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RequestRegister(Customer customer)
        {
            if (ModelState.IsValid)
            {
                string query = "SELECT * FROM Customer WHERE Email = @email";
                var result = cm.DatabaseCustomer(customer, query);

                if (result == null){
                    _db.Add(customer);
                    _db.SaveChanges();
                    TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";
                    await Login(customer);
                }
                else
                {
                    TempData["MensagemErro"] = "Email ja cadastrado.";
                }
            }
            return RedirectToAction("Menu", "Product");
        }
    }
}
