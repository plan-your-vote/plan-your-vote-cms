using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;
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
            return View(new BallotIssueViewModel()
            {
                OptionsTitles = new List<string>
                {
                    "", "",
                }
            });
        }

        // POST: BallotIssues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BallotIssueViewModel model)
        {
            if (ModelState.IsValid)
            {
                BallotIssue issue = new BallotIssue
                {
                    BallotIssueTitle = model.BallotIssueTitle,
                    Description = model.Description,
                    ElectionId = _managedElectionID,
                };
                _context.Add(issue);

                foreach (string title in model.OptionsTitles)
                {
                    if (title != null && title.Length > 0)
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
            List<string> optionTitles = options.Select(optionTitle => optionTitle.IssueOptionTitle).ToList();

            BallotIssueViewModel model = new BallotIssueViewModel
            {
                BallotIssueTitle = ballotIssue.BallotIssueTitle,
                Description = ballotIssue.Description,
                OptionsTitles = optionTitles,
            };

            return View(model);
        }

        // POST: BallotIssues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BallotIssueTitle,Description,OptionsTitles")] BallotIssueViewModel model)
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
