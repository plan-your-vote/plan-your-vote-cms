using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;
using Web.ViewModels;
using Microsoft.Extensions.Localization;

namespace Web
{
    [Authorize]
    public class CandidatesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly int _managedElectionID;

        private readonly IStringLocalizer<CandidatesController> _localizer;

        public CandidatesController(ApplicationDbContext context)
        {
            _context = context;
            _managedElectionID = _context.StateSingleton.Find(State.STATE_ID).ManagedElectionID;
        }

        // GET: Candidates
        public async Task<IActionResult> Index()
        {
            var races = await _context.Races
                .Where(r => r.ElectionId == _managedElectionID)
                .OrderBy(r => r.BallotOrder)
                .ToListAsync();

            var candidateRaces = (
                await _context.CandidateRaces
                    .Include(cr => cr.Race)
                    .Include(cr => cr.Candidate)
                    .Include(cr => cr.Candidate.Organization)
                    .Where(cr => cr.Candidate.ElectionId == _managedElectionID)
                    .OrderBy(cr => cr.RaceId).ThenBy(cr => cr.BallotOrder)
                    .ToListAsync()
                )
                .GroupBy(cr => cr.RaceId)
                .Select(cr => cr).ToList();


            var unlisted = await _context.Candidates
                .Include(c => c.Organization)
                .Where(c => c.ElectionId == _managedElectionID && c.CandidateRaces.Count == 0)
                .OrderBy(c => c.Name)
                .ToListAsync();

            CandidatesByRaceViewModel model = new CandidatesByRaceViewModel
            {
                Races = races,
                CandidatesByRace = candidateRaces,
                UnlistedCandidates = unlisted
            };
            return View(model);
        }

        public virtual async Task<IActionResult> GetCandidates(string orderBy)
        {
            CandidatesByRaceViewModel model = new CandidatesByRaceViewModel
            {
                Races = await _context.Races
                    .Where(r => r.ElectionId == _managedElectionID)
                    .OrderBy(r => r.BallotOrder)
                    .ToListAsync()
            };

            if ("ballot-order".Equals(orderBy))
            {
                model.CandidatesByRace = (
                    await _context.CandidateRaces
                        .Include(cr => cr.Race)
                        .Include(cr => cr.Candidate)
                        .Include(cr => cr.Candidate.Organization)
                        .Where(cr => cr.Candidate.ElectionId == _managedElectionID)
                        .OrderBy(cr => cr.RaceId).ThenBy(cr => cr.BallotOrder)
                        .ToListAsync()
                    )
                    .GroupBy(cr => cr.RaceId)
                    .Select(cr => cr).ToList();

                model.UnlistedCandidates = await _context.Candidates
                    .Include(c => c.Organization)
                    .Where(c => c.ElectionId == _managedElectionID && c.CandidateRaces.Count == 0)
                    .OrderBy(c => c.Name)
                    .ToListAsync();
            }
            else if ("alphabet".Equals(orderBy))
            {
                model.CandidatesByRace = (
                    await _context.CandidateRaces
                        .Include(cr => cr.Race)
                        .Include(cr => cr.Candidate)
                        .Include(cr => cr.Candidate.Organization)
                        .Where(cr => cr.Candidate.ElectionId == _managedElectionID)
                        .OrderBy(cr => cr.RaceId).ThenBy(cr => cr.Candidate.Name)
                        .ToListAsync()
                    )
                    .GroupBy(cr => cr.RaceId)
                    .Select(cr => cr).ToList();

                model.UnlistedCandidates = await _context.Candidates
                    .Include(c => c.Organization)
                    .Where(c => c.ElectionId == _managedElectionID && c.CandidateRaces.Count == 0)
                    .OrderBy(c => c.Name)
                    .ToListAsync();
            }
            else if ("reverse-alphabet".Equals(orderBy))
            {
                model.CandidatesByRace = (
                    await _context.CandidateRaces
                        .Include(cr => cr.Race)
                        .Include(cr => cr.Candidate)
                        .Include(cr => cr.Candidate.Organization)
                        .Where(cr => cr.Candidate.ElectionId == _managedElectionID)
                        .OrderBy(cr => cr.RaceId).ThenByDescending(cr => cr.Candidate.Name)
                        .ToListAsync()
                    )
                    .GroupBy(cr => cr.RaceId)
                    .Select(cr => cr).ToList();

                model.UnlistedCandidates = await _context.Candidates
                    .Include(c => c.Organization)
                    .Where(c => c.ElectionId == _managedElectionID && c.CandidateRaces.Count == 0)
                    .OrderByDescending(c => c.Name)
                    .ToListAsync();
            }
            else
            {
                return NotFound();
            }

            return PartialView("CandidateList", model);
        }

