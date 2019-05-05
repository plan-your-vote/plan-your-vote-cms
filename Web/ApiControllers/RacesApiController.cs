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
    [Route("api/races")]
    [ApiController]
    public class RacesApiController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private int _currentElection;

        public RacesApiController(ApplicationDbContext context)
        {
            _context = context;
            _currentElection = context.StateSingleton.Find(State.STATE_ID).CurrentElection;
        }

        // GET: api/Races
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Race>>> Get()
        {
            VotingPage votingPage = new VotingPage()
            {
                PageTitle = "REVIEW AND SELECT CANDIDATES",
                PageDescription = "Add up to 1 mayor, 10 councillors, 7 Park Board commissioners, and 9 school trustees to your plan. Open a candidate to read their profile and add them to your plan. Change your choices in the selected candidates area above.\nA candidate’s profile expresses their views alone and these views aren’t endorsed by the City of Vancouver. Profiles are included exactly as candidates wrote them.\nIf you live in the UBC Lands or University Endowment Lands, and you do not own property in Vancouver, you can only vote for school trustees in the election.",
                PageNumber = 1,
            };

            var races = await _context.Races
                .Where(race => race.ElectionId == _currentElection)
                .Select(race => new
                {
                    race,
                    candidates = race.CandidateRaces
                    .Select(cr => new
                    {
                        cr.Candidate,
                        cr.Candidate.Contacts
                    })

                })
                .ToListAsync();

            return Ok(new
            {
                votingPage,
                races,
            });
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
