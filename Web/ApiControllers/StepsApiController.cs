using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.ApiControllers
{
    [Route("api/steps")]
    [ApiController]
    public class StepsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private int _currentElection;

        public StepsApiController(ApplicationDbContext context)
        {
            _context = context;
            _currentElection = _context.StateSingleton.Find(State.STATE_ID).CurrentElection;
        }

        // GET: api/StepsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Step>>> GetSteps()
        {
            return await _context.Steps.Where(s => s.ElectionId == _currentElection).ToListAsync();
        }

        // GET: api/StepsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Step>> GetStep(int id)
        {
            var step = await _context.Steps.FindAsync(id);

            if (step == null)
            {
                return NotFound();
            }

            return step;
        }

        private bool StepExists(int id)
        {
            return _context.Steps.Any(e => e.ID == id);
        }
    }
}
