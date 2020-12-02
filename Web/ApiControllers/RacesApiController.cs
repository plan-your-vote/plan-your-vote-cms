using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ApiDTO;
using Web.Data;
using PlanYourVoteLibrary2;

namespace Web.Controllers
{
    [Route("api/races")]
    [ApiController]
    public class RacesApiController : ControllerBase
    {
        public const int STEP_NUMBER = 1; // Hard-coded

        private readonly ApplicationDbContext _context;
        private int _runningElection;

        public RacesApiController(ApplicationDbContext context)
        {
            _context = context;
            _runningElection = context.StateSingleton.Find(State.STATE_ID).RunningElectionID;
        }

        // GET: api/Races
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Race>>> Get()
        {
            var step1 = _context.Steps.Where(step => step.StepNumber == STEP_NUMBER).First();

            VotingPage votingPage = new VotingPage()
            {
                PageTitle = step1.StepTitle,
                PageDescription = step1.StepDescription,
                PageNumber = STEP_NUMBER,
            };

            var races = await _context.Races
                .Where(race => race.ElectionId == _runningElection)
                .Select(race => new
                {
                    race.BallotOrder,
                    race.PositionName,
                    race.NumberNeeded,
                    Candidates = race.CandidateRaces.Select(cr => new
                    {
                        cr.BallotOrder,
                        cr.Candidate.CandidateId,
                        cr.Candidate.Name,
                        cr.Candidate.Picture,
                        OrganizationName = cr.Candidate.Organization.Name,
                        Details = cr.Candidate.Details.Select(detail => new
                        {
                            detail.Title,
                            detail.Text,
                            detail.Format,
                        }),
                        Contacts = cr.Candidate.Contacts.Select(contact => new
                        {
                            contact.ContactMethod,
                            contact.ContactValue,
                        }),
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
