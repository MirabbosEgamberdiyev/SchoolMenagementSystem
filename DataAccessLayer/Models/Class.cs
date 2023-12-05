using DataAccessLayer.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models;

public class Class: BaseModel
{
    [Required, MinLength(3), MaxLength(100)]
    public string ClassName { get; set; } = string.Empty;



    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Scholarship> Scholarships { get; set; } = new List<Scholarship>();

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual ICollection<Fees> Fees { get; set; } = new List<Fees>();

    public virtual ICollection<StudentAttendance> StudentAttendances { get; set; } = new List<StudentAttendance>();

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();

}
