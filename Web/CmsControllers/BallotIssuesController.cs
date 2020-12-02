using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using PlanYourVoteLibrary2;
using Web.ViewModels;

namespace Web
{
    [Authorize]
    public class BallotIssuesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private int _managedElectionID;

        public BallotIssuesController(ApplicationDbContext context)
        {
            _context = context;
            _managedElectionID = _context.StateSingleton.Find(State.STATE_ID).ManagedElectionID;
        }

        // GET: BallotIssues
        public async Task<IActionResult> Index()
        {
            return View(await _context.BallotIssues.Where(b => b.ElectionId == _managedElectionID).ToListAsync());
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
            return View(new BallotIssue
            {
                BallotIssueOptions = new List<IssueOption> {
                    new IssueOption() {
                        IssueOptionInfo = ""
                    },
                    new IssueOption() {
                        IssueOptionInfo = ""
                    }
                }
            });
        }

        // POST: BallotIssues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BallotIssue issue)
        {
            if (ModelState.IsValid)
            {
                issue.ElectionId = _managedElectionID;

                for (int i = issue.BallotIssueOptions.Count-1; i >= 0; i--)
                {
                    if (issue.BallotIssueOptions[i].IssueOptionInfo == null || issue.BallotIssueOptions[i].IssueOptionInfo == "")
                    {
                        issue.BallotIssueOptions.RemoveAt(i);
                    }
                }

                if (issue.BallotIssueOptions.Count < 2)
                {
                    ViewData["IssueOptionsError"] = "Please enter at least 2 options.";
                    return View(issue);
                }

                _context.Add(issue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(issue);
        }

        public virtual IActionResult GetOptionFields(int count)
        {
            List<IssueOption> options = new List<IssueOption>();

            for (int i = 0; i <= count; i++)
            {
                options.Add(new IssueOption());
            }

            BallotIssue ballotIssue = new BallotIssue
            {
                BallotIssueOptions = options
            };

            return PartialView("IssueOption", ballotIssue);
        }

        // GET: BallotIssues/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: BallotIssues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BallotIssue ballotIssue)
        {
            if (id != ballotIssue.BallotIssueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    for (int i = ballotIssue.BallotIssueOptions.Count - 1; i >= 0; i--)
                    {
                        if (ballotIssue.BallotIssueOptions[i].IssueOptionInfo == null || 
                            ballotIssue.BallotIssueOptions[i].IssueOptionInfo == "")
                        {
                            ballotIssue.BallotIssueOptions.RemoveAt(i);
                        }
                    }

                    if (ballotIssue.BallotIssueOptions.Count < 2)
                    {
                        ViewData["IssueOptionsError"] = "Please enter at least 2 options.";
                        return View(ballotIssue);
                    }

                    // remove the existing issue options
                    var existing = _context.IssueOptions.Where(op => op.BallotIssueId == id).ToList();
                    _context.RemoveRange(existing);

                    ballotIssue.ElectionId = _managedElectionID;
                    _context.Update(ballotIssue);
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
                .Include(b => b.BallotIssueOptions)
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
