using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Web.Data;
using Web.Models;
using Web.Models.JSON;
using Web.Resources;

namespace Web.CmsControllers
{
    [Authorize(Roles = Constants.Account.ROLE_ADMIN)]
    public class ImportsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<ImportsController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly LocService _locService;

        public ImportsController(ApplicationDbContext context, IConfiguration configuration,
            ILogger<ImportsController> logger, IWebHostEnvironment env, LocService locService)
        {
            _context = context;
            _logger = logger;
            _env = env;
            _configuration = configuration;
            _locService = locService;
        }

        public IActionResult LoadElectionsFromJsonFile()
        {
            ViewBag.Importing = _locService.GetLocalizedHtmlString("Election");
            return View("Upload");
        }

        [HttpPost]
        public IActionResult LoadElectionsFromJsonFile(IFormFile file)
        {
            string strType = _locService.GetLocalizedHtmlString("Election");

            if (file != null)
            {
                // Delete existing elections
                _context.Elections.RemoveRange(_context.Elections);
                _context.SaveChanges();

                string absoluteFileName = uploadTheFile(file);

                List<Election> data = SeedData.GetElections(absoluteFileName);
                _context.Elections.AddRange(data);
                _context.SaveChanges();

                string uploadMsg = $@"{_locService.GetLocalizedHtmlString("file_uploaded_to")} 
                        {absoluteFileName}";
                string countMsg = $@"{data.Count} {strType} {_locService.GetLocalizedHtmlString("items")}.";

                ViewBag.Message = $"{uploadMsg} - {countMsg}.";
            }
            else
            {
                string strFormat = _locService.GetLocalizedHtmlString("choose_json_file_to_upload");
                ViewBag.Message = String.Format(strFormat, strType);
            }
            ViewBag.Importing = strType;
            return View("Upload");
        }

        public IActionResult LoadIssueOptionsFromJsonFile()
        {
            ViewBag.Importing = _locService.GetLocalizedHtmlString("BallotIssue");
            return View("Upload");
        }

        [HttpPost]
        public IActionResult LoadIssueOptionsFromJsonFile(IFormFile file)
        {
            string strType = _locService.GetLocalizedHtmlString("BallotIssueOptions");

            if (file != null)
            {
                // Delete existing issue options
                _context.IssueOptions.RemoveRange(_context.IssueOptions);
                _context.SaveChanges();

                string absoluteFileName = uploadTheFile(file);

                List<IssueOption> data = SeedData.GetIssueOptions(absoluteFileName);
                 _context.IssueOptions.AddRange(data);
                _context.SaveChanges();

                string uploadMsg = $@"{_locService.GetLocalizedHtmlString("file_uploaded_to")} 
                        {absoluteFileName}";
                string countMsg = $@"{data.Count} {strType} {_locService.GetLocalizedHtmlString("items")}.";

                ViewBag.Message = $"{uploadMsg} - {countMsg}.";
            }
            else
            {
                string strFormat = _locService.GetLocalizedHtmlString("choose_json_file_to_upload");
                ViewBag.Message = String.Format(strFormat, strType);
            }
            ViewBag.Importing = strType;
            return View("Upload");
        }

        public IActionResult LoadStepsFromJsonFile()
        {
            ViewBag.Importing = _locService.GetLocalizedHtmlString("shared_layout_ElectionDetailsOptionSteps");
            return View("Upload");
        }

        [HttpPost]
        public IActionResult LoadStepsFromJsonFile(IFormFile file)
        {
            string strType = _locService.GetLocalizedHtmlString("shared_layout_ElectionDetailsOptionSteps");
            if (file != null)
            {
                // Delete existing issue options
                _context.Steps.RemoveRange(_context.Steps);
                _context.SaveChanges();

                string absoluteFileName = uploadTheFile(file);

                List<Step> data = SeedData.GetSteps(absoluteFileName);
                _context.Steps.AddRange(data);
                _context.SaveChanges();

                string uploadMsg = $@"{_locService.GetLocalizedHtmlString("file_uploaded_to")} 
                        {absoluteFileName}";
                string countMsg = $@"{data.Count} 
                        {strType} 
                        {_locService.GetLocalizedHtmlString("items")}.";

                ViewBag.Message = $"{uploadMsg} - {countMsg}.";
            }
            else
            {
                string strFormat = _locService.GetLocalizedHtmlString("choose_json_file_to_upload");
                ViewBag.Message = String.Format(strFormat, strType);
            }
            ViewBag.Importing = strType;
            return View("Upload");
        }

        public IActionResult LoadBallotIssuesFromJsonFile()
        {
            ViewBag.Importing = _locService.GetLocalizedHtmlString("BallotIssue");
            return View("Upload");
        }

        [HttpPost]
        public IActionResult LoadBallotIssuesFromJsonFile(IFormFile file)
        {
            string strType = _locService.GetLocalizedHtmlString("BallotIssue");

            if (file != null)
            {
                // Delete existing ballot issues
                _context.BallotIssues.RemoveRange(_context.BallotIssues);
                _context.SaveChanges();

                string absoluteFileName = uploadTheFile(file);

                 List<BallotIssue> data = SeedData.GetBallotIssues(absoluteFileName);
                _context.BallotIssues.AddRange(data);
                _context.SaveChanges();

                string uploadMsg = $@"{_locService.GetLocalizedHtmlString("file_uploaded_to")} 
                        {absoluteFileName}";
                string countMsg = $@"{data.Count} {strType} {_locService.GetLocalizedHtmlString("items")}.";

                ViewBag.Message = $"{uploadMsg} - {countMsg}.";
            }
            else
            {
                string strFormat = _locService.GetLocalizedHtmlString("choose_json_file_to_upload");
                ViewBag.Message = String.Format(strFormat, strType);
            }
            ViewBag.Importing = strType;
            return View("Upload");
        }

