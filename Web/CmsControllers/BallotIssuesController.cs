using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VotingModelLibrary.Models;
using Web.Data;
using Web.Models;

namespace Web
{
    [Authorize]
    public class BallotIssuesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private State state;

        public BallotIssuesController(ApplicationDbContext context)
        {
            _context = context;
            state = context.StateSingleton.Find(State.STATE_ID);
        }

        // GET: BallotIssues
        public async Task<IActionResult> Index()
        {
            return View(await _context.BallotIssues.Where(b => b.ElectionId == state.currentElection).ToListAsync());
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
        public async Task<IActionResult> Create(BallotIssueCreate model)
        {
            if (ModelState.IsValid)
            {
                BallotIssue issue = new BallotIssue
                {
                    BallotIssueTitle = model.BallotIssueTitle,
                    Description = model.Description,
                    ElectionId = _context.StateSingleton.Find(State.STATE_ID).currentElection
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

            var options = _context.IssueOptions.Where(op => op.BallotIssueId == id).ToList();

            BallotIssueCreate model = new BallotIssueCreate
            {
                BallotIssueTitle = ballotIssue.BallotIssueTitle,
                Description = ballotIssue.Description,
                OptionsTitles = new List<string> { "", "", "", "" }
            };

            if (options != null)
            {
                int cnt = 0;
                foreach (var op in options)
                    model.OptionsTitles.Add(options[cnt++].IssueOptionTitle);
            }

            return View(model);
        }

        // POST: BallotIssues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BallotIssueTitle,Description,OptionsTitles")] BallotIssueCreate model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var bi = _context.BallotIssues.Find(id);
                    bi.BallotIssueTitle = model.BallotIssueTitle;
                    bi.Description = model.Description;
                    _context.Update(bi);

                    foreach (string title in model.OptionsTitles)
                    {
                        if (title != null && title.Length > 0)
                        {
                            var existing = _context.IssueOptions.Where(op => op.BallotIssueId == id).ToList();
                            _context.RemoveRange(existing);

                            IssueOption opt = new IssueOption
                            {
                                BallotIssueId = id,
                                IssueOptionTitle = title,
                                IssueOptionInfo = title,
                            };
                            _context.Add(opt);
                        }
                    }
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!BallotIssueExists(id))
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
