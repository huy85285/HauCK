using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HauCK.Model;

public class UserModel
{
    public string FistName { get; set; }
    public string LastName { get; set; }
    public int NumberPhone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
}
