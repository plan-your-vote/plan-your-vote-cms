using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VotingModelLibrary.Models;
using Web.Data;

namespace Web
{
    public class PollingStationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PollingStationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PollingStations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PollingStations.Include(p => p.Election);
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
            ViewData["ElectionId"] = new SelectList(_context.Elections, "ElectionId", "ElectionId");
            return View();
        }

        // POST: PollingStations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PollingStationId,ElectionId,Name,AdditionalInfo,Address,WheelchairInfo,ParkingInfo,WashroomInfo,GeneralAccessInfo,Latitude,Longitute")] PollingStation pollingStation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pollingStation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ElectionId"] = new SelectList(_context.Elections, "ElectionId", "ElectionId", pollingStation.ElectionId);
            return View(pollingStation);
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
            ViewData["ElectionId"] = new SelectList(_context.Elections, "ElectionId", "ElectionId", pollingStation.ElectionId);
            return View(pollingStation);
        }

        // POST: PollingStations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PollingStationId,ElectionId,Name,AdditionalInfo,Address,WheelchairInfo,ParkingInfo,WashroomInfo,GeneralAccessInfo,Latitude,Longitute")] PollingStation pollingStation)
        {
            if (id != pollingStation.PollingStationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pollingStation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PollingStationExists(pollingStation.PollingStationId))
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
            ViewData["ElectionId"] = new SelectList(_context.Elections, "ElectionId", "ElectionId", pollingStation.ElectionId);
            return View(pollingStation);
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

        private bool PollingStationExists(int id)
        {
            return _context.PollingStations.Any(e => e.PollingStationId == id);
        }
    }
}
