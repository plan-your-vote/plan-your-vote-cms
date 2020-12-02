using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using PlanYourVoteLibrary2;

namespace Web.ApiControllers
{
    [Route("api/socialmedia")]
    [ApiController]
    public class SocialMediasAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SocialMediasAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SocialMediasAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SocialMedia>>> GetSocialMedias()
        {
            var socialMedias = await _context
                .SocialMedias
                .Select(socialMedia => new
                {
                    socialMedia.MediaName,
                    socialMedia.Message,
                    socialMedia.Link,
                })
                .ToListAsync();

            return Ok(new
            {
                socialMedias,
            });
        }

        // GET: api/SocialMediasAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SocialMedia>> GetSocialMedia(int id)
        {
            var socialMedia = await _context.SocialMedias.FindAsync(id);

            if (socialMedia == null)
            {
                return NotFound();
            }

            return socialMedia;
        }

        // PUT: api/SocialMediasAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSocialMedia(int id, SocialMedia socialMedia)
        {
            if (id != socialMedia.ID)
            {
                return BadRequest();
            }

            _context.Entry(socialMedia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocialMediaExists(id))
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

        // POST: api/SocialMediasAPI
        [HttpPost]
        public async Task<ActionResult<SocialMedia>> PostSocialMedia(SocialMedia socialMedia)
        {
            _context.SocialMedias.Add(socialMedia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSocialMedia", new { id = socialMedia.ID }, socialMedia);
        }

        // DELETE: api/SocialMediasAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SocialMedia>> DeleteSocialMedia(int id)
        {
            var socialMedia = await _context.SocialMedias.FindAsync(id);
            if (socialMedia == null)
            {
                return NotFound();
            }

            _context.SocialMedias.Remove(socialMedia);
            await _context.SaveChangesAsync();

            return socialMedia;
        }

        private bool SocialMediaExists(int id)
        {
            return _context.SocialMedias.Any(e => e.ID == id);
        }
    }
}
