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
    [Route("api/races")]
    [ApiController]
    public class RacesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private int _currentElection;

        public RacesApiController(ApplicationDbContext context)
        {
            _context = context;
            _currentElection = context.StateSingleton.Find(State.STATE_ID).currentElection;
        }

        // GET: api/Races
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Race>>> GetRaces()
        {
            return await _context.Races
                .Include(r => r.CandidateRaces)
                .Where(r=>r.ElectionId == _currentElection)
                .ToListAsync();
        }

        // GET: api/Races/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Race>> GetRace(int id)
        {
            var race = await _context.Races.FindAsync(id);

            if (race == null)
            {
                return NotFound();
            }

            return race;
        }

        private bool RaceExists(int id)
        {
            return _context.Races.Any(e => e.RaceId == id);
        }
    }
}
