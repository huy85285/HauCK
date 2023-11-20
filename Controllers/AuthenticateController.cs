using Azure;
using HauCK.Data;
using HauCK.Entiities;
using HauCK.Enum;
using HauCK.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HauCK.Controllers;

public class AuthenticateController : Controller
{
    private readonly UserManager<Entiities.User> _userManager;
    private readonly RoleManager<IdentityRole> _RoleManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;
    private readonly SignInManager<User> _signInManager;

    public AuthenticateController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, ApplicationDbContext db,RoleManager<IdentityRole> RoleManager)
    {
        _RoleManager= RoleManager;
        _userManager = userManager;
        _signInManager= signInManager;
        _configuration = configuration;
        _context = db;
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (_userManager.Users.Count()==0)
        {
            
            await _RoleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            User userNew = new User()
            {
                Email = "string@gmail.com",
                UserName = "string",
                Address = "address",
                DateOfBirth= DateTime.Now,
                FistName="string",
                LastName= "string",
                PhoneNumber= "string",                
            };
            var result = await _userManager.CreateAsync(userNew, "@As123");
            await _userManager.AddToRoleAsync(userNew, UserRoles.Admin);
        }
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }

}
