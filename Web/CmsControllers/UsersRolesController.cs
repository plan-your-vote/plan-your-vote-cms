using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Web.Data;

namespace Web.Controllers
{
    
    public class UsersRoles : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersRoles(ApplicationDbContext context)
        {
            _context = context;
        }


        public ActionResult Index()
        {
            UsersRoles userRoles;
            var roles =  _context.Roles.ToList();
            var users = _context.Users.ToList();
            return View();
        }

    }
}
