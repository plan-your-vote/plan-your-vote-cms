using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Data;
using Web.ApiDTO;

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
            _currentElection = _context.StateSingleton.Find(State.STATE_ID).CurrentElection;
        }

        // GET: api/BallotIssues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BallotIssue>>> Get()
        {
            VotingPage votingPage = new VotingPage()
            {
                PageTitle = "REVIEW CAPITAL PLAN BORROWING QUESTIONS",
                PageDescription = @"Add your response to the Capital Plan borrowing questions to your plan. \n The ballot will have 3 ""yes"" or ""no"" questions on whether the City can borrow $300 million to help pay for projects in the Capital Plan. \n The 2019-2022 Capital Plan invests $300,000,000 in City facilities and infrastructure to provide services to the people of Vancouver. \n If a majority of voters vote yes, then City Council can borrow the funds for these projects.",
                PageNumber = 2,
            };

            var ballotIssues = await _context.BallotIssues
                .Include(b => b.BallotIssueOptions)
                .Where(b => b.ElectionId == _currentElection)
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
