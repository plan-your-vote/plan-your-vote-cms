using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Data;

namespace Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollingStationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private int _currentElection;

        public PollingStationsController(ApplicationDbContext context)
        {
            _context = context;
            _currentElection = _context.StateSingleton.Find(State.STATE_ID).CurrentElection;
        }

        // GET: api/PollingStations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PollingStation>>> GetPollingStations()
        {
            return await _context.PollingStations.Where(b => b.ElectionId == _currentElection).ToListAsync();
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
