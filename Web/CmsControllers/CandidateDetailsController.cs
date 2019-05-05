using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.CmsControllers
{
    public class CandidateDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CandidateDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CandidateDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.CandidateDetails.ToListAsync());
        }

        // GET: CandidateDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidateDetail = await _context.CandidateDetails
                .FirstOrDefaultAsync(m => m.ID == id);
            if (candidateDetail == null)
            {
                return NotFound();
            }

            return View(candidateDetail);
        }

        // GET: CandidateDetails/Create
        public IActionResult Create()
        {
            ViewData["CandidateIds"] = new SelectList(_context.Candidates, "CandidateId", "Name");
            return View();
        }

        // POST: CandidateDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CandidateId,Title,Text,Format,Lang")] CandidateDetail candidateDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(candidateDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CandidateIds"] = new SelectList(_context.Candidates, "CandidateId", "Name", candidateDetail.CandidateId);
            return View(candidateDetail);
        }

        // GET: CandidateDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidateDetail = await _context.CandidateDetails.FindAsync(id);
            if (candidateDetail == null)
            {
                return NotFound();
            }

            ViewData["CandidateIds"] = new SelectList(_context.Candidates, "CandidateId", "Name", candidateDetail.CandidateId);
            return View(candidateDetail);
        }

        // POST: CandidateDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CandidateId,Title,Text,Format,Lang")] CandidateDetail candidateDetail)
        {
            if (id != candidateDetail.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidateDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidateDetailExists(candidateDetail.ID))
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

            ViewData["CandidateIds"] = new SelectList(_context.Candidates, "CandidateId", "Name", candidateDetail.CandidateId);
            return View(candidateDetail);
        }

        // GET: CandidateDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidateDetail = await _context.CandidateDetails
                .FirstOrDefaultAsync(m => m.ID == id);
            if (candidateDetail == null)
            {
                return NotFound();
            }

            return View(candidateDetail);
        }

        // POST: CandidateDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var candidateDetail = await _context.CandidateDetails.FindAsync(id);
            _context.CandidateDetails.Remove(candidateDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidateDetailExists(int id)
        {
            return _context.CandidateDetails.Any(e => e.ID == id);
        }
    }
}
