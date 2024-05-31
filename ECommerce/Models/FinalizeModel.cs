using ECommerce.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Principal;

namespace ECommerce.Models
{
    public class FinalizeModel
    {
        private readonly ClaimsPrincipal _user;
        readonly private ApplicationDbContext _db;

        public FinalizeModel(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _user = httpContextAccessor.HttpContext.User;
        }

        public void SendEmail(List<Product> products, string email)
        {
            

        }
    }
}
