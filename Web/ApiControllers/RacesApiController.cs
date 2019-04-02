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
    [Route("api/Races")]
    [ApiController]
    public class RacesApiController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private int _currentElection;

        public RacesApiController(ApplicationDbContext context)
        {
            _context = context;
            _currentElection = _context.StateSingleton.Find(State.STATE_ID).currentElection;
        }

        // GET: api/Races
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Race>>> Get()
        {
            return await _context.Races.Include(c => c.CandidateRaces).ThenInclude(p => p.Candidate).Where(b => b.ElectionId == _currentElection).ToListAsync();
        }


        // GET: api/Races/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Race>> GetRace(int id)
        {
            var issue = await _context.Races.FindAsync(id);

            if (issue == null)
            {
                return NotFound();
            }

            return issue;
        }

        // POST: api/Races
        [HttpPost]
        public async Task<ActionResult<Race>> PostRace(Race issue)
        {
            _context.Races.Add(issue);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetRace", new { id = issue.RaceId }, issue);
        }

        // PUT: api/Races/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRace(int id, Race issue)
        {
            if (id != issue.RaceId)
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

        // DELETE: api/Races/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Race>> DeleteRace(int id)
        {
            var issue = await _context.Races.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }

            _context.Races.Remove(issue);
            await _context.SaveChangesAsync();

            return issue;
        }

        private bool IssueExists(int id)
        {
            return _context.Races.Any(e => e.RaceId == id);
        }

    }
}
