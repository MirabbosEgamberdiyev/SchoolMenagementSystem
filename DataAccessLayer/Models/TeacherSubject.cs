

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace DataAccessLayer.Models;

public class TeacherSubject:BaseModel
{
    [Required]
    public int ClassId { get; set; }
    [Required]
    public int SubjectId { get; set; }
    [Required]
    public int TeacherId { get; set; }

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    
    [ForeignKey("ClassId")]
    public virtual Class Class { get; set; }

    [ForeignKey("SubjectId")]
    public virtual Subject Subject { get; set; }

    [ForeignKey("TeacherId")]
    public virtual Teacher Teacher { get; set; }


}
