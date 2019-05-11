using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Data;

namespace Web
{
    [Authorize]
    public class CandidatesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly int _managedElectionID;

        public CandidatesController(ApplicationDbContext context)
        {
            _context = context;
            _managedElectionID = _context.StateSingleton.Find(State.STATE_ID).ManagedElectionID;
        }

        // GET: Candidates
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Candidates
                .Where(c => c.ElectionId == _managedElectionID)
                .Include(c => c.Organization).Include(c => c.Contacts).Include(c => c.CandidateRaces);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Candidates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidates
                .Include(c => c.Details)
                .Include(c => c.Organization)
                .Include(c => c.CandidateRaces)
                .FirstOrDefaultAsync(m => m.CandidateId == id);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        // GET: Candidates/Create
        public IActionResult Create()
        {
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationId");
            return View();
        }

        // POST: Candidates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Candidate model, IFormFile image, int organizationId, string removedDetails)
        {
            var fileName = "";
            var nameOfile = "";
            if (image != null)
            {
                if (image.ContentType != "image/jpeg"
                        && image.ContentType != "image/png"
                        && image.ContentType != "image/gif")
                {
                    ViewData["ImageMessage"] = "Invalid image type. Image must be a JPEG, GIF, or PNG.";
                    ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationId");
                    return View();
                }

                if (image.Length < 4500)
                {
                    ViewData["ImageMessage"] = "Invalid image size. Image must be a minimum size of 5KB.";
                    ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationId");
                    return View();
                }

                nameOfile = "images\\" + GenerateImageId() + Path.GetFileName(image.FileName);
                fileName = "wwwroot\\" + nameOfile;
                image.CopyTo(new FileStream(fileName, FileMode.Create));
                model.Picture = nameOfile;
            }

            // Remove the "deleted" items from the list
            if (removedDetails != null && removedDetails != "")
            {
                string[] itemsToRemove = removedDetails.Split(',', StringSplitOptions.RemoveEmptyEntries);
                int[] indexes = new int[itemsToRemove.Length];
                for (int i = 0; i < itemsToRemove.Length; ++i)
                {
                    indexes[i] = int.Parse(itemsToRemove[i]);
                }

                // sort in ascending order
                indexes = indexes.OrderBy(i => i).ToArray();

                for (int i = indexes.Length - 1; i >= 0; --i)
                {
                    model.Details.RemoveAt(indexes[i]);
                }
            }

            Candidate candidate = new Candidate
            {
                CandidateId = model.CandidateId,
                ElectionId = _managedElectionID,
                Name = model.Name,
                Picture = model.Picture,
                OrganizationId = organizationId,
                Details = model.Details,
                Contacts = model.Contacts
            };
            ModelState.Clear();

            if (TryValidateModel(candidate))
            {
                _context.Add(candidate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationId", candidate.OrganizationId);
            return View(candidate);
        }

        public static string GenerateImageId()
        {
            Random R = new Random();
            string strDateTimeNumber = DateTime.Now.ToString("yyyyMMddHHmmssms");
            string strRandomResult = R.Next(1, 1000).ToString();
            return strDateTimeNumber + strRandomResult;
        }

        public virtual IActionResult GetFields(int count)
        {
            List<CandidateDetail> list = new List<CandidateDetail>();

            for (int i = 0; i <= count; i++)
            {
                list.Add(new CandidateDetail());
            }

            Candidate candidate = new Candidate
            {
                Details = list
            };

            return PartialView("CandidateDetail", candidate);
        }

        // GET: Candidates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidates
                .Include(c => c.Details)
                .FirstOrDefaultAsync(c => c.CandidateId == id);
            if (candidate == null)
            {
                return NotFound();
            }
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationId", candidate.OrganizationId);
            return View(candidate);
        }

        // POST: Candidates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Candidate model, IFormFile image, string removedDetails)
        {
            if (id != model.CandidateId)
            {
                return NotFound();
            }

            // Remove the "deleted" items from the list
            if (removedDetails != null && removedDetails != "")
            {
                string[] itemsToRemove = removedDetails.Split(',', StringSplitOptions.RemoveEmptyEntries);
                int[] indexes = new int[itemsToRemove.Length];
                for (int i = 0; i < itemsToRemove.Length; ++i)
                {
                    indexes[i] = int.Parse(itemsToRemove[i]);
                }

                // sort in ascending order
                indexes = indexes.OrderBy(i => i).ToArray();

                for (int i = indexes.Length-1; i >= 0; --i)
                {
                    model.Details.RemoveAt(indexes[i]);
                }
            }

            Candidate candidate = new Candidate
            {
                CandidateId = id,
                ElectionId = _managedElectionID,
                Name = model.Name,
                OrganizationId = model.OrganizationId,
                Details = model.Details,
                Contacts = model.Contacts
            };

            ModelState.Clear();

            if (TryValidateModel(candidate))
            {
                if (image != null)
                {
                    if (image.ContentType != "image/jpeg" 
                        && image.ContentType != "image/png" 
                        && image.ContentType != "image/gif")
                    {
                        ViewData["ImageMessage"] = "Invalid image type. Image must be a JPEG, GIF, or PNG.";
                        ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationId", model.OrganizationId);
                        return View(model);
                    }

                    if (image.Length < 4500)
                    {
                        ViewData["ImageMessage"] = "Invalid image size. Image must be a minimum size of 5KB.";
                        ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationId", model.OrganizationId);
                        return View(model);
                    }

                    string nameOfile = "images\\" + GenerateImageId() + Path.GetFileName(image.FileName);
                    string fileName = "wwwroot\\" + nameOfile;
                    image.CopyTo(new FileStream(fileName, FileMode.Create));
                    candidate.Picture = nameOfile;
                }
                else
                {
                    var existing = _context.Candidates
                        .Where(c => c.CandidateId == id)
                        .Select(c => new { Pic = c.Picture });
                    foreach (var result in existing)
                    {
                        candidate.Picture = result.Pic;
                    }
                }

                try
                {
                    // remove all the candidate's current details from the database because the update will just
                    // recreate them anyways
                    var existingDetails = _context.CandidateDetails.Where(cd => cd.CandidateId == id).ToList();
                    _context.RemoveRange(existingDetails);

                    _context.Update(candidate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidateExists(candidate.CandidateId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationId", candidate.OrganizationId);
            return View(model);
        }

        // GET: Candidates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidates
                .Include(c => c.Organization)
                .Include(c => c.Details)
                .FirstOrDefaultAsync(m => m.CandidateId == id);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidateExists(int id)
        {
            return _context.Candidates.Any(e => e.CandidateId == id);
        }
    }
}
