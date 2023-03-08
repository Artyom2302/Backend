using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammersController : ControllerBase
    {
        private readonly OrderContext _context;

        public ProgrammersController(OrderContext context)
        {
            _context = context;
        }

        // GET: api/Programmers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Programmer>>> GetProgrammers()
        {
          if (_context.Programmers == null)
          {
              return NotFound();
          }
            return await _context.Programmers.ToListAsync();
        }

        // GET: api/Programmers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Programmer>> GetProgrammer(int id)
        {
          if (_context.Programmers == null)
          {
              return NotFound();
          }
            var programmer = await _context.Programmers.FindAsync(id);

            if (programmer == null)
            {
                return NotFound();
            }

            return programmer;
        }

        // PUT: api/Programmers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgrammer(int id, Programmer programmer)
        {
            if (id != programmer.Order_Id)
            {
                return BadRequest();
            }

            _context.Entry(programmer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgrammerExists(id))
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

        // POST: api/Programmers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Programmer>> PostProgrammer(Programmer programmer)
        {
          if (_context.Programmers == null)
          {
              return Problem("Entity set 'OrderContext.Programmers'  is null.");
          }
            _context.Programmers.Add(programmer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProgrammerExists(programmer.Order_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProgrammer", new { id = programmer.Order_Id }, programmer);
        }

        // DELETE: api/Programmers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgrammer(int id)
        {
            if (_context.Programmers == null)
            {
                return NotFound();
            }
            var programmer = await _context.Programmers.FindAsync(id);
            if (programmer == null)
            {
                return NotFound();
            }

            _context.Programmers.Remove(programmer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgrammerExists(int id)
        {
            return (_context.Programmers?.Any(e => e.Order_Id == id)).GetValueOrDefault();
        }
    }
}
