using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Models.DTO;
using System.ComponentModel.Design.Serialization;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StackNamesController : ControllerBase
    {
        private readonly OrderContext _context;

        public StackNamesController(OrderContext context)
        {
            _context = context;
        }

        // GET: api/StackNames
        [HttpGet]
       // [Authorize]
        public async Task<ActionResult<IEnumerable<StackNames>>> GetStackNames()
        {
          if (_context.StackNames == null)
          {
              return NotFound();
          }
            return await _context.StackNames.Include(s=>s.DifferentNames).ToListAsync();
        }

        // GET: api/StackNames/5
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<StackNames>> GetStackNames(int id)
        {
          if (_context.StackNames == null)
          {
              return NotFound();
          }
            var stackNames = await _context.StackNames.FindAsync(id);

            if (stackNames == null)
            {
                return NotFound();
            }

            return stackNames;
        }

        // PUT: api/StackNames/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
      //  [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutStackNames(int id, StackNames stackNames)
        {
            if (id != stackNames.Id)
            {
                return BadRequest();
            }

            _context.Entry(stackNames).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StackNamesExists(id))
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

        [HttpPut]
        [Route("AddStackNames")]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddStackNames(int id, string stackname)
        {
            var StackNames = _context.StackNames.Find(id);
            if (StackNames == null)
            {
                return BadRequest();
            }
           StackName stack =new StackName(stackname.ToLower());

            if (!StackNames.AddDiffNames(stack))
            {
                return BadRequest("Can't add");
            };
            _context.SaveChanges();
            return NoContent();
        }


        // POST: api/StackNames
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
       // [Authorize(Roles = "Admin")]
        public async Task<ActionResult<StackNames>> PostStackNames(string name)
        {
            name = name.ToLower();
          if (_context.StackNames == null)
          {
              return Problem("Entity set 'OrderContext.StackNames'  is null.");
          }
          var stack=_context.StackNames.Where(s=>s.Namestack==name).FirstOrDefault();
            if (stack != null)
                return Problem("Exists");
            StackNames stackNames=new StackNames(name);
            _context.StackNames.Add(stackNames);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStackNames", new { id = stackNames.Id }, stackNames);
        }

        // DELETE: api/StackNames/5
        [HttpDelete("{id}")]
      //  [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStackNames(int id)
        {
            if (_context.StackNames == null)
            {
                return NotFound();
            }
            var stackNames = await _context.StackNames.FindAsync(id);
            if (stackNames == null)
            {
                return NotFound();
            }

            _context.StackNames.Remove(stackNames);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StackNamesExists(int id)
        {
            return (_context.StackNames?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
