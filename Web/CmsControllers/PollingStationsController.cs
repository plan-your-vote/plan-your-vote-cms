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
    public class PollingStationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly int _electionId;

        public PollingStationsController(ApplicationDbContext context)
        {
            _context = context;
            _electionId = _context.StateSingleton.Find(State.STATE_ID).CurrentElection;
        }

        // GET: PollingStations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PollingStations
                .Include(p => p.Election)
                .Where(ps => ps.ElectionId == _electionId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PollingStations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollingStation = await _context.PollingStations
                .Include(p => p.Election)
                .Include(p => p.PollingStationDates)
                .FirstOrDefaultAsync(m => m.PollingStationId == id);
            if (pollingStation == null)
            {
                return NotFound();
            }

            return View(pollingStation);
        }

        // GET: PollingStations/Create
        public IActionResult Create()
        {
            return View(new PollingStationGroup()
            {
                PollingStationDates = new List<DateTime>(),
                PollingStartTimes = new List<DateTime>(),
                PollingEndTimes = new List<DateTime>()
            });
        }

        // POST: PollingStations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PollingStationGroup pollingStationGroup)
        {
            if (ModelState.IsValid)
            {
                PollingStation pollingStation = new PollingStation
                {
                    ElectionId = _electionId,
                    Name = pollingStationGroup.PollingStationName,
                    AdditionalInfo = pollingStationGroup.AdditionalInfo,
                    Address = pollingStationGroup.Address,
                    WheelchairInfo = pollingStationGroup.WheelchairInfo,
                    ParkingInfo = pollingStationGroup.ParkingInfo,
                    WashroomInfo = pollingStationGroup.WashroomInfo,
                    GeneralAccessInfo = pollingStationGroup.GeneralAccessInfo,
                    Latitude = pollingStationGroup.Latitude,
                    Longitude = pollingStationGroup.Longitude
                };
                _context.Add(pollingStation);

                if (pollingStationGroup.PollingStationDates != null)
                {
                    for (var i = 0; i < pollingStationGroup.PollingStationDates.Count; ++i)
                    {
                        PollingStationDate pollingDate = new PollingStationDate
                        {
                            PollingStationId = pollingStation.PollingStationId,
                            PollingDate = pollingStationGroup.PollingStationDates[i].Date,
                            StartTime = pollingStationGroup.PollingStationDates[i].Date.Add(pollingStationGroup.PollingStartTimes[i].TimeOfDay),
                            EndTime = pollingStationGroup.PollingStationDates[i].Date.Add(pollingStationGroup.PollingEndTimes[i].TimeOfDay)
                        };
                        _context.Add(pollingDate);
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pollingStationGroup);
        }

        // GET: PollingStations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollingStation = await _context.PollingStations.FindAsync(id);

            if (pollingStation == null)
            {
                return NotFound();
            }

            var DateGroup = _context.PollingStationDates.Where(op => op.PollingStationId == id).ToList();
            List<DateTime> PollDates = DateGroup.Select(d => d.PollingDate).ToList();
            List<DateTime> StartTimes = DateGroup.Select(d => d.StartTime).ToList();
            List<DateTime> EndTimes = DateGroup.Select(d => d.EndTime).ToList();

            PollingStationGroup group = new PollingStationGroup
            {
                PollingStationName = pollingStation.Name,
                AdditionalInfo = pollingStation.AdditionalInfo,
                Address = pollingStation.Address,
                WheelchairInfo = pollingStation.WheelchairInfo,
                ParkingInfo = pollingStation.ParkingInfo,
                WashroomInfo = pollingStation.WashroomInfo,
                GeneralAccessInfo = pollingStation.GeneralAccessInfo,
                Latitude = pollingStation.Latitude,
                Longitude = pollingStation.Longitude,
                PollingStationDates = PollDates,
                PollingStartTimes = StartTimes,
                PollingEndTimes = EndTimes
            };
            return View(group);
        }

        // POST: PollingStations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("PollingStationName,AdditionalInfo,Address,WheelchairInfo,ParkingInfo,WashroomInfo,GeneralAccessInfo,Latitude,Longitude,PollingStationDates,PollingStartTimes,PollingEndTimes")] PollingStationGroup group)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var pollingStation = _context.PollingStations.Find(id);
                    pollingStation.Name = group.PollingStationName;
                    pollingStation.AdditionalInfo = group.AdditionalInfo;
                    pollingStation.Address = group.Address;
                    pollingStation.WheelchairInfo = group.WheelchairInfo;
                    pollingStation.ParkingInfo = group.ParkingInfo;
                    pollingStation.WashroomInfo = group.WashroomInfo;
                    pollingStation.GeneralAccessInfo = group.GeneralAccessInfo;
                    pollingStation.Latitude = group.Latitude;
                    pollingStation.Longitude = group.Longitude;
                    _context.Update(pollingStation);

                    if (group.PollingStationDates != null)
                    {
                        for (var i = 0; i < group.PollingStationDates.Count; i++)
                        {
                            var existing = _context.PollingStationDates
                                .Where(psd => psd.PollingStationId == id)
                                .ToList();
                            _context.RemoveRange(existing);

                            PollingStationDate psDate = new PollingStationDate
                            {
                                PollingStationId = pollingStation.PollingStationId,
                                PollingDate = group.PollingStationDates[i].Date,
                                StartTime = group.PollingStationDates[i].Date.Add(group.PollingStartTimes[i].TimeOfDay),
                                EndTime = group.PollingStationDates[i].Date.Add(group.PollingEndTimes[i].TimeOfDay)
                            };
                            _context.Add(psDate);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PollingStationExists(id))
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

        // GET: PollingStations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollingStation = await _context.PollingStations
                .Include(p => p.Election)
                .FirstOrDefaultAsync(m => m.PollingStationId == id);
            if (pollingStation == null)
            {
                return NotFound();
            }

            return View(pollingStation);
        }

        // POST: PollingStations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pollingStation = await _context.PollingStations.FindAsync(id);
            _context.PollingStations.Remove(pollingStation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyStartTime(List<DateTime> PollingStationDates, List<DateTime> PollingStartTimes)
        {
            if (PollingStationDates.Count != PollingStartTimes.Count)
            {
                return Json(data: "Please enter a start time for each poll date.");
            }

            return Json(data: true);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyEndTime(List<DateTime> PollingStationDates, List<DateTime> PollingEndTimes)
        {
            if (PollingStationDates.Count != PollingEndTimes.Count)
            {
                return Json(data: "Please enter an end time for each poll date.");
            }

            return Json(data: true);
        }

        private bool PollingStationExists(int id)
        {
            return _context.PollingStations.Any(e => e.PollingStationId == id);
        }
    }
}
