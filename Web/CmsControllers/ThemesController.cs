using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Web.ViewModels;

namespace Web.CmsControllers
{

    [Authorize(Roles = Constants.Account.ROLE_ADMIN)]
    public class ThemesController : Controller
    {
		
		private readonly IHostingEnvironment _env;

        private readonly ApplicationDbContext _context;

        public ThemesController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
			_env = env;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(GetThemesViewModel());
        }

        [HttpGet]
        public IActionResult getImg()
        {
        	return File("~/images/logo/logo.png", "image/jpeg");
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

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {

            string fileType = "";
            if (file == null || file.Length == 0)
                return Content("file not selected");
            if (file.ContentType != "image/jpeg"
                && file.ContentType != "image/png"
                && file.ContentType != "image/gif")
            {
                return Content("file not an image");
                // return View("Index");
            }

            if (file.ContentType == "image/png")
            {
                fileType = "png";
            }

            if (file.ContentType == "image/jpeg")
            {
                fileType = "jpeg";
            }

            if (file.ContentType == "image/gif")
            {
                fileType = "gif";
            }

            string filename = "logo." + fileType;

            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot", "images",
                "logo", filename);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return RedirectToAction("Index");
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