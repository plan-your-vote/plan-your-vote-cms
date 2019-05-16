using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.ViewModels;

namespace Web.CmsControllers
{
    [Authorize(Roles = Constants.Account.ROLE_ADMIN)]
    public class UserRoleViewModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRoleViewModelsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserRoleViewModels
        public IActionResult Index()
        {
            //var users = _context.Users.ToList();
            //var roles = _context.Roles.ToList();
            //var userrole = _context.UserRoles.ToList();
            //var models = new List<UserRoleViewModel>();
            //foreach (IdentityUser u in users)
            //{
            //    UserRoleViewModel model = new UserRoleViewModel();
            //    model.userId = u.Id;
            //    model.user = u;
            //    model.roles = new List<IdentityRole>();
            //    foreach (IdentityUserRole<string> ur in userrole)
            //    {
            //        if (ur.UserId == u.Id)
            //        {
            //            model.roles.Add(roles.FirstOrDefault(r => r.Id == ur.RoleId));
            //        }
            //    }
            //    models.Add(model);
            //}
            //return View(models);

            var models = new List<UserRoleViewModel>();

            var users = _context.Users.ToList();
            var roles = _context.Roles.ToList();
            var userrole = _context.UserRoles.ToList();

            foreach (IdentityUser user in users)
            {
                UserRoleViewModel model = new UserRoleViewModel
                {
                    UserId = user.Id,
                    User = user,
                    Roles = new List<IdentityRole>()
                };

                foreach (IdentityUserRole<string> ur in userrole)
                {
                    if (ur.UserId == user.Id)
                    {
                        model.Roles.Add(roles.Find(r => r.Id == ur.RoleId));
                    }
                }

                models.Add(model);
            }
            return View(models);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = _context.Users.ToList();
            var roles = _context.Roles.ToList();
            var userrole = _context.UserRoles.ToList();

            UserRoleViewModel model = new UserRoleViewModel
            {
                UserId = id,
                User = users.Find(u => u.Id == id),
                Roles = new List<IdentityRole>()
            };

            foreach (IdentityUserRole<string> ur in userrole)
            {
                if (ur.UserId == id)
                {
                    model.Roles.Add(roles.Find(r => r.Id == ur.RoleId));
                }
            }

            if (model == null)
            {
                return NotFound();
            }

            ViewData["roles"] = roles;
            return View(model);
        }

        public async Task<IActionResult> AddUserRole(string id, string id2)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Id == id);
            var user = _context.Users.FirstOrDefault(u => u.Id == id2);
            IdentityUserRole<string> iur = new IdentityUserRole<string>();
            iur.UserId = id2;
            iur.RoleId = id;
            await _context.UserRoles.AddAsync(iur);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> DeleteUserRole(string id, string id2)
        {
            var userrole = _context.UserRoles.FirstOrDefault(ur => ur.RoleId == id && ur.UserId == id2);
            var adminrole = _context.Roles.FirstOrDefault(r => r.Name == Constants.Account.ROLE_ADMIN);
            if (id == adminrole.Id)
            {
                if (_context.UserRoles.Count(ur => ur.RoleId == adminrole.Id) <= 1)
                {
                    TempData["info"] = "At least 1 Admin";
                    return Redirect(Request.Headers["Referer"].ToString());
                }
            }
            _context.UserRoles.Remove(userrole);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
