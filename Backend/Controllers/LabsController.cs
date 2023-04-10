using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Models.DTO;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabsController : ControllerBase
    {
        private readonly OrderContext _context;

        public LabsController(OrderContext context)
        {
            _context = context;
        }

        // GET: api/Labs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LabDTO>>> GetLabs()
        {
          if (_context.Labs == null)
          {
              return NotFound();
          }
          List<LabDTO> DTOs = new List<LabDTO>();
            foreach (var item in _context.Labs)
            {
                DTOs.Add(new LabDTO(item));
            }
            return DTOs;
        }

        // GET: api/Labs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetLab(int id)
        {
          if (_context.Labs == null)
          {
              return NotFound();
          }
            var lab = await _context.Labs.FindAsync(id);

            if (lab == null)
            {
                return NotFound();
            }
            

            return new UserDTO(lab.user);
        }

        // PUT: api/Labs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLab(int id, Lab lab)
        {
            if (id != lab.Id)
            {
                return BadRequest();
            }

            _context.Entry(lab).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LabExists(id))
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

        // POST: api/Labs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lab>> PostLab(Lab lab)
        {
          if (_context.Labs == null)
          {
              return Problem("Entity set 'OrderContext.Labs'  is null.");
          }
            _context.Labs.Add(lab);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLab", new { id = lab.Id }, lab);
        }

        // DELETE: api/Labs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLab(int id)
        {
            if (_context.Labs == null)
            {
                return NotFound();
            }
            var lab = await _context.Labs.FindAsync(id);
            if (lab == null)
            {
                return NotFound();
            }

            _context.Labs.Remove(lab);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LabExists(int id)
        {
            return (_context.Labs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
