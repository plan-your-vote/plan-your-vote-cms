using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanYourVoteLibrary2;
using Web.Data;

namespace Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ThemesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Theme (Returns selected theme)
        [Route("~/api/Theme")]
        [HttpGet]
        public IActionResult GetTheme()
        {
            Theme selectedTheme = _context.Themes.First(t => t.Selected);

            return Ok(new
            {
                SelectedTheme = selectedTheme,
                Images = _context.Images.Where(t => t.ThemeName == selectedTheme.ThemeName),
            });
        }

        // GET: api/Themes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Theme>> GetTheme(string id)
        {
            var theme = await _context.Themes.FindAsync(id);

            if (theme == null)
            {
                return NotFound();
            }

            return theme;
        }

        // PUT: api/Themes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTheme(string id, Theme theme)
        {
            if (id != theme.ThemeName)
            {
                return BadRequest();
            }

            _context.Entry(theme).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThemeExists(id))
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

        // POST: api/Themes
        [HttpPost]
        public async Task<ActionResult<Theme>> PostTheme(Theme theme)
        {
            _context.Themes.Add(theme);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTheme", new { id = theme.ThemeName }, theme);
        }

        // DELETE: api/Themes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Theme>> DeleteTheme(string id)
        {
            var theme = await _context.Themes.FindAsync(id);
            if (theme == null)
            {
                return NotFound();
            }

            _context.Themes.Remove(theme);
            await _context.SaveChangesAsync();

            return theme;
        }

        private bool ThemeExists(string id)
        {
            return _context.Themes.Any(e => e.ThemeName == id);
        }
    }
}
