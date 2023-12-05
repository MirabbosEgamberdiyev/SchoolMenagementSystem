


using DataAccessLayer.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models;

public class Teacher: BaseModel
{
    [Required, MinLength(3), MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [MinLength(3), MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [ForeignKey(nameof(Subject))]
    public int SubjectId { get; set; }
    //Birthday
    public DateTime? DOB { get; set; }

    public Gender Gender { get; set; }

    [MaxLength(13)]
    public string PhoneNumber { get; set; } = string.Empty;

    [MinLength(5), MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Address { get; set; } = string.Empty;

    [MaxLength(30), MinLength(6)]
    public string Password { get; set; } = string.Empty;
}
