using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lange_dbConnect.Models;

namespace Lange_dbConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScrapReasonsController : ControllerBase
    {
        private readonly AdventureWorks2019Context _context;

        public ScrapReasonsController(AdventureWorks2019Context context)
        {
            _context = context;
        }

        // GET: api/ScrapReasons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScrapReason>>> GetScrapReasons()
        {
          if (_context.ScrapReasons == null)
          {
              return NotFound();
          }
            return await _context.ScrapReasons.ToListAsync();
        }

        // GET: api/ScrapReasons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScrapReason>> GetScrapReason(short id)
        {
          if (_context.ScrapReasons == null)
          {
              return NotFound();
          }
            var scrapReason = await _context.ScrapReasons.FindAsync(id);

            if (scrapReason == null)
            {
                return NotFound();
            }

            return scrapReason;
        }

        // PUT: api/ScrapReasons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScrapReason(short id, ScrapReason scrapReason)
        {
            if (id != scrapReason.ScrapReasonId)
            {
                return BadRequest();
            }

            _context.Entry(scrapReason).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScrapReasonExists(id))
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

        // POST: api/ScrapReasons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ScrapReason>> PostScrapReason(ScrapReason scrapReason)
        {
          if (_context.ScrapReasons == null)
          {
              return Problem("Entity set 'AdventureWorks2019Context.ScrapReasons'  is null.");
          }
            _context.ScrapReasons.Add(scrapReason);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScrapReason", new { id = scrapReason.ScrapReasonId }, scrapReason);
        }

        // DELETE: api/ScrapReasons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScrapReason(short id)
        {
            if (_context.ScrapReasons == null)
            {
                return NotFound();
            }
            var scrapReason = await _context.ScrapReasons.FindAsync(id);
            if (scrapReason == null)
            {
                return NotFound();
            }

            _context.ScrapReasons.Remove(scrapReason);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScrapReasonExists(short id)
        {
            return (_context.ScrapReasons?.Any(e => e.ScrapReasonId == id)).GetValueOrDefault();
        }
    }
}
