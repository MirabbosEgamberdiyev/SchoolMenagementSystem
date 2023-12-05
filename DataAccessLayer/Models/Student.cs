
using DataAccessLayer.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace DataAccessLayer.Models;

public class Student: BaseModel
{
    [Required, MinLength(3), MaxLength(50)]
    public string FirsName { get; set; } = string.Empty;

    [MinLength(3), MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
    //Birthday 

    public DateTime? DOB { get; set; }

    public string Gender { get; set; }
    [MaxLength(13)]
    public string? Mobile { get; set; }
    public string RollNo { get; set; }
    [MaxLength(1000)]
    public string Address { get; set; }
    [Required]
    public int? ClassId { get; set; }

    [ForeignKey("ClassId")]
    public virtual Class Class { get; set; }
    public virtual ICollection<StudentParent> StudentParents { get; set; } = new List<StudentParent>();
    public virtual ICollection<Scholarship> Scholarships { get; set; } = new List<Scholarship>();
    public virtual ICollection<StudentAttendance> StudentAttendances { get; set; } = new List<StudentAttendance>();
}
