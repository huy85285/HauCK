using System.ComponentModel.DataAnnotations;

namespace HauCK.Model;

public class RoleAssignmentModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Describe { get; set; }
}
