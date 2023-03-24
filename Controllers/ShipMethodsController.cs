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
    public class ShipMethodsController : ControllerBase
    {
        private readonly AdventureWorks2019Context _context;

        public ShipMethodsController(AdventureWorks2019Context context)
        {
            _context = context;
        }

        // GET: api/ShipMethods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipMethod>>> GetShipMethods()
        {
          if (_context.ShipMethods == null)
          {
              return NotFound();
          }
            return await _context.ShipMethods.ToListAsync();
        }

        // GET: api/ShipMethods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShipMethod>> GetShipMethod(int id)
        {
          if (_context.ShipMethods == null)
          {
              return NotFound();
          }
            var shipMethod = await _context.ShipMethods.FindAsync(id);

            if (shipMethod == null)
            {
                return NotFound();
            }

            return shipMethod;
        }

        // PUT: api/ShipMethods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShipMethod(int id, ShipMethod shipMethod)
        {
            if (id != shipMethod.ShipMethodId)
            {
                return BadRequest();
            }

            _context.Entry(shipMethod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShipMethodExists(id))
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

        // POST: api/ShipMethods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShipMethod>> PostShipMethod(ShipMethod shipMethod)
        {
          if (_context.ShipMethods == null)
          {
              return Problem("Entity set 'AdventureWorks2019Context.ShipMethods'  is null.");
          }
            _context.ShipMethods.Add(shipMethod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShipMethod", new { id = shipMethod.ShipMethodId }, shipMethod);
        }

        // DELETE: api/ShipMethods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipMethod(int id)
        {
            if (_context.ShipMethods == null)
            {
                return NotFound();
            }
            var shipMethod = await _context.ShipMethods.FindAsync(id);
            if (shipMethod == null)
            {
                return NotFound();
            }

            _context.ShipMethods.Remove(shipMethod);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShipMethodExists(int id)
        {
            return (_context.ShipMethods?.Any(e => e.ShipMethodId == id)).GetValueOrDefault();
        }
    }
}
