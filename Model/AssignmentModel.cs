using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HauCK.Model;

public class AssignmentModel
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Describe { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public Guid AssignmentOwenr { get; set; }
    public int Status { get; set; }
    public DateTime DateUpdate { get; set; }
    public DateTime DateCreate { get; set; }
}
