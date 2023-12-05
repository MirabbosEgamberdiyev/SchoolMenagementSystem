
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models;

public class TeacherAttendance:BaseModel
{
    [Required]
    public int TeacherId { get; set; }
    public bool? Status { get; set; }
    public DateTime? Date { get; set; }

    [ForeignKey("TeacherId")]
    public virtual Teacher Teacher { get; set; } = new Teacher();
}
