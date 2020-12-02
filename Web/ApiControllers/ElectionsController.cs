using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanYourVoteLibrary2;
using Web.Data;

namespace Web.ApiControllers
{
    [Route("api/election")]
    [ApiController]
    public class ElectionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly int _runningElectionID;

        public ElectionsController(ApplicationDbContext context)
        {
            _context = context;
            _runningElectionID = context.StateSingleton.Find(State.STATE_ID).RunningElectionID;
        }

        // GET: api/Election
        [HttpGet]
        public async Task<ActionResult<Election>> GetElections()
        {
            return await _context.Elections.FindAsync(_runningElectionID);
        }

        // GET: api/Election/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Election>> GetElection(int id)
        {
            var election = await _context.Elections.FindAsync(id);

            if (election == null)
            {
                return NotFound();
            }

            return election;
        }

        private bool ElectionExists(int id)
        {
            return _context.Elections.Any(e => e.ElectionId == id);
        }
    }
}
