using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanYourVoteLibrary2;
using Web.Data;
using Web.ApiDTO;

namespace Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollingPlacesController : ControllerBase
    {
        public const int STEP_NUMBER = 3; // Hard-coded

        private readonly ApplicationDbContext _context;
        private int _runningElectionID;

        public PollingPlacesController(ApplicationDbContext context)
        {
            _context = context;
            _runningElectionID = _context.StateSingleton.Find(State.STATE_ID).RunningElectionID;
        }

        // GET: api/PollingPlaces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PollingPlace>>> GetPollingPlaces()
        {
            var step3 = _context.Steps.Where(step => step.StepNumber == STEP_NUMBER).First();

            VotingPage votingPage = new VotingPage()
            {
                PageTitle = step3.StepTitle,
                PageDescription = step3.StepDescription,
                PageNumber = STEP_NUMBER,
            };

            var pollingPlaces = await _context.PollingPlaces
                .Where(pp => pp.ElectionId == _runningElectionID)
                .Select(pp => new
                {
                    pp.PollingPlaceId,
                    pp.PollingPlaceName,
                    pp.Address,
                    pp.PollingStationName,
                    pp.ParkingInfo,
                    pp.WheelchairInfo,
                    pp.AdvanceOnly,
                    pp.LocalArea,
                    pp.Phone,
                    pp.Email,
                    pp.Latitude,
                    pp.Longitude,
                    PollingPlaceDates = pp.PollingPlaceDates.Select(ppd => new
                    {
                        ppd.PollingDate,
                        ppd.StartTime,
                        ppd.EndTime,
                    })
                })
                .ToListAsync();

            return Ok(new
            {
                votingPage,
                pollingPlaces
            });
        }

        // GET: api/PollingPlaces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PollingPlace>> GetPollingPlace(int id)
        {
            var pollingPlaces = await _context.PollingPlaces.FindAsync(id);

            if (pollingPlaces == null)
            {
                return NotFound();
            }

            return pollingPlaces;
        }

        private bool PollingPlaceExists(int id)
        {
            return _context.PollingPlaces.Any(e => e.PollingPlaceId == id);
        }
    }
}
