using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanYourVoteLibrary2;
using Web.Data;
using Web.ApiDTO;

namespace Web.Controllers
{
    [Route("api/ballotissues")]
    [ApiController]
    public class BallotIssuesApiController : ControllerBase
    {
        public const int STEP_NUMBER = 2; // Hard-coded

        private readonly ApplicationDbContext _context;
        private readonly int _runningElectionID;

        public BallotIssuesApiController(ApplicationDbContext context)
        {
            _context = context;
            _runningElectionID = _context.StateSingleton.Find(State.STATE_ID).RunningElectionID;
        }

        // GET: api/BallotIssues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BallotIssue>>> Get()
        {
            var step2 = _context.Steps.Where(step => step.StepNumber == STEP_NUMBER).First();

            VotingPage votingPage = new VotingPage()
            {
                PageTitle = step2.StepTitle,
                PageDescription = step2.StepDescription,
                PageNumber = STEP_NUMBER,
            };

            var ballotIssues = await _context.BallotIssues
                .Include(b => b.BallotIssueOptions)
                .Where(b => b.ElectionId == _runningElectionID)
                .ToListAsync();

            return Ok(new
            {
                votingPage,
                ballotIssues
            });
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
