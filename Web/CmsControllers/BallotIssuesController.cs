using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VotingModelLibrary.Models;
using Web.Data;
using Web.Models;

namespace Web
{
    public class BallotIssuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BallotIssuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BallotIssues
        public async Task<IActionResult> Index()
        {
            return View(await _context.BallotIssues.ToListAsync());
        }

        // GET: BallotIssues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ballotIssue = await _context.BallotIssues
                .Include(b => b.BallotIssueOptions)
                .FirstOrDefaultAsync(m => m.BallotIssueId == id);
            if (ballotIssue == null)
            {
                return NotFound();
            }

            return View(ballotIssue);
        }

        // GET: BallotIssues/Create
        public IActionResult Create()
        { 
            return View();
        }

        // POST: BallotIssues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BallotIssueId,BallotIssueTitle,Description,OptionsTitles")] BallotIssueCreate model)
        {
            if (ModelState.IsValid)
            {
                BallotIssue issue = new BallotIssue
                {
                    BallotIssueTitle = model.BallotIssueTitle,
                    Description = model.Description
                };
                _context.Add(issue);

                foreach(string title in model.OptionsTitles)
                {
                    if(title != null && title.Length > 0)
                    {
                        IssueOption opt = new IssueOption
                        {
                            BallotIssueId = issue.BallotIssueId,
                            IssueOptionTitle = title,
                            IssueOptionInfo = title,
                        };
                        _context.Add(opt);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: BallotIssues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ballotIssue = await _context.BallotIssues.FindAsync(id);
            if (ballotIssue == null)
            {
                return NotFound();
            }
            return View(ballotIssue);
        }

        // POST: BallotIssues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BallotIssueId,BallotIssueTitle,Description")] BallotIssue ballotIssue)
        {
            if (id != ballotIssue.BallotIssueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ballotIssue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BallotIssueExists(ballotIssue.BallotIssueId))
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
            return View(ballotIssue);
        }

        // GET: BallotIssues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ballotIssue = await _context.BallotIssues
                .FirstOrDefaultAsync(m => m.BallotIssueId == id);
            if (ballotIssue == null)
            {
                return NotFound();
            }

            return View(ballotIssue);
        }

        // POST: BallotIssues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ballotIssue = await _context.BallotIssues.FindAsync(id);
            _context.BallotIssues.Remove(ballotIssue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BallotIssueExists(int id)
        {
            return _context.BallotIssues.Any(e => e.BallotIssueId == id);
        }
    }
}
