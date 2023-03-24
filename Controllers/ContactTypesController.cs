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
    public class ContactTypesController : ControllerBase
    {
        private readonly AdventureWorks2019Context _context;

        public ContactTypesController(AdventureWorks2019Context context)
        {
            _context = context;
        }

        // GET: api/ContactTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactType>>> GetContactTypes()
        {
          if (_context.ContactTypes == null)
          {
              return NotFound();
          }
            return await _context.ContactTypes.ToListAsync();
        }

        // GET: api/ContactTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactType>> GetContactType(int id)
        {
          if (_context.ContactTypes == null)
          {
              return NotFound();
          }
            var contactType = await _context.ContactTypes.FindAsync(id);

            if (contactType == null)
            {
                return NotFound();
            }

            return contactType;
        }

        // PUT: api/ContactTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactType(int id, ContactType contactType)
        {
            if (id != contactType.ContactTypeId)
            {
                return BadRequest();
            }

            _context.Entry(contactType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactTypeExists(id))
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

        // POST: api/ContactTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContactType>> PostContactType(ContactType contactType)
        {
          if (_context.ContactTypes == null)
          {
              return Problem("Entity set 'AdventureWorks2019Context.ContactTypes'  is null.");
          }
            _context.ContactTypes.Add(contactType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactType", new { id = contactType.ContactTypeId }, contactType);
        }

        // DELETE: api/ContactTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactType(int id)
        {
            if (_context.ContactTypes == null)
            {
                return NotFound();
            }
            var contactType = await _context.ContactTypes.FindAsync(id);
            if (contactType == null)
            {
                return NotFound();
            }

            _context.ContactTypes.Remove(contactType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactTypeExists(int id)
        {
            return (_context.ContactTypes?.Any(e => e.ContactTypeId == id)).GetValueOrDefault();
        }
    }
}
