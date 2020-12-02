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

namespace Web.Controllers
{
    [Route("api/candidates")]
    [ApiController]
    public class CandidatesApiController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private int _runningElection;

        public CandidatesApiController(ApplicationDbContext context)
        {
            _context = context;
            _runningElection = _context.StateSingleton.Find(State.STATE_ID).RunningElectionID;
        }

        // GET: api/Candidates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidate>>> Get()
        {
            return await _context.Candidates.Where(b => b.ElectionId == _runningElection).ToListAsync();
        }

        // GET: api/Candidates/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Candidate>> GetCandidate(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id);

            if (candidate == null)
            {
                return NotFound();
            }

            return candidate;
        }

        private bool IssueExists(int id)
        {
            return _context.Candidates.Any(e => e.CandidateId == id);
        }

    }
}
