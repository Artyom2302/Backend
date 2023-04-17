using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Models.DTO;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using System.Data;

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
        [Authorize]
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

        [HttpGet]
        [Route("ShowLabs")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<LabDTO>>> GetLabs(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var user=_context.Users.Include(u=>u.Labs).Where(u => u.Id==id).FirstOrDefault();
            if (user == null) { 
                return NotFound();
            }

            List<LabDTO> DTOs = new List<LabDTO>();
            foreach (var item in user.Labs)
            {
                DTOs.Add(new LabDTO(item));
            }
            return DTOs;
        }

        [HttpGet]
        [Route("ShowUserByLogin")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetUserByLogin(string login)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var user = _context.Users.Include(u => u.Labs).Where(u => u.Login == login).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            return new UserDTO(user);
        }

        [HttpGet]
        [Route("ShowUserByRole")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetUserByAdmin(bool isAdmin)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var user = _context.Users.Include(u => u.Labs).Where(u => u.IsAdmin ==  isAdmin).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            return new UserDTO(user);
        }


        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize]
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

        [HttpGet()]
        [Route("GetToken")]
        public object GetToken()
        {
            return AuthOptions.GenerateToken();
        }

        [HttpGet()]
        [Route("GetAdminToken")]
        public object GetAdminToken()
        {
            return AuthOptions.GenerateToken(true);
        }


        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
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
        [Authorize]
        public async Task<IActionResult> PutUserLab(int id,LabDTOAdd labDTO)
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
            string stackname = FindStack(labDTO.Main_stack);
            labDTO.Main_stack = stackname;
            var lab=new Lab(labDTO);
            
            if (!user.AddLab(lab)){
                return BadRequest("Can't add");
            };
            _context.SaveChanges();            
            return NoContent();
        }

        [HttpPut]
        [Route("ChangeUserRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeUserRole(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return BadRequest();
            }
            
           
            user.IsAdmin = !user.IsAdmin;
           
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
        [Authorize(Roles = "Admin")]
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

        [HttpDelete]
        [Route("DeleteUserLab")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserLab(int id,string labname)
        {
            var user = _context.Users.Include(u => u.Labs).Where(u=>u.Id==id).FirstOrDefault();
            if (user == null)
            {
                return BadRequest();
            }
            var lab = _context.Labs.Where(l => l.Name == labname).FirstOrDefault();
            if (lab== null)
            {
                return BadRequest("Not Found");
            }  


            if (!user.DeleteLab(lab))
            {
                return BadRequest("Can't Delete");
            };
            lab.UserId = null;
           _context.Entry(user).State = EntityState.Modified;
            _context.Entry(lab).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
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
