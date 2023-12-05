
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccessLayer.Models;

public class Subject: BaseModel
{

    [Required]
    public int ClassId { get; set; }
    [Required, MinLength(3), MaxLength(100)]
    public string SubjectName { get; set; } = string.Empty;

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
    public virtual ICollection<StudentAttendance> StudentAttendances { get; set; } = new List<StudentAttendance>();

    public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();

    [ForeignKey("ClassId")]
    public virtual Class Class { get; set; }
}
