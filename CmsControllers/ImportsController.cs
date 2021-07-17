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

namespace Web.CmsControllers
{
    [Authorize(Roles = Constants.Account.ROLE_ADMIN)]
    public class ImportsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<ImportsController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public ImportsController(ApplicationDbContext context, IConfiguration configuration,
            ILogger<ImportsController> logger, IWebHostEnvironment env)
        {
            _context = context;
            _logger = logger;
            _env = env;
            _configuration = configuration;
        }

        public IActionResult LoadElectionsFromJsonFile()
        {
            ViewBag.Importing = "Elections";
            return View("Upload");
        }

        [HttpPost]
        public IActionResult LoadElectionsFromJsonFile(IFormFile file)
        {
            if (file != null)
            {
                // Delete existing elections
                _context.Elections.RemoveRange(_context.Elections);
                _context.SaveChanges();

                string absoluteFileName = uploadTheFile(file);

                List<Election> data = SeedData.GetElections(absoluteFileName);
                _context.Elections.AddRange(data);
                _context.SaveChanges();

                ViewBag.Message = $"File successfully uploaded to {absoluteFileName} comprising {data.Count} items of elections data.";
            }
            else
            {
                ViewBag.Message = $"ERROR: Please choose an elections JSON file to upload.";
            }
            ViewBag.Importing = "Elections";
            return View("Upload");
        }

        public IActionResult LoadIssueOptionsFromJsonFile()
        {
            ViewBag.Importing = "Issue-Options";
            return View("Upload");
        }

        [HttpPost]
        public IActionResult LoadIssueOptionsFromJsonFile(IFormFile file)
        {
            if (file != null)
            {
                // Delete existing issue options
                _context.IssueOptions.RemoveRange(_context.IssueOptions);
                _context.SaveChanges();

                string absoluteFileName = uploadTheFile(file);

                List<IssueOption> data = SeedData.GetIssueOptions(absoluteFileName);
                 _context.IssueOptions.AddRange(data);
                _context.SaveChanges();

                ViewBag.Message = $"File successfully uploaded to {absoluteFileName} comprising {data.Count} items of issue-option data.";
            }
            else
            {
                ViewBag.Message = $"ERROR: Please choose an issue-options JSON file to upload.";
            }
            ViewBag.Importing = "Issue-Options";
            return View("Upload");
        }

        public IActionResult LoadStepsFromJsonFile()
        {
            ViewBag.Importing = "Steps";
            return View("Upload");
        }

        [HttpPost]
        public IActionResult LoadStepsFromJsonFile(IFormFile file)
        {
            if (file != null)
            {
                // Delete existing issue options
                _context.Steps.RemoveRange(_context.Steps);
                _context.SaveChanges();

                string absoluteFileName = uploadTheFile(file);

                List<Step> data = SeedData.GetSteps(absoluteFileName);
                _context.Steps.AddRange(data);
                _context.SaveChanges();

                ViewBag.Message = $"File successfully uploaded to {absoluteFileName} comprising {data.Count} items of step data.";
            }
            else
            {
                ViewBag.Message = $"ERROR: Please choose an steps JSON file to upload.";
            }
            ViewBag.Importing = "Steps";
            return View("Upload");
        }

        public IActionResult LoadBallotIssuesFromJsonFile()
        {
            ViewBag.Importing = "Ballot-Issues";
            return View("Upload");
        }

        [HttpPost]
        public IActionResult LoadBallotIssuesFromJsonFile(IFormFile file)
        {
            if (file != null)
            {
                // Delete existing ballot issues
                _context.BallotIssues.RemoveRange(_context.BallotIssues);
                _context.SaveChanges();

                string absoluteFileName = uploadTheFile(file);

                 List<BallotIssue> data = SeedData.GetBallotIssues(absoluteFileName);
                _context.BallotIssues.AddRange(data);
                _context.SaveChanges();

                ViewBag.Message = $"File successfully uploaded to {absoluteFileName} comprising {data.Count} items of ballots-issues data.";
            }
            else
            {
                ViewBag.Message = $"ERROR: Please choose an ballot-issues JSON file to upload.";
            }
            ViewBag.Importing = "Ballot_Issues";
            return View("Upload");
        }

        public IActionResult LoadPollingPlacesFromJsonFile()
        {
            ViewBag.Importing = "Polling-Places";
            return View("Upload");
        }

        [HttpPost]
        public IActionResult LoadPollingPlacesFromJsonFile(IFormFile file)
        {
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

                ViewBag.Message = $"File successfully uploaded to {absoluteFileName} comprising {data.Count} items of polling places data.";
            }
            else
            {
                ViewBag.Message = $"ERROR: Please choose an polling-places JSON file to upload.";
            }
            ViewBag.Importing = "Polling-Places";
            return View("Upload");
        }

        public IActionResult LoadCandidatesFromJsonFile()
        {
            ViewBag.Importing = "Candidates";
            return View("Upload");
        }

        [HttpPost]
        public IActionResult LoadCandidatesFromJsonFile(IFormFile file)
        {
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

                ViewBag.Message = $"File successfully uploaded to {absoluteFileName} comprising {candidateData.Count} items of data.";
            }
            else
            {

                ViewBag.Message = $"ERROR: Please choose a candidates JSON file to upload.";
            }
            ViewBag.Importing = "Candidates";
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
                uploadDirectory = _configuration["Uploads:DestinationDirectory"];
            else
                uploadDirectory = "uploads/";

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

