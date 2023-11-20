using HauCK.Data;
using HauCK.Entiities;
using HauCK.Extends;
using HauCK.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Pkcs;

namespace HauCK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CommentModel>>> GetComments()
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            return await _context.Comments.Convert();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentModel>> GetComment(Guid id)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment.Convert();
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(Guid id, CommentModel comment)
        {
            if (id != comment.Guid)
            {
                return BadRequest();
            }

            _context.Entry(comment.Convert()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(CommentModel comment)
        {
            if (_context.Comments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Comments'  is null.");
            }
            comment.dateTime = DateTime.Now;
            String username = HttpContext.User?.Identity?.Name ?? "";
            var _user = _context.Users.FirstOrDefault(x => x.UserName == username);
            if (_user == null)
            {
                return BadRequest();
            }
            comment.UserId = Guid.Parse(_user.Id);

            _context.Comments.Add(comment.Convert());
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.Guid }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(Guid id)
        {
            return (_context.Comments?.Any(e => e.Guid == id)).GetValueOrDefault();
        }
    }
}
