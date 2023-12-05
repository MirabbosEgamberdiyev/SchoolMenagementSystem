



using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models;

public class StudentAttendance: BaseModel
{
    [Required]
    public int ClassId { get; set; }
    [Required]
    public int SubjectId { get; set; }

    [Required]
    public int StudentId { get; set; }
    public bool? Status { get; set; }
    public DateTime Date { get; set; }

    // Navigation properties
    public virtual Class Class { get; set; } = new Class();
    public virtual Subject Subject { get; set; } = new Subject();
    public virtual Student Student { get; set; } = new Student();
}
