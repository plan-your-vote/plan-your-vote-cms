using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VotingModelLibrary.Models;
using Web.Data;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BallotIssuesController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public BallotIssuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BallotIssues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BallotIssue>>> Get()
        {
            return await _context.BallotIssues.ToListAsync();
        }

        // GET: api/BallotIssues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BallotIssue>> GetBallotIssue(int id)
        {
            var issue = await _context.BallotIssues.FindAsync(id);

            if (issue == null)
            {
                return NotFound();
            }

            return issue;
        }

        // POST: api/BallotIssues
        [HttpPost]
        public async Task<ActionResult<BallotIssue>> PostBallotIssue(BallotIssue issue)
        {
            _context.BallotIssues.Add(issue);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBallotIssue", new { id = issue.BallotIssueId}, issue);
        }

        // PUT: api/BallotIssues/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBallotIssue(int id, BallotIssue issue)
        {
            if (id != issue.BallotIssueId)
            {
                return BadRequest();
            }

            _context.Entry(issue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IssueExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/BallotIssues/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BallotIssue>> DeleteBallotIssue(int id)
        {
            var issue = await _context.BallotIssues.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }

            _context.BallotIssues.Remove(issue);
            await _context.SaveChangesAsync();

            return issue;
        }

        private bool IssueExists(int id)
        {
            return _context.BallotIssues.Any(e => e.BallotIssueId == id);
        }

    }
}
