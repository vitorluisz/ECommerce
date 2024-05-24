using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class LoginController : Controller
    {
        readonly private ApplicationDbContext _db;

        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult login()
        {
            return View();
        }
    }
}
