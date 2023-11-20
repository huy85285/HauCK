using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HauCK.Model;

public class CommentModel
{
    public Guid Guid { get; set; }
    public Guid Assignment { get; set; }
    public Guid UserId { get; set; }
    public DateTime dateTime { get; set; }
    public string Content { get; set; }
}
