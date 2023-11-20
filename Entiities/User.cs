using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HauCK.Entiities
{
    public class User:IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FistName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [MaxLength(15)]
        public int NumberPhone { get; set; }
        [MaxLength(50)]
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
