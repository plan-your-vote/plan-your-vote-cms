using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using PlanYourVoteLibrary2;
using Web.ViewModels;

namespace Web.CmsControllers
{
    [Authorize(Roles = Constants.Account.ROLE_ADMIN)]
    public class ThemesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThemesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(GetThemesViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ThemesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string newThemeName;

                try
                {
                    newThemeName = ChangeCurrentTheme(viewModel);
                    TempData["Success"] = $"{newThemeName} voting theme has been selected";
                }
                catch (Exception)
                {
                    //TODO Log error
                    ViewData["Error"] = $"Error occurred while changing voting theme";
                }
            }
            return View(GetThemesViewModel());
        }

        private string ChangeCurrentTheme(ThemesViewModel viewModel)
        {
            Theme currentSelectedTheme = _context.Themes.Where(t => t.Selected).First();
            currentSelectedTheme.Selected = false;
            _context.Update(currentSelectedTheme);

            Theme newSelectedTheme = _context.Themes.First(t => t.ThemeName == viewModel.SelectedTheme);
            newSelectedTheme.Selected = true;
            _context.Update(newSelectedTheme);

            _context.SaveChangesAsync();

            return newSelectedTheme.ThemeName;
        }

        private ThemesViewModel GetThemesViewModel()
        {
            List<Theme> themes = _context.Themes.ToList();
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            foreach (Theme theme in themes)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Text = theme.ThemeName,
                    Value = theme.ThemeName
                });
            }

            return new ThemesViewModel()
            {
                SelectedTheme = themes.First(t => t.Selected).ThemeName,
                Themes = selectListItems,
            };
        }
    }
}