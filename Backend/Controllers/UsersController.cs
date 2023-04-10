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
    public class UsersController : ControllerBase
    {
        private readonly OrderContext _context;

        public UsersController(OrderContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            List<UserDTO> DTOs = new List<UserDTO>();
            foreach (var user in _context.Users)
            {
                DTOs.Add(new UserDTO(user));
            }
            return DTOs;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return new UserDTO(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        [Route("PutUserLab")]
        public async Task<IActionResult> PutUserLab(int id,LabDTO labDTO)
        {
            var user = _context.Users.Find(id);
            if (user==null)
            {
                return BadRequest();
            }
            if (_context.Labs.Where(l => l.Name == labDTO.Name).FirstOrDefault() != null)
            {
                return BadRequest("Already exists");
            }
            var lab=new Lab(labDTO);
            lab.user= user; 
            if (!user.AddLab(lab)){
                return BadRequest("Can't add");
            };
            _context.SaveChanges();            
            return NoContent();
        }


        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserDTO userDTO)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'OrderContext.Users'  is null.");
          }
            var user = new User(userDTO);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
