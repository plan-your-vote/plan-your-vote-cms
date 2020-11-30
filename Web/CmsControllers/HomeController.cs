using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Data;
using Web.Models;
using Web.ViewModels;
using Microsoft.Extensions.Localization;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly int _managedElectionID;
        private readonly ILogger _logger;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
            _managedElectionID = _context.StateSingleton.Find(State.STATE_ID).ManagedElectionID;
        }

        public IActionResult Index()
        {
            State s = _context.StateSingleton.Find(State.STATE_ID);
            Election e = _context.Elections.Where(el => el.ElectionId == _managedElectionID).First();
            DashboardViewModel dashboard = new DashboardViewModel
            {
                ElectionName = _localizer[e.ElectionName],
                CandidatesCount = _context.Candidates
                    .Where(c => c.ElectionId == _managedElectionID)
                    .Count(),
                BallotIssuesCount = _context.BallotIssues
                    .Where(c => c.ElectionId == _managedElectionID)
                    .Count(),
                PollingPlacesCount = _context.PollingPlaces
                    .Where(c => c.ElectionId == _managedElectionID)
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
