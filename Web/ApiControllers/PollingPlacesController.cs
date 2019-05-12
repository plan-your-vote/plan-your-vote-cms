using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Data;
using Web.ApiDTO;

namespace Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollingPlacesController : ControllerBase
    {
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
            VotingPage votingPage = new VotingPage()
            {
                PageTitle = "CHOOSE YOUR VOTING DATE AND LOCATION",
                PageDescription = "Not sure when you want to vote yet? Don't worry - you're not committing to a particular day or place. If you live in the UBC Lands or University Endowment Lands, you can vote at 2 voting places only on October 20 Opens in new window. These 2 places are not shown on the map below. Skip this step to review your choices and create your plan.",
                PageNumber = 3,
            };

            var pollingPlaces = await _context.PollingPlaces
                .Where(pp => pp.ElectionId == _runningElectionID)
                .Select(pp => new
                {
                    pp.PollingPlaceName,
                    pp.Address,
                    pp.PollingStationName,
                    pp.GeneralAccessInfo,
                    pp.ParkingInfo,
                    pp.WheelchairInfo,
                    pp.Latitude,
                    pp.Longitude,
                    pp.WashroomInfo,
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