        // GET: Candidates/Reorder/5
        public async Task<IActionResult> Reorder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _context.CandidateRaces
                .Include(cr => cr.Race)
                .Include(cr => cr.Candidate)
                .Where(cr => cr.RaceId == id)
                .OrderBy(cr => cr.BallotOrder)
                .ToListAsync());
        }

        // POST: Candidates/Reorder/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reorder(int id, IList<CandidateRace> candidateRaces)
        {
            if (candidateRaces.Count == 0 || id != candidateRaces.First().RaceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                IList<CandidateRace> existingRaces = await _context.CandidateRaces
                    .Where(cr => cr.RaceId == id)
                    .ToListAsync();

                foreach (var race in candidateRaces)
                {
                    var current = existingRaces.Where(existing => existing.CandidateRaceId == race.CandidateRaceId);

                    if (current != null && current.ToList().Count > 0)
                    {
                        current.First().BallotOrder = race.BallotOrder;
                        _context.Update(current.First());
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(candidateRaces);
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
                .Include(c => c.Contacts)
                .Include(c => c.Organization)
                .Include(c => c.CandidateRaces)
                .FirstOrDefaultAsync(m => m.CandidateId == id);

            if (candidate == null)
            {
                return NotFound();
            }

            ViewData["Races"] = await _context.Races
                .Where(r => r.ElectionId == _managedElectionID)
                .ToListAsync();

            return View(candidate);
        }

        // GET: Candidates/Create
        public IActionResult Create()
        {
            CandidateViewModel model = new CandidateViewModel
            {
                Candidate = new Candidate
                {
                    Details = new List<CandidateDetail>()
                    {
                        new CandidateDetail()
                    },
                    Contacts = new List<Contact>()
                    {
                        new Contact()
                    }
                },
                Organizations = new SelectList(_context.Organizations, "OrganizationId", "Name"),
                Races = new SelectList(_context.Races
                        .Where(r => r.ElectionId == _managedElectionID)
                        .OrderBy(r => r.BallotOrder),
                    "RaceId", "PositionName")
            };

            return View(model);
        }

        // POST: Candidates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CandidateViewModel model)
        {
            var wwwrootPath = "";
            var imagesPath = "";
            if (model.Image != null)
            {
                if (model.Image.ContentType != "image/jpeg"
                        && model.Image.ContentType != "image/png"
                        && model.Image.ContentType != "image/gif")
                {
                    ViewData["ImageMessage"] = _localizer["Invalid image type. Image must be a JPEG, GIF, or PNG."];
                    return View(model);
                }

                if (model.Image.Length < 4500)
                {
                    ViewData["ImageMessage"] = _localizer["Invalid image size. Image must be a minimum size of 5KB."];
                    return View(model);
                }

                imagesPath = "images\\" + Utility.GetCurrentDateTime + Path.GetFileName(model.Image.FileName);
                wwwrootPath = "wwwroot\\" + imagesPath;
                model.Image.CopyTo(new FileStream(wwwrootPath, FileMode.Create));
                model.Candidate.Picture = imagesPath;
            }
            else
            {
                model.Candidate.Picture = "images\\default.jpg";
            }

            // Remove the "deleted" details from the list
            if (model.RemovedDetails != null && model.RemovedDetails != "")
            {
                string[] itemsToRemove = model.RemovedDetails.Split(',', StringSplitOptions.RemoveEmptyEntries);
                int[] indexes = new int[itemsToRemove.Length];
                for (int i = 0; i < itemsToRemove.Length; ++i)
                {
                    indexes[i] = int.Parse(itemsToRemove[i]);
                }

                // sort in ascending order
                indexes = indexes.OrderBy(i => i).ToArray();

                for (int i = indexes.Length - 1; i >= 0; --i)
                {
                    model.Candidate.Details.RemoveAt(indexes[i]);
                }
            }

            // Remove the "deleted" contacts from the list
            if (model.RemovedContacts != null && model.RemovedContacts != "")
            {
                string[] contactsToRemove = model.RemovedContacts.Split(',', StringSplitOptions.RemoveEmptyEntries);
                int[] indexes = new int[contactsToRemove.Length];
                for (int i = 0; i < contactsToRemove.Length; ++i)
                {
                    indexes[i] = int.Parse(contactsToRemove[i]);
                }

                // sort in ascending order
                indexes = indexes.OrderBy(i => i).ToArray();

                for (int i = indexes.Length - 1; i >= 0; --i)
                {
                    model.Candidate.Contacts.RemoveAt(indexes[i]);
                }
            }

            List<CandidateRace> candidateRaces = null;

            if (model.RaceIds != null)
            {
                candidateRaces = new List<CandidateRace>();

                foreach (string raceid in model.RaceIds)
                {
                    int next = _context.CandidateRaces.Where(c => c.RaceId == int.Parse(raceid)).Count() + 1;

                    CandidateRace cr = new CandidateRace
                    {
                        CandidateId = model.Candidate.CandidateId,
                        RaceId = int.Parse(raceid),
                        BallotOrder = next
                    };
                    candidateRaces.Add(cr);
                }
            }

            model.Candidate.ElectionId = _managedElectionID;
            model.Candidate.CandidateRaces = candidateRaces;

            ModelState.Clear();

            if (TryValidateModel(model.Candidate))
            {
                _context.Add(model.Candidate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(model);
        }

        public virtual IActionResult GetDetailFields(int count)
        {
            List<CandidateDetail> list = new List<CandidateDetail>();

            for (int i = 0; i <= count; i++)
            {
                list.Add(new CandidateDetail());
            }

            CandidateViewModel model = new CandidateViewModel
            {
                Candidate = new Candidate
                {
                    Details = list
                }
            };

            return PartialView("CandidateDetail", model);
        }

        public virtual IActionResult GetContactFields(int count)
        {
            List<Contact> list = new List<Contact>();

            for (int i = 0; i <= count; i++)
            {
                list.Add(new Contact());
            }

            CandidateViewModel model = new CandidateViewModel
            {
                Candidate = new Candidate
                {
                    Contacts = list
                }
            };

            return PartialView("CandidateContact", model);
        }

        // GET: Candidates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidates
                .Include(c => c.CandidateRaces)
                .Include(c => c.Details)
                .Include(c => c.Contacts)
                .FirstOrDefaultAsync(c => c.CandidateId == id);

            if (candidate == null)
            {
                return NotFound();
            }

            CandidateViewModel model = new CandidateViewModel
            {
                Candidate = candidate,
                Organizations = new SelectList(_context.Organizations, "OrganizationId", "Name", candidate.OrganizationId),
                Races = new SelectList(_context.Races
                        .Where(r => r.ElectionId == _managedElectionID)
                        .OrderBy(r => r.BallotOrder),
                    "RaceId", "PositionName")
            };

            return View(model);
        }

        // POST: Candidates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CandidateViewModel model)
        {
            if (id != model.Candidate.CandidateId)
            {
                return NotFound();
            }

            // Remove the "deleted" details from the list
            if (model.RemovedDetails != null && model.RemovedDetails != "")
            {
                string[] itemsToRemove = model.RemovedDetails.Split(',', StringSplitOptions.RemoveEmptyEntries);
                int[] indexes = new int[itemsToRemove.Length];
                for (int i = 0; i < itemsToRemove.Length; ++i)
                {
                    indexes[i] = int.Parse(itemsToRemove[i]);
                }

                // sort in ascending order
                indexes = indexes.OrderBy(i => i).ToArray();

                for (int i = indexes.Length - 1; i >= 0; --i)
                {
                    model.Candidate.Details.RemoveAt(indexes[i]);
                }
            }

            // Remove the "deleted" contacts from the list
            if (model.RemovedContacts != null && model.RemovedContacts != "")
            {
                string[] contactsToRemove = model.RemovedContacts.Split(',', StringSplitOptions.RemoveEmptyEntries);
                int[] indexes = new int[contactsToRemove.Length];
                for (int i = 0; i < contactsToRemove.Length; ++i)
                {
                    indexes[i] = int.Parse(contactsToRemove[i]);
                }

                // sort in ascending order
                indexes = indexes.OrderBy(i => i).ToArray();

                for (int i = indexes.Length - 1; i >= 0; --i)
                {
                    model.Candidate.Contacts.RemoveAt(indexes[i]);
                }
            }

            List<CandidateRace> candidateRaces = null;

            if (model.RaceIds != null)
            {
                candidateRaces = new List<CandidateRace>();

                foreach (string raceid in model.RaceIds)
                {
                    int next = _context.CandidateRaces.Where(c => c.RaceId == int.Parse(raceid)).Count() + 1;

                    CandidateRace cr = new CandidateRace
                    {
                        CandidateId = model.Candidate.CandidateId,
                        RaceId = int.Parse(raceid),
                        BallotOrder = next
                    };
                    candidateRaces.Add(cr);
                }
            }

            model.Candidate.ElectionId = _managedElectionID;
            model.Candidate.CandidateRaces = candidateRaces;

            ModelState.Clear();

            if (TryValidateModel(model.Candidate))
            {
                if (model.Image != null)
                {
                    if (model.Image.ContentType != "image/jpeg"
                        && model.Image.ContentType != "image/png"
                        && model.Image.ContentType != "image/gif")
                    {
                        ViewData["ImageMessage"] = _localizer["Invalid image type. Image must be a JPEG, GIF, or PNG."];
                        return View(model);
                    }

                    if (model.Image.Length < 4500)
                    {
                        ViewData["ImageMessage"] = _localizer["Invalid image size. Image must be a minimum size of 5KB."];
                        return View(model);
                    }

                    string nameOfile = "images\\" + Utility.GetCurrentDateTime + Path.GetFileName(model.Image.FileName);
                    string fileName = "wwwroot\\" + nameOfile;
                    model.Image.CopyTo(new FileStream(fileName, FileMode.Create));
                    model.Candidate.Picture = nameOfile;
                }
                else
                {
                    var existing = _context.Candidates
                        .Where(c => c.CandidateId == id)
                        .Select(c => new { Pic = c.Picture });
                    foreach (var result in existing)
                    {
                        model.Candidate.Picture = result.Pic;
                    }
                }

                try
                {
                    // remove all the candidate's current details, contacts, and races because the update
                    // will recreate them
                    var existingDetails = _context.CandidateDetails.Where(cd => cd.CandidateId == id).ToList();
                    _context.RemoveRange(existingDetails);
                    var existingContacts = _context.Contacts.Where(cd => cd.CandidateId == id).ToList();
                    _context.RemoveRange(existingContacts);
                    var existingRaces = _context.CandidateRaces.Where(cr => cr.CandidateId == id).ToList();
                    _context.RemoveRange(existingRaces);

                    _context.Update(model.Candidate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidateExists(model.Candidate.CandidateId))
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
                .Include(c => c.Contacts)
                .Include(c => c.CandidateRaces)
                .FirstOrDefaultAsync(m => m.CandidateId == id);

            if (candidate == null)
            {
                return NotFound();
            }

            ViewData["Races"] = await _context.Races
                .Where(r => r.ElectionId == _managedElectionID)
                .ToListAsync();

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
