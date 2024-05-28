using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.Controllers
{
    public class LoginController : Controller
    {
        readonly private ApplicationDbContext _db;

        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Customer model)
        {
            if (ModelState.IsValid)
            {
                var customer = _db.Customer
                    .Where(c => c.Email == model.Email && c.Password == model.Password)
                    .FirstOrDefault();

                if (customer != null)
                {
                    await SignInAsync(customer);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password");
                }
            }

            return View(model);
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
                return RedirectToAction("Index", "Home");
            }
        }

        

        //public IActionResult Login()
        //{
        //    return View();
        //}

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RequestLogin(Customer customer)
        {

            return RedirectToAction("Menu");
        }

        [HttpPost]
        public IActionResult RequestRegister(Customer customer)
        {
            return RedirectToAction("Menu");
        }
    }
}
