using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.ViewModels;

namespace Web.CmsControllers
{
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
            var users = _context.Users.ToList();
            var roles = _context.Roles.ToList();
            var userrole = _context.UserRoles.ToList();
            var models = new List<UserRoleViewModel>();
            foreach(IdentityUser u in users)
            {
                UserRoleViewModel model = new UserRoleViewModel();
                model.userId = u.Id;
                model.user = u;
                model.roles = new List<IdentityRole>();
                foreach(IdentityUserRole<string> ur in userrole)
                {
                    if (ur.UserId == u.Id)
                    {
                        model.roles.Add(roles.FirstOrDefault(r=>r.Id == ur.RoleId));
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

            UserRoleViewModel model = new UserRoleViewModel();
            model.userId = id;
            model.user = users.FirstOrDefault(u => u.Id == id);
            model.roles = new List<IdentityRole>();
            foreach (IdentityUserRole<string> ur in userrole)
            {
                if (ur.UserId == id)
                {
                    model.roles.Add(roles.FirstOrDefault(r => r.Id == ur.RoleId));
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
            _context.UserRoles.Remove(userrole);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> EditRole()
        {

            var roles = _context.Roles.ToList();

            return View(roles);
        }

        public IActionResult CreateRole()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole([Bind("Name")] IdentityRole identityRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(identityRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(EditRole));
            }
            return View(identityRole);
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _context.Roles.FindAsync(id);
            var userroles = _context.UserRoles.Where(ur => ur.RoleId == id);
            foreach(IdentityUserRole<string> ur in userroles)
            {
                _context.UserRoles.Remove(ur);
            }
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(EditRole));
        }
        
    }
}
