using System.ComponentModel.DataAnnotations;

namespace HauCK.Entiities
{
    public class RoleAssignment
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public String Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string Describe { get; set; }
    }
}
