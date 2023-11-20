using Azure;
using HauCK.Data;
using HauCK.Entiities;
using HauCK.Enum;
using HauCK.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HauCK.Controllers;
public class RegisterController : Controller
{
    private readonly UserManager<Entiities.User> _userManager;
    private readonly RoleManager<IdentityRole> _RoleManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;
    private readonly SignInManager<User> _signInManager;

    public RegisterController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, ApplicationDbContext db, RoleManager<IdentityRole> RoleManager)
    {
        _RoleManager = RoleManager;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _context = db;
    }
    [HttpPost]
    [Route("register-admin")]
    [Authorize(Roles = $"{UserRoles.Admin}")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
    {
        var userExists = await _userManager.FindByNameAsync(model.Username);
        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User already exists!" });

        User user = new User()
        {
            Email = model.Email,
            UserName = model.Username,
            Address = model.Address,
            DateOfBirth = model.DateOfBirth,
            FistName = model.FistName,
            LastName = model.LastName,
            PhoneNumber = model.NumberPhone,
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User creation failed! Please check user details and try again." });

        await _userManager.AddToRoleAsync(user, model.Role);

        return Ok(new ResponseModel { Status = "Success", Message = "User created successfully!" });
    }
}
