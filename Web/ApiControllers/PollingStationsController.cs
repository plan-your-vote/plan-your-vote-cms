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
    public class PollingStationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private int _runningElectionID;

        public PollingStationsController(ApplicationDbContext context)
        {
            _context = context;
            _runningElectionID = _context.StateSingleton.Find(State.STATE_ID).RunningElectionID;
        }

        // GET: api/PollingStations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PollingStation>>> GetPollingStations()
        {
            VotingPage votingPage = new VotingPage()
            {
                PageTitle = "CHOOSE YOUR VOTING DATE AND LOCATION",
                PageDescription = "Not sure when you want to vote yet? Don't worry - you're not committing to a particular day or place. If you live in the UBC Lands or University Endowment Lands, you can vote at 2 voting places only on October 20 Opens in new window. These 2 places are not shown on the map below. Skip this step to review your choices and create your plan.",
                PageNumber = 3,
            };

            var pollingStations = await _context.PollingStations.Where(b => b.ElectionId == _runningElectionID).ToListAsync();

            return Ok(new
            {
                votingPage,
                pollingStations
            });
        }

        // GET: api/PollingStations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PollingStation>> GetPollingStation(int id)
        {
            var pollingStation = await _context.PollingStations.FindAsync(id);

            if (pollingStation == null)
            {
                return NotFound();
            }

            return pollingStation;
        }

        private bool PollingStationExists(int id)
        {
            return _context.PollingStations.Any(e => e.PollingStationId == id);
        }
    }
}
