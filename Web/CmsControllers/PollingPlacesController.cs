using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlanYourVoteLibrary2;
using Web.Data;

namespace Web
{
    [Authorize]
    public class PollingPlacesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly int _managedElectionID;

        public PollingPlacesController(ApplicationDbContext context)
        {
            _context = context;
            _managedElectionID = _context.StateSingleton.Find(State.STATE_ID).ManagedElectionID;
        }

        // GET: PollingPlaces
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PollingPlaces
                .Include(p => p.Election)
                .Where(ps => ps.ElectionId == _managedElectionID);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PollingPlaces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollingPlace = await _context.PollingPlaces
                .Include(p => p.Election)
                .Include(p => p.PollingPlaceDates)
                .FirstOrDefaultAsync(m => m.PollingPlaceId == id);
            if (pollingPlace == null)
            {
                return NotFound();
            }

            return View(pollingPlace);
        }

        // GET: PollingPlaces/Create
        public IActionResult Create()
        {
            return View(new PollingPlace
            {
                PollingPlaceDates = new List<PollingPlaceDate>
                {
                    // Add one date to populate the partial view
                    new PollingPlaceDate
                    {
                        PollingDate = DateTime.Now
                    }
                }
            });
        }

        // POST: PollingPlaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PollingPlace pollingPlace, string removedDates)
        {
            pollingPlace.ElectionId = _managedElectionID;

            if (removedDates != null && !removedDates.Equals(""))
            {
                string[] itemsToRemove = removedDates.Split(',', StringSplitOptions.RemoveEmptyEntries);
                int[] indexes = new int[itemsToRemove.Length];
                for (int i = 0; i < itemsToRemove.Length; ++i)
                {
                    indexes[i] = int.Parse(itemsToRemove[i]);
                }

                // sort in ascending order
                indexes = indexes.OrderBy(i => i).ToArray();

                for (int i = indexes.Length - 1; i >= 0; --i)
                {
                    pollingPlace.PollingPlaceDates.RemoveAt(indexes[i]);
                }
            }

            if (ModelState.IsValid)
            {
                foreach (var date in pollingPlace.PollingPlaceDates)
                {
                    date.StartTime = date.PollingDate.Add(date.StartTime.TimeOfDay);
                    date.EndTime = date.PollingDate.Add(date.EndTime.TimeOfDay);
                }

                _context.Add(pollingPlace);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pollingPlace);
        }

        public virtual IActionResult GetDateFields(int count)
        {
            List<PollingPlaceDate> dates = new List<PollingPlaceDate>();

            for (int i = 0; i <= count; i++)
            {
                dates.Add(new PollingPlaceDate {
                    PollingDate = DateTime.Now
                });
            }

            PollingPlace pollingPlace = new PollingPlace
            {
                PollingPlaceDates = dates
            };

            return PartialView("PollingDate", pollingPlace);
        }

        // GET: PollingPlaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollingPlace = await _context.PollingPlaces
                .Include(p => p.PollingPlaceDates)
                .FirstOrDefaultAsync(p => p.PollingPlaceId == id);

            if (pollingPlace == null)
            {
                return NotFound();
            }

            return View(pollingPlace);
        }

        // POST: PollingPlaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PollingPlace pollingPlace, string removedDates)
        {
            pollingPlace.ElectionId = _managedElectionID;

            if (removedDates != null && !removedDates.Equals(""))
            {
                string[] itemsToRemove = removedDates.Split(',', StringSplitOptions.RemoveEmptyEntries);
                int[] indexes = new int[itemsToRemove.Length];
                for (int i = 0; i < itemsToRemove.Length; ++i)
                {
                    indexes[i] = int.Parse(itemsToRemove[i]);
                }

                // sort in ascending order
                indexes = indexes.OrderBy(i => i).ToArray();

                for (int i = indexes.Length - 1; i >= 0; --i)
                {
                    pollingPlace.PollingPlaceDates.RemoveAt(indexes[i]);
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (pollingPlace.PollingPlaceDates != null)
                    {
                        // remove the existing dates from the database so they do not get duplicated
                        var existing = await _context.PollingPlaceDates
                            .Where(pollDate => pollDate.PollingPlaceId == id)
                            .ToListAsync();
                        _context.RemoveRange(existing);

                        foreach (var date in pollingPlace.PollingPlaceDates)
                        {
                            date.StartTime = date.PollingDate.Add(date.StartTime.TimeOfDay);
                            date.EndTime = date.PollingDate.Add(date.EndTime.TimeOfDay);
                        }
                    }

                    _context.Update(pollingPlace);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PollingPlaceExists(id))
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
            return View(pollingPlace);
        }

        // GET: PollingPlaces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollingPlace = await _context.PollingPlaces
                .Include(p => p.Election)
                .Include(p => p.PollingPlaceDates)
                .FirstOrDefaultAsync(m => m.PollingPlaceId == id);
            if (pollingPlace == null)
            {
                return NotFound();
            }

            return View(pollingPlace);
        }

        // POST: PollingPlaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pollingPlace = await _context.PollingPlaces.FindAsync(id);
            _context.PollingPlaces.Remove(pollingPlace);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyStartTime(List<DateTime> PollingPlaceDates, List<DateTime> PollingStartTimes)
        {
            if (PollingPlaceDates.Count != PollingStartTimes.Count)
            {
                return Json(data: "Please enter a start time for each poll date.");
            }

            return Json(data: true);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyEndTime(List<DateTime> PollingPlaceDates, List<DateTime> PollingEndTimes)
        {
            if (PollingPlaceDates.Count != PollingEndTimes.Count)
            {
                return Json(data: "Please enter an end time for each poll date.");
            }

            return Json(data: true);
        }

        private bool PollingPlaceExists(int id)
        {
            return _context.PollingPlaces.Any(e => e.PollingPlaceId == id);
        }
    }
}
