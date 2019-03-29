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
    [Route("api/[controller]")]
    [ApiController]
    public class PollingStationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private int _currentElection;

        public PollingStationsController(ApplicationDbContext context)
        {
            _context = context;
            _currentElection = _context.StateSingleton.Find(State.STATE_ID).currentElection;
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

        // PUT: api/PollingStations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPollingStation(int id, PollingStation pollingStation)
        {
            if (id != pollingStation.PollingStationId)
            {
                return BadRequest();
            }

            _context.Entry(pollingStation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollingStationExists(id))
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

        // POST: api/PollingStations
        [HttpPost]
        public async Task<ActionResult<PollingStation>> PostPollingStation(PollingStation pollingStation)
        {
            _context.PollingStations.Add(pollingStation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPollingStation", new { id = pollingStation.PollingStationId }, pollingStation);
        }

        // DELETE: api/PollingStations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PollingStation>> DeletePollingStation(int id)
        {
            var pollingStation = await _context.PollingStations.FindAsync(id);
            if (pollingStation == null)
            {
                return NotFound();
            }

            _context.PollingStations.Remove(pollingStation);
            await _context.SaveChangesAsync();

            return pollingStation;
        }

        private bool PollingStationExists(int id)
        {
            return _context.PollingStations.Any(e => e.PollingStationId == id);
        }
    }
}
