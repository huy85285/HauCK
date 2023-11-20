using HauCK.Data;
using HauCK.Entiities;
using HauCK.Extends;
using HauCK.Model;
using Microsoft.AspNetCore.Mvc;

namespace HauCK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AssignersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Assigners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignerModel>>> GetAssigners()
        {
            if (_context.Assigners == null)
            {
                return NotFound();
            }
            return await _context.Assigners.Convert();
        }

        // GET: api/Assigners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AssignerModel>> GetAssigner(Guid id)
        {
            if (_context.Assigners == null)
            {
                return NotFound();
            }
            var assigner = await _context.Assigners.FindAsync(id);

            if (assigner == null)
            {
                return NotFound();
            }

            return assigner.Convert();
        }



        // POST: api/Assigners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Assigner>> PostAssigner(AssignerModel assigner)
        {
            if (_context.Assigners == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Assigners'  is null.");
            }
            String username = HttpContext.User?.Identity?.Name ?? "";
            var _user = _context.Users.FirstOrDefault(x => x.UserName == username);
            if (_user == null)
            {
                return BadRequest();
            }
            assigner.CreateTime = DateTime.Now;
            assigner.AssignmentID = Guid.Parse(_user.Id);
            _context.Assigners.Add(assigner.Convert());
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssigner", new { id = assigner.Guid }, assigner);
        }

        // DELETE: api/Assigners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssigner(Guid id)
        {
            if (_context.Assigners == null)
            {
                return NotFound();
            }
            var assigner = await _context.Assigners.FindAsync(id);
            if (assigner == null)
            {
                return NotFound();
            }

            _context.Assigners.Remove(assigner);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AssignerExists(Guid id)
        {
            return (_context.Assigners?.Any(e => e.Guid == id)).GetValueOrDefault();
        }
    }
}
