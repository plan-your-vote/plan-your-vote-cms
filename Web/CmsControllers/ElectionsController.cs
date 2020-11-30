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
using Microsoft.Extensions.Localization;

namespace Web
{
    [Authorize(Roles = Constants.Account.ROLE_ADMIN)]
    public class ElectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IStringLocalizer<ElectionsController> _localizer;

        public ElectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Elections
        public async Task<IActionResult> Index()
        {
            return View(await _context.Elections.ToListAsync());
        }

        // GET: Elections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var election = await _context.Elections
                .FirstOrDefaultAsync(m => m.ElectionId == id);
            if (election == null)
            {
                return NotFound();
            }

            return View(election);
        }

        // GET: Elections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Elections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ElectionName,StartDate,EndDate,Description")] Election election)
        {
            if (ModelState.IsValid)
            {
                _context.Add(election);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(election);
        }

        // GET: Elections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var election = await _context.Elections.FindAsync(id);
            if (election == null)
            {
                return NotFound();
            }
            return View(election);
        }

        // POST: Elections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ElectionId,ElectionName,StartDate,EndDate,Description")] Election election)
        {
            if (id != election.ElectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(election);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElectionExists(election.ElectionId))
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
            return View(election);
        }

        // GET: Elections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var election = await _context.Elections
                .FirstOrDefaultAsync(m => m.ElectionId == id);
            if (election == null)
            {
                return NotFound();
            }

            return View(election);
        }

        // POST: Elections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var election = await _context.Elections.FindAsync(id);
            _context.Elections.Remove(election);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElectionExists(int id)
        {
            return _context.Elections.Any(e => e.ElectionId == id);
        }
    }
}
