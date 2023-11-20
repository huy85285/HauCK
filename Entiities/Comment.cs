using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HauCK.Entiities
{
    public class Comment
    {
        [Key]
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public Guid Assignment { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        [Column(TypeName = "DateTime")]
        public DateTime dateTime { get; set; }
        [Required]
        [Column(TypeName = "nvarchar")]
        [MaxLength(100)]
        public string Content { get; set; }
    }
}
