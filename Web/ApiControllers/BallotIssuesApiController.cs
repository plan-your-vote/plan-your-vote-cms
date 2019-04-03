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
    [Route("api/ballotissues")]
    [ApiController]
    public class BallotIssuesApiController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private int _currentElection;

        public BallotIssuesApiController(ApplicationDbContext context)
        {
            _context = context;
            _currentElection = _context.StateSingleton.Find(State.STATE_ID).currentElection;
        }

        // GET: api/BallotIssues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BallotIssue>>> Get()
        {
            return await _context.BallotIssues
                .Include(b => b.BallotIssueOptions)
                .Where(b => b.ElectionId == _currentElection)
                .ToListAsync();
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

        private bool IssueExists(int id)
        {
            return _context.BallotIssues.Any(e => e.BallotIssueId == id);
        }

    }
}
