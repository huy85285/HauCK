using HauCK.Entiities;
using HauCK.Enum;
using HauCK.Extends;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HauCK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<Entiities.User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // GET api/<UserController>/5
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var users = _userManager.Users;
            return Ok(users.Convert());
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(string id)
        {
            var userById = _userManager.FindByIdAsync(id);
            var userByName = _userManager.FindByNameAsync(id);
            if (userById == null&& userByName==null)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }
    }
}
