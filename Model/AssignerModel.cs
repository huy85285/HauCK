using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HauCK.Model;

public class AssignerModel
{
    public Guid Guid { get; set; }
    public Guid TaskId { get; set; }
    public Guid UserId { get; set; }
    public Guid UserUpdateId { get; set; }
    public DateTime CreateTime { get; set; }
    public Guid RoleAssignmentID { get; set; }
    public Guid AssignmentID { get; set; }
}
