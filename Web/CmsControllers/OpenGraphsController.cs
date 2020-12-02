using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlanYourVoteLibrary2;

namespace Web.CmsControllers
{
    [Authorize]
    public class OpenGraphsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OpenGraphsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OpenGraphs
        public async Task<IActionResult> Index()
        {
            return View(await _context.OpenGraph.ToListAsync());
        }

        // GET: OpenGraphs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var openGraph = await _context.OpenGraph
                .FirstOrDefaultAsync(m => m.OpenGraphId == id);
            if (openGraph == null)
            {
                return NotFound();
            }

            return View(openGraph);
        }

        // GET: OpenGraphs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OpenGraphs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OpenGraphId,Title,URL,Image,Determiner,Locale,SiteName")] OpenGraph openGraph)
        {
            if (ModelState.IsValid)
            {
                _context.Add(openGraph);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(openGraph);
        }

        // GET: OpenGraphs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var openGraph = await _context.OpenGraph.FindAsync(id);
            if (openGraph == null)
            {
                return NotFound();
            }
            return View(openGraph);
        }

        // POST: OpenGraphs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OpenGraphId,Title,URL,Image,Determiner,Locale,SiteName")] OpenGraph openGraph)
        {
            if (id != openGraph.OpenGraphId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(openGraph);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpenGraphExists(openGraph.OpenGraphId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(openGraph);
        }

        // GET: OpenGraphs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var openGraph = await _context.OpenGraph
                .FirstOrDefaultAsync(m => m.OpenGraphId == id);
            if (openGraph == null)
            {
                return NotFound();
            }

            return View(openGraph);
        }

        // POST: OpenGraphs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var openGraph = await _context.OpenGraph.FindAsync(id);
            _context.OpenGraph.Remove(openGraph);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpenGraphExists(int id)
        {
            return _context.OpenGraph.Any(e => e.OpenGraphId == id);
        }
    }
}
