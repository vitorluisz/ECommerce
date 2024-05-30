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
            await HttpContext.SignOutAsync();
            return RedirectToAction("Menu", "Product");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Customer model)
        {
            if (ModelState.IsValid)
            {
                var customer = cm.DatabaseCustomer(model);

                if (customer != null)
                {
                    await SignInAsync(customer);
                    return RedirectToAction("Menu", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password");
                }
            }

            return RedirectToAction("Menu", "Product");
        }

        private async Task<IActionResult> SignInAsync(Customer customer)
        {
            var claims = new[] {
                new Claim(ClaimTypes.Name, customer.Email),
                new Claim(ClaimTypes.Role, customer.IsAdmin ? "Admin" : "Usuario")
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
                _db.Add(customer);
                _db.SaveChanges();
                await Login(customer);
            }
            return RedirectToAction("Menu", "Product");
        }
    }
}
