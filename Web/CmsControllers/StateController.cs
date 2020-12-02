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

namespace Web.CmsControllers
{
    [Authorize]
    public class StateController : Controller
    {
        private static ApplicationDbContext _context;
        private int _managedElectionID;

        public StateController(ApplicationDbContext context)
        {
            _context = context;

            _managedElectionID = _context.StateSingleton.AsNoTracking().First().ManagedElectionID;
        }

        // GET: State
        public async Task<IActionResult> Index()
        {
            State state = await _context.StateSingleton
                .Include(s => s.RunningElection)
                .Include(s => s.ManagedElection)
                .SingleAsync()
                .ConfigureAwait(false);

            return View(state);
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
            ViewData["Elections"] = new SelectList(_context.Elections, "ElectionId", "ElectionName");
            return View(state);
        }

        // POST: State/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = Constants.Account.ROLE_ADMIN)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StateId,RunningElectionID,ManagedElectionID")] State state)
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
