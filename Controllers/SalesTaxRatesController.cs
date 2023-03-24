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
    public class SalesTaxRatesController : ControllerBase
    {
        private readonly AdventureWorks2019Context _context;

        public SalesTaxRatesController(AdventureWorks2019Context context)
        {
            _context = context;
        }

        // GET: api/SalesTaxRates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesTaxRate>>> GetSalesTaxRates()
        {
          if (_context.SalesTaxRates == null)
          {
              return NotFound();
          }
            return await _context.SalesTaxRates.ToListAsync();
        }

        // GET: api/SalesTaxRates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesTaxRate>> GetSalesTaxRate(int id)
        {
          if (_context.SalesTaxRates == null)
          {
              return NotFound();
          }
            var salesTaxRate = await _context.SalesTaxRates.FindAsync(id);

            if (salesTaxRate == null)
            {
                return NotFound();
            }

            return salesTaxRate;
        }

        // PUT: api/SalesTaxRates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesTaxRate(int id, SalesTaxRate salesTaxRate)
        {
            if (id != salesTaxRate.SalesTaxRateId)
            {
                return BadRequest();
            }

            _context.Entry(salesTaxRate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesTaxRateExists(id))
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

        // POST: api/SalesTaxRates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SalesTaxRate>> PostSalesTaxRate(SalesTaxRate salesTaxRate)
        {
          if (_context.SalesTaxRates == null)
          {
              return Problem("Entity set 'AdventureWorks2019Context.SalesTaxRates'  is null.");
          }
            _context.SalesTaxRates.Add(salesTaxRate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalesTaxRate", new { id = salesTaxRate.SalesTaxRateId }, salesTaxRate);
        }

        // DELETE: api/SalesTaxRates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesTaxRate(int id)
        {
            if (_context.SalesTaxRates == null)
            {
                return NotFound();
            }
            var salesTaxRate = await _context.SalesTaxRates.FindAsync(id);
            if (salesTaxRate == null)
            {
                return NotFound();
            }

            _context.SalesTaxRates.Remove(salesTaxRate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalesTaxRateExists(int id)
        {
            return (_context.SalesTaxRates?.Any(e => e.SalesTaxRateId == id)).GetValueOrDefault();
        }
    }
}
