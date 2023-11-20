using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HauCK.Data;
using HauCK.Entiities;
using HauCK.Model;
using HauCK.Extends;
using Microsoft.AspNetCore.Authorization;
using HauCK.Enum;
using Azure;

namespace HauCK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Assignments
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<AssignmentModel>>> GetAssignments()
        {
            var ac = HttpContext.User;
            if (_context.Assignments == null)
            {
                return NotFound();
            }
            return await _context.Assignments.Convert();
        }

        // GET: api/Assignments/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AssignmentModel>> GetAssignment(Guid id)
        {
          if (_context.Assignments == null)
          {
              return NotFound();
          }
            var assignment = await _context.Assignments.FindAsync(id);

            if (assignment == null)
            {
                return NotFound();
            }

            return assignment.Convert();
        }

        // PUT: api/Assignments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = $"{UserRoles.Leader}, {UserRoles.Admin}")]
        public async Task<IActionResult> PutAssignment(Guid id, Assignment assignment)
        {
            if (id != assignment.Guid)
            {
                return BadRequest();
            }
            assignment.DateUpdate = DateTime.Now;
            _context.Entry(assignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Assignments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Leader}, {UserRoles.Admin}")]
        public async Task<ActionResult<Assignment>> PostAssignment(Assignment assignment)
        {
          if (_context.Assignments == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Assignments'  is null.");
          }
            assignment.Guid=Guid.NewGuid();
            String username = HttpContext.User?.Identity?.Name??"";
            var _user= _context.Users.FirstOrDefault(x => x.UserName == username);
            if (_user==null)
            {
                return BadRequest();
            }
            assignment.Guid = Guid.NewGuid();
            assignment.AssignmentOwenr = Guid.Parse(_user.Id);
            assignment.DateCreate = DateTime.Now;
            assignment.DateUpdate = DateTime.Now;
            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Assignments/5
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{UserRoles.Leader}, {UserRoles.Admin}")]
        public async Task<IActionResult> DeleteAssignment(Guid id)
        {
            if (_context.Assignments == null)
            {
                return NotFound();
            }
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }

            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [Authorize]
        private bool AssignmentExists(Guid id)
        {
            return (_context.Assignments?.Any(e => e.Guid == id)).GetValueOrDefault();
        }
    }
}
