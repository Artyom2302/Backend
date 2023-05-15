using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Models.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authorization;

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
      //  [Authorize]
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

        [HttpGet]
        [Route("ShowLabsByDate")]
      //  [Authorize]
        public async Task<ActionResult<IEnumerable<LabDTO>>> GetLabsByDataTme(DateTime datestart,DateTime datefinish)
        {
            var labs = _context.Labs.Where(l => l.Date >= datestart && l.Date<=datefinish) ;
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

        [HttpGet]
        [Route("ShowLabByName")]
        //  [Authorize]
        public async Task<ActionResult<LabDTO>> GetLabByName(string name)
        {
            if (_context.Labs == null)
            {
                return NotFound();
            }
            var lab = _context.Labs.Where(s => s.Name == name).FirstOrDefault();
            if (lab == null)
            {
                return NotFound();
            }
            LabDTO dTO = new LabDTO(lab);
            return dTO;
        }



        [HttpGet]
        [Route("ShowLabByMainStack")]
      //  [Authorize]
        public async Task<ActionResult<List<LabDTO>>> GetLabsByMainStack(string mainstack)
        {
            if (_context.Labs == null)
            {
                return NotFound();
            }
            string stack = FindStack(mainstack);
            var labs = _context.Labs.Where(s => s.Main_stack == stack);
            if (labs == null)
            {
                return NotFound();
            }
            List<LabDTO> dTOs = new List<LabDTO>();
            foreach (var item in labs)
            {
                dTOs.Add(new LabDTO(item));
            }
            return dTOs;
        }


        [HttpGet]
        [Route("ShowLabByDone")]
     //   [Authorize]
        public async Task<ActionResult<List<LabDTO>>> GetLabsByDone(bool done)
        {
            var labs = _context.Labs.Where(l => l.IsDone==done);
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
       // [Authorize]

        public async Task<ActionResult<UserDTO>> GetUserLab(int id)
        {
          if (_context.Labs == null)
          {
              return NotFound();
          }
            var lab = await _context.Labs.FindAsync(id);
            var user = await _context.Users.FindAsync(lab.UserId);
            if (lab == null || user==null)
            {
                return NotFound();
            }
            
            return new UserDTO(user);
        }



        // PUT: api/Labs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
     //  [Authorize(Roles = "Admin")]
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


        [HttpPut]
        [Route("PutLabReview")]
        //[Authorize]
        public async Task<IActionResult> PutLabReview(int id ,ReviewDTO review)
        {
            var lab = _context.Labs.Find(id);
            if (lab == null)
            {
                return BadRequest();
            }

            if (!lab.AddReview(review))
            {
                return BadRequest("Can't add");
            };
            _context.SaveChanges();
            return NoContent();
        }


        [HttpPut]
        [Route("ChangeLabState")]
        //[Authorize]
        public async Task<IActionResult> ChangeLabState(int id)
        {
            var lab = _context.Labs.Find(id);
            if (lab == null)
            {
                return BadRequest();
            }
            lab.IsDone = !lab.IsDone;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteLabReview")]
        //[Authorize]
        public async Task<IActionResult> DeleteLabReview(int id)
        {
            var lab = _context.Labs.Include(l => l.Review).Where(l=>l.Id==id).FirstOrDefault() ;
            if (lab == null)
            {
                return BadRequest();
            }

            lab.DeleteReview();


            _context.SaveChanges();
            return NoContent();
        }


        // POST: api/Labs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
       // [Authorize]
        public async Task<ActionResult<Lab>> PostLab(LabDTOAdd dTO)
        {
          if (_context.Labs == null)
          {
              return Problem("Entity set 'OrderContext.Labs'  is null.");
          }

            string stackname = FindStack(dTO.Main_stack);
            dTO.Main_stack = stackname;
            Lab lab = new Lab(dTO);
            _context.Labs.Add(lab);
            await _context.SaveChangesAsync();

            return lab;
        }

        // DELETE: api/Labs/5
        [HttpDelete("{id}")]
       // [Authorize]
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

        private string FindStack(string name)
        {
            foreach (var stack in _context.StackNames.Include(s => s.DifferentNames))
            {
                if (stack.Namestack.ToLower() == name.ToLower())
                {
                    return stack.Namestack;
                }
                else
                {
                    foreach (var item in stack.DifferentNames)
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
