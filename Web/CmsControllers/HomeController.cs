using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            State s = _context.StateSingleton.Find(State.STATE_ID);
            Election e = _context.Elections.Where(el => el.ElectionId == s.CurrentElection).First();
            DashboardViewModel dashboard = new DashboardViewModel
            {
                ElectionName = e.Name,
                CandidatesCount = _context.Candidates
                    .Where(c => c.ElectionId == s.CurrentElection)
                    .Count(),
                BallotIssuesCount = _context.BallotIssues
                    .Where(c => c.ElectionId == s.CurrentElection)
                    .Count(),
                PollingStationsCount = _context.PollingStations
                    .Where(c => c.ElectionId == s.CurrentElection)
                    .Count()
            };

            return View(dashboard);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) });
            
            return LocalRedirect(returnUrl);
        }
    }
}