        public IActionResult LoadPollingPlacesFromJsonFile()
        {
            ViewBag.Importing = _locService.GetLocalizedHtmlString("home_index_pollingPlaces");
            return View("Upload");
        }

        [HttpPost]
        public IActionResult LoadPollingPlacesFromJsonFile(IFormFile file)
        {
            string strType = _locService.GetLocalizedHtmlString("home_index_pollingPlaces");

            if (file != null)
            {
                // Delete existing polling dates
                _context.PollingPlaceDates.RemoveRange(_context.PollingPlaceDates);
                _context.SaveChanges();
                _context.PollingPlaces.RemoveRange(_context.PollingPlaces);
                _context.SaveChanges();

                string absoluteFileName = uploadTheFile(file);

                List<PollingPlace> data = SeedData.GetPollingPlaces(absoluteFileName);
                _context.PollingPlaces.AddRange(data);
                _context.SaveChanges();

                string uploadMsg = $@"{_locService.GetLocalizedHtmlString("file_uploaded_to")} 
                        {absoluteFileName}";
                string countMsg = $@"{data.Count} {strType} {_locService.GetLocalizedHtmlString("items")}.";

                ViewBag.Message = $"{uploadMsg} - {countMsg}.";
            }
            else
            {
                string strFormat = _locService.GetLocalizedHtmlString("choose_json_file_to_upload");
                ViewBag.Message = String.Format(strFormat, strType);
            }
            ViewBag.Importing = strType;
            return View("Upload");
        }

        public IActionResult LoadCandidatesFromJsonFile()
        {
            ViewBag.Importing = _locService.GetLocalizedHtmlString("candidates_index_title");
            return View("Upload");
        }

        [HttpPost]
        public IActionResult LoadCandidatesFromJsonFile(IFormFile file)
        {
            string strType = _locService.GetLocalizedHtmlString("candidates_index_title");

            List<JSONCandidate> candidateData;
            string uploadDirecotroy;

            if (file != null)
            {
                // Extract file name from whatever was posted by browser
                var fileName = System.IO.Path.GetFileName(file.FileName);

                if (_configuration["Uploads:DestinationDirectory"] != null)
                    uploadDirecotroy = _configuration["Uploads:DestinationDirectory"];
                else 
                    uploadDirecotroy = "uploads/";

                var uploadPath = Path.Combine(_env.ContentRootPath, uploadDirecotroy);

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string absoluteFileName = uploadPath + fileName;

                // If file with same name exists delete it
                if (System.IO.File.Exists(absoluteFileName))
                {
                    System.IO.File.Delete(absoluteFileName);
                }

                // Create new local file and copy contents of uploaded file
                using (var localFile = System.IO.File.OpenWrite(absoluteFileName))
                using (var uploadedFile = file.OpenReadStream())
                {
                    uploadedFile.CopyTo(localFile);
                }

                deleteCandidateContactRaceOrganizationData();

                candidateData = SeedData.GetJsonData<JSONCandidate>(absoluteFileName);

                var organizations = SeedData.GetOrganizations(_context, candidateData).ToArray();
                _context.Organizations.AddRange(organizations);
                _context.SaveChanges();

                var races = SeedData.GetRaces(_context, candidateData).ToArray();
                _context.Races.AddRange(races);
                _context.SaveChanges();

                SeedData.GetCandidatesAndContacts(_context, candidateData);

                string uploadMsg = $@"{_locService.GetLocalizedHtmlString("file_uploaded_to")} 
                        {absoluteFileName}";
                string countMsg = $@"{candidateData.Count} {strType} {_locService.GetLocalizedHtmlString("items")}.";

                ViewBag.Message = $"{uploadMsg} - {countMsg}.";
            }
            else
            {

                string strFormat = _locService.GetLocalizedHtmlString("choose_json_file_to_upload");
                ViewBag.Message = String.Format(strFormat, strType);
            }
            ViewBag.Importing = strType;
            return View("Upload");
        }

        private void deleteCandidateContactRaceOrganizationData()
        {
            _context.CandidateDetails.RemoveRange(_context.CandidateDetails);
            _context.SaveChanges();
            _context.Candidates.RemoveRange(_context.Candidates);
            _context.SaveChanges();
            _context.Contacts.RemoveRange(_context.Contacts);
            _context.SaveChanges();
            _context.CandidateRaces.RemoveRange(_context.CandidateRaces);
            _context.SaveChanges();
            _context.Races.RemoveRange(_context.Races);
            _context.SaveChanges();
            _context.Organizations.RemoveRange(_context.Organizations);
            _context.SaveChanges();

        }

        private string uploadTheFile(IFormFile file)
        {
            string uploadDirectory;

            // Extract file name from whatever was posted by browser
            var fileName = System.IO.Path.GetFileName(file.FileName);

            if (_configuration["Uploads:DestinationDirectory"] != null)
                uploadDirectory = _configuration["Uploads:DestinationDirectory"] + Path.DirectorySeparatorChar;
            else
                uploadDirectory = "uploads" + Path.DirectorySeparatorChar;

            var uploadPath = Path.Combine(_env.ContentRootPath, uploadDirectory);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string absoluteFileName = uploadPath + fileName;

            // If file with same name exists delete it
            if (System.IO.File.Exists(absoluteFileName))
            {
                System.IO.File.Delete(absoluteFileName);
            }

            // Create new local file and copy contents of uploaded file
            using (var localFile = System.IO.File.OpenWrite(absoluteFileName))
            using (var uploadedFile = file.OpenReadStream())
            {
                uploadedFile.CopyTo(localFile);
            }

            return absoluteFileName;
        }

    }
}

