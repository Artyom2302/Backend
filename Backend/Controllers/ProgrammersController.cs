using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

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
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<Programmer>>> GetProgrammers()
        {
          if (_context.Programmers == null)
          {
              return NotFound();
          }
            return await _context.Programmers.ToListAsync();
        }

        // GET: api/Programmers/5
        [HttpGet("{surname}/{name}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<Programmer>> GetProgrammer(string name,string surname)
        {
          if (_context.Programmers == null)
          {
              return NotFound();
          }
            var responce = _context.Programmers.Include(p => p.Orders).FirstOrDefault(p => p.Name == name && p.Surname == surname);  // добавляем данные по компаниям
                                    

            if (responce == null)
            {
                return NotFound();
            }

            

            return responce;
        }

        // PUT: api/Programmers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

       

        // POST: api/Programmers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize]
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

        [HttpGet("Name")]
        public async Task<IActionResult> GetProgrammerToken(int id)
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
