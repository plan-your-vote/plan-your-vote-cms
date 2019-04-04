using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VotingModelLibrary.Models;
using Web.Data;

namespace Web.CmsControllers
{
    [Authorize]
    public class StateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StateController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: State
        public async Task<IActionResult> Index()
        {
            State s = _context.StateSingleton.Find(State.STATE_ID);
            Election current = _context.Elections.Where(e => e.ElectionId == s.currentElection).First();
            ViewBag.ElectionName = current.Name;
            return View(await _context.StateSingleton.ToListAsync());
        }

        // GET: State/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _context.StateSingleton.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }
            ViewData["Elections"] = new SelectList(_context.Elections, "ElectionId", "Name");
            return View(state);
        }

        // POST: State/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StateId,currentElection")] State state)
        {
            if (id != state.StateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(state);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StateExists(state.StateId))
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
            return View(state);
        }

        private bool StateExists(int id)
        {
            return _context.StateSingleton.Any(e => e.StateId == id);
        }
    }
}
