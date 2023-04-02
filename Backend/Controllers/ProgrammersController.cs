using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Backend.Models.DTO;

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
        [Authorize]
        public async Task<ActionResult<IEnumerable<Programmer>>> GetProgrammers()
        {
            if (_context.Programmers == null)
            {
                return NotFound();
            }
            return await _context.Programmers.Include(p=>p.Orders).ToListAsync();
        }

        // GET: api/Programmers/5
        [HttpGet("{id}")]
        [Authorize]
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


        [HttpGet]
        [Route("GetProgByName")]
        [Authorize]
        public async Task<ActionResult<Programmer>> GetProgrammerBySurName(string surname, string name)
        {
            if (_context.Programmers == null)
            {
                return NotFound();
            }
            var responce = _context.Programmers.Include(p => p.Orders).FirstOrDefault(p => p.Name == name && p.Surname == surname);

            if (responce == null)
            {
                return NotFound();
            }

            return responce;
        }

        [HttpGet]
        [Route("GetProgsByStack")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProgrammerDTO>>> GetProgrammersByMainStack(string main_stack)
        {
            if (_context.Programmers == null)
            {
                return NotFound();
            }
            var responce = _context.Programmers.Include(p => p.Orders).Where(p => p.Main_stack == main_stack).ToList();

            if (responce == null)
            {
                return NotFound();
            }
            List<ProgrammerDTO> dTOs = new List<ProgrammerDTO>();
            foreach (var prog in responce) {
                dTOs.Add(new ProgrammerDTO(prog));
            }

            return dTOs;
        }

        [HttpGet]
        [Route("GetProgByOrderName")]
        [Authorize]
        public ActionResult<ProgrammerDTO> GetProgrammerByOrdername(string ordername)
        {
            if (_context.Programmers == null)
            {
                return NotFound();
            }

            foreach (var prog in from Programmer prog in _context.Programmers
                                 from order in prog.Orders
                                 where order.Name == ordername
                                 select prog)

            {
                ProgrammerDTO dto = new ProgrammerDTO(prog);
                return dto;
            }


            return NotFound();
        }

        // PUT: api/Programmers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutProgrammer(int id, Programmer programmer)
        {
            if (id != programmer.Id)
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


        [HttpPut()]
        [Route("AddOrder")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddOrder(int prog_id, int order_id)
        {
            if (!ProgrammerExists(prog_id)|| !(_context.Orders?.Any(e => e.Id == order_id)).GetValueOrDefault())
            {
                return NotFound();
            }
            var order = _context.Orders.Where(o => o.Id == order_id).FirstOrDefault();
            foreach (var ord in _context.Programmers.SelectMany(programmer => programmer.Orders))
            {
                if (ord == order)
                {
                    return BadRequest();
                };
            }

            var prog  = _context.Programmers.Where(p=>p.Id == prog_id).FirstOrDefault();

            prog.AddOrder(order);
            _context.Entry(prog).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut()]
        [Route("DeleteOrder")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrder(int prog_id, int order_id)
        {
            if (!ProgrammerExists(prog_id) || !(_context.Orders?.Any(e => e.Id == order_id)).GetValueOrDefault())
            {
                return NotFound();
            }
            var prog=_context.Programmers.FirstOrDefault(p=>p.Id==prog_id);

            var order = _context.Orders.FirstOrDefault(o => o.Id == order_id);
            if (prog.Orders.Contains(order))
            {
                prog.DeleteOrder(order);
                _context.Entry(prog).State = EntityState.Modified;
                _context.SaveChanges();

            }
            else
            {
                return NotFound();
            }

           
            return NoContent();
        }



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
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProgrammer", new { id = programmer.Id }, programmer);
        }

        // DELETE: api/Programmers/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProgrammer(int id)
        {
            if (_context.Programmers == null)
            {
                return NotFound();
            }
            var programmer = _context.Programmers.Include(p=> p.Orders).FirstOrDefault(p=> p.Id==id);
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
            return (_context.Programmers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
