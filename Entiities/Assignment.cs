using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HauCK.Entiities
{
    public class Assignment
    {
        [Key]
        [Required]
        public Guid Guid { get; set; }
        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Describe { get; set; }
        [Required]
        public DateTime DateStart { get; set; }
        [Required]
        public DateTime DateEnd { get; set; }
        public DateTime DateUpdate { get; set; }
        public DateTime DateCreate { get; set; }
        [Required]
        public Guid AssignmentOwenr { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
