using HauCK.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HauCK.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _RoleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _RoleManager = roleManager;
        }

        [HttpGet]
        [Route("role")]
        [Authorize(Roles = $"{UserRoles.Leader}, {UserRoles.Admin}")]
        public async Task<IActionResult> getAll()
        {
            return Ok(
            new
                {
                    ListNV = _RoleManager.Roles.Select(x => new { x.Name }).ToList(),
                });
        }
    }
}
