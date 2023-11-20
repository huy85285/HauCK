using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HauCK.Entiities
{
    public class Assigner
    {
        [Key]
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public Guid TaskId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid UserUpdateId { get; set; }
        public DateTime CreateTime { get; set; }
        [Required]
        public Guid RoleAssignmentID { get; set; }
        public Guid AssignmentID { get; set; }
    }
}
