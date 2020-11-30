using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Data;
using Microsoft.AspNetCore.Authorization;
using Web.ViewModels;
using Microsoft.Extensions.Localization;

namespace Web.CmsControllers
{
    public class RacesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly int _managedElectionID;
        private readonly IStringLocalizer<RacesController> _localizer;


        public RacesController(ApplicationDbContext context)
        {
            _context = context;
            _managedElectionID = _context.StateSingleton.Find(State.STATE_ID).ManagedElectionID;
        }

        // GET: Races
        public async Task<IActionResult> Index()
        {
            return View(await _context.Races
                .Where(r => r.ElectionId == _managedElectionID)
                .OrderBy(r => r.BallotOrder)
                .ToListAsync());
        }

        // GET: Races/Reorder
        public async Task<IActionResult> Reorder()
        {
            return View(await _context.Races
                    .Where(r => r.ElectionId == _managedElectionID)
                    .OrderBy(r => r.BallotOrder)
                    .ToListAsync());
        }

        // POST: Races/Reorder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reorder(IList<Race> races)
        {
            if (ModelState.IsValid)
            {
                IList<Race> existingRaces = await _context.Races
                .Where(r => r.ElectionId == _managedElectionID)
                .OrderBy(r => r.BallotOrder)
                .ToListAsync();

                foreach (var race in races)
                {
                    var current = existingRaces.Where(r => r.RaceId == race.RaceId);
                    if (current != null && current.ToList().Count > 0)
                    {
                        current.First().BallotOrder = race.BallotOrder;
                        _context.Update(current.First());
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(races);
        }

        // GET: Races/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var race = await _context.Races
                .Include(r => r.Election)
                .Include(r => r.CandidateRaces)
                .FirstOrDefaultAsync(m => m.RaceId == id);

            if (race == null)
            {
                return NotFound();
            }

            ViewData["Candidates"] = await _context.Candidates
                .Where(c => c.ElectionId == _managedElectionID)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return View(race);
        }

        // GET: Races/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Races/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RaceId,PositionName,Description,NumberNeeded")] Race race)
        {
            int next = _context.Races.Where(r => r.ElectionId == _managedElectionID).Count() + 1;
            race.BallotOrder = next;
            race.ElectionId = _managedElectionID;

            ModelState.Clear();

            if (TryValidateModel(race))
            {
                _context.Add(race);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(race);
        }

        // GET: Races/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var race = await _context.Races
                .Include(r => r.CandidateRaces)
                .FirstOrDefaultAsync(r => r.RaceId == id);

            RaceViewModel model = new RaceViewModel
            {
                Race = race,
                Candidates = new SelectList(_context.Candidates
                        .Where(c => c.ElectionId == _managedElectionID)
                        .OrderBy(c => c.Name), 
                    "CandidateId", "Name")
            };

            if (race == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Races/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RaceViewModel model)
        {
            if (id != model.Race.RaceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model.Race);

                    var crs = _context.CandidateRaces.Where(cr => cr.RaceId == id).ToList();
                    _context.RemoveRange(crs);

                    if (model.CandidateIds != null)
                    {
                        foreach (string cr in model.CandidateIds)
                            _context.Add(new CandidateRace
                            {
                                CandidateId = int.Parse(cr),
                                RaceId = id,
                            });
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RaceExists(model.Race.RaceId))
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

            return View(model);
        }

        // GET: Races/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var race = await _context.Races
                .Include(r => r.Election)
                .Include(r => r.CandidateRaces)
                .FirstOrDefaultAsync(m => m.RaceId == id);

            if (race == null)
            {
                return NotFound();
            }

            ViewData["Candidates"] = await _context.Candidates
                .Where(c => c.ElectionId == _managedElectionID)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return View(race);
        }

        // POST: Races/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var race = await _context.Races.FindAsync(id);
            _context.Races.Remove(race);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RaceExists(int id)
        {
            return _context.Races.Any(e => e.RaceId == id);
        }
    }
}