using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Models.DTO;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

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
        //[Authorize]
        public async Task<ActionResult<IEnumerable<ProgrammerDTO>>> GetProgrammers()
        {
            if (_context.Programmers == null)
            {
                return NotFound();
            }
            List<ProgrammerDTO> DTOs = new List<ProgrammerDTO>();
            foreach (var prog in _context.Programmers)
            {
                DTOs.Add(new ProgrammerDTO(prog));
            }
            return DTOs;
        }

        // GET: api/Programmers/5
        [HttpGet("{id}")]
      // [Authorize]
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
        [Route("ShowLabs")]
       // [Authorize]
        public async Task<ActionResult<List<LabDTO>>> GetLabs(int id)
        {
            if (_context.Programmers == null)
            {
                return NotFound();
            }
            var programmer = _context.Programmers.Include(p=>p.Labs).Where(p=>p.Id==id).FirstOrDefault();

            if (programmer == null)
            {
                return NotFound();
            }
            List<LabDTO> DTOs = new List<LabDTO>();
            foreach (var lab in programmer.Labs)
            {
                DTOs.Add(new LabDTO(lab));
            }
            return DTOs;
           
        }

        [HttpGet]
        [Route("ShowProgByInitials")]
       // [Authorize]
        public async Task<ActionResult<ProgrammerDTO>> GetProgByInitials(string surname,string name)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var prog = _context.Programmers.Include(p => p.Labs).Where(p=>p.Name==name || p.Surname == surname).FirstOrDefault();
            if (prog == null)
            {
                return NotFound();
            }
            return new ProgrammerDTO(prog);
        }

        [HttpGet]
        [Route("ShowProgByMainStack")]
       // [Authorize]
        public async Task<ActionResult<List<ProgrammerDTO>>> GetProgByMainStack(string mainstack)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            string stack=FindStack(mainstack);
            var prog = _context.Programmers.Where(s=>s.Main_stack==stack);
            if (prog == null)
            {
                return NotFound();
            }
            List <ProgrammerDTO> dTOs = new List<ProgrammerDTO>();
            foreach (var item in prog)
            {
                dTOs.Add(new ProgrammerDTO(item));
            }
            return dTOs;
        }


        // PUT: api/Programmers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
      //  [Authorize(Roles = "Admin")]
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

        [HttpPut]
        [Route("PutLab")]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutProgrammerLab(int id,int labId)
        {

            var prog = _context.Programmers.Find(id);
            if (prog == null)
            {
                return BadRequest();
            }
            var lab = _context.Labs.Find(labId);
            if (lab == null)
            {
                return BadRequest("Dont exists");
            }

            if (!prog.AddLab(lab))
            {
                return BadRequest("Can't add");
            };
            _context.SaveChanges();
            return NoContent();
        }

        // POST: api/Programmers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
       // [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Programmer>> PostProgrammer(ProgrammerDTO dTO)
        {
          if (_context.Programmers == null)
          {
              return Problem("Entity set 'OrderContext.Programmers'  is null.");
          }

          string stackname =FindStack(dTO.Main_stack);
            dTO.Main_stack = stackname;
          Programmer programmer=new Programmer(dTO);
            _context.Programmers.Add(programmer);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProgrammer", new { id = programmer.Id }, programmer);
        }

        // DELETE: api/Programmers/5
        [HttpDelete("{id}")]
      //  [Authorize(Roles = "Admin")]
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

        [HttpDelete]
        [Route("DeleteProgLab")]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProgLab(int id, string labname)
        {
            var prog = _context.Programmers.Include(u => u.Labs).Where(u => u.Id == id).FirstOrDefault();
            if (prog == null)
            {
                return BadRequest();
            }
            var lab = _context.Labs.Where(l => l.Name == labname).FirstOrDefault();
            if (lab == null)
            {
                return BadRequest("Not Found");
            }


            if (!prog.DeleteLab(lab))
            {
                return BadRequest("Can't Delete");
            };
            lab.UserId = null;
            _context.Entry(prog).State = EntityState.Modified;
            _context.Entry(lab).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        private bool ProgrammerExists(int id)
        {
            return (_context.Programmers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private string FindStack(string name)
        {
            foreach (var stack in _context.StackNames.Include(s=>s.DifferentNames)) { 
                if(stack.Namestack.ToLower() == name.ToLower())
                {
                    return stack.Namestack;
                }
                else
                {
                    foreach(var item in stack.DifferentNames)
                    {
                        if (item.Name.ToLower() == name.ToLower())
                        {
                            return stack.Namestack;
                        }
                    }
                } 
            }
            return " ";
        }
    }
}
