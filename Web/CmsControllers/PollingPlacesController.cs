using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;
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
            return View(new PollingPlaceGroup()
            {
                PollingPlaceDates = new List<DateTime>(),
                PollingStartTimes = new List<DateTime>(),
                PollingEndTimes = new List<DateTime>()
            });
        }

        // POST: PollingPlaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PollingPlaceGroup pollingPlaceGroup)
        {
            if (ModelState.IsValid)
            {
                PollingPlace pollingPlace = new PollingPlace
                {
                    ElectionId = _managedElectionID,
                    PollingPlaceName = pollingPlaceGroup.PollingPlaceName,
                    PollingStationName = pollingPlaceGroup.PollingStationName,
                    Address = pollingPlaceGroup.Address,
                    WheelchairInfo = pollingPlaceGroup.WheelchairInfo,
                    ParkingInfo = pollingPlaceGroup.ParkingInfo,
                    Latitude = pollingPlaceGroup.Latitude,
                    Longitude = pollingPlaceGroup.Longitude
                };
                _context.Add(pollingPlace);

                if (pollingPlaceGroup.PollingPlaceDates != null)
                {
                    for (var i = 0; i < pollingPlaceGroup.PollingPlaceDates.Count; ++i)
                    {
                        PollingPlaceDate pollingDate = new PollingPlaceDate
                        {
                            PollingPlaceId = pollingPlace.PollingPlaceId,
                            PollingDate = pollingPlaceGroup.PollingPlaceDates[i].Date,
                            StartTime = pollingPlaceGroup.PollingPlaceDates[i].Date.Add(pollingPlaceGroup.PollingStartTimes[i].TimeOfDay),
                            EndTime = pollingPlaceGroup.PollingPlaceDates[i].Date.Add(pollingPlaceGroup.PollingEndTimes[i].TimeOfDay)
                        };
                        _context.Add(pollingDate);
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pollingPlaceGroup);
        }

        // GET: PollingPlaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollingPlace = await _context.PollingPlaces.FindAsync(id);

            if (pollingPlace == null)
            {
                return NotFound();
            }

            var DateGroup = _context.PollingPlaceDates.Where(op => op.PollingPlaceId == id).ToList();
            List<DateTime> PollDates = DateGroup.Select(d => d.PollingDate).ToList();
            List<DateTime> StartTimes = DateGroup.Select(d => d.StartTime).ToList();
            List<DateTime> EndTimes = DateGroup.Select(d => d.EndTime).ToList();

            PollingPlaceGroup group = new PollingPlaceGroup
            {
                PollingPlaceName = pollingPlace.PollingPlaceName,
                PollingStationName = pollingPlace.PollingStationName,
                Address = pollingPlace.Address,
                WheelchairInfo = pollingPlace.WheelchairInfo,
                ParkingInfo = pollingPlace.ParkingInfo,
                Latitude = pollingPlace.Latitude,
                Longitude = pollingPlace.Longitude,
                AdvanceOnly = pollingPlace.AdvanceOnly,
                LocalArea = pollingPlace.LocalArea,
                Phone = pollingPlace.Phone,
                Email = pollingPlace.Email,
                PollingPlaceDates = PollDates,
                PollingStartTimes = StartTimes,
                PollingEndTimes = EndTimes
            };
            return View(group);
        }

        // POST: PollingPlaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("PollingPlaceName,PollingStationName,Address,WheelchairInfo,ParkingInfo,Latitude,Longitude,AdvanceOnly,LocalArea,Phone,Email,PollingPlaceDates,PollingStartTimes,PollingEndTimes")] PollingPlaceGroup group)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var pollingPlace = _context.PollingPlaces.Find(id);
                    pollingPlace.PollingPlaceName = group.PollingPlaceName;
                    pollingPlace.PollingStationName = group.PollingStationName;
                    pollingPlace.Address = group.Address;
                    pollingPlace.WheelchairInfo = group.WheelchairInfo;
                    pollingPlace.ParkingInfo = group.ParkingInfo;
                    pollingPlace.Latitude = group.Latitude;
                    pollingPlace.Longitude = group.Longitude;
                    pollingPlace.Longitude = group.Longitude;
                    pollingPlace.AdvanceOnly = group.AdvanceOnly;
                    pollingPlace.LocalArea = group.LocalArea;
                    pollingPlace.Phone = group.Phone;
                    pollingPlace.Email = group.Email;
                    _context.Update(pollingPlace);

                    if (group.PollingPlaceDates != null)
                    {
                        for (var i = 0; i < group.PollingPlaceDates.Count; i++)
                        {
                            var existing = _context.PollingPlaceDates
                                .Where(psd => psd.PollingPlaceId == id)
                                .ToList();
                            _context.RemoveRange(existing);

                            PollingPlaceDate psDate = new PollingPlaceDate
                            {
                                PollingPlaceId = pollingPlace.PollingPlaceId,
                                PollingDate = group.PollingPlaceDates[i].Date,
                                StartTime = group.PollingPlaceDates[i].Date.Add(group.PollingStartTimes[i].TimeOfDay),
                                EndTime = group.PollingPlaceDates[i].Date.Add(group.PollingEndTimes[i].TimeOfDay)
                            };
                            _context.Add(psDate);
                        }
                    }
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
            return View(group);
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
