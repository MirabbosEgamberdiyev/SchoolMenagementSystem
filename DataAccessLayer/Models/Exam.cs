

using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models;

public class Exam: BaseModel
{

    [Required]
    public int ClassId { get; set; }
    [Required]
    public int SubjectId { get; set; }
    public string RollNo { get; set; } = string.Empty;
    public int TotalMarks { get; set; }
    public int OutOfMarks { get; set; }

    // Navigation properties
    public virtual Class Class { get; set; }
    public virtual Subject Subject { get; set; }
    public virtual Student Student { get; set; }
}
