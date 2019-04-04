using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VotingModelLibrary.Models;
using Web.Data;

namespace Web.ApiControllers
{
    [Route("api/election")]
    [ApiController]
    public class ElectionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly int _election;   

        public ElectionsController(ApplicationDbContext context)
        {
            _context = context;
            _election = context.StateSingleton.Find(State.STATE_ID).currentElection;
        }

        // GET: api/Election
        [HttpGet]
        public async Task<ActionResult<Election>> GetElections()
        {
            return await _context.Elections.FindAsync(_election);
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
