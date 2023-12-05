

using DataAccessLayer.Enums;
using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Dto.StudentDtos;

public class StudentDto:BaseEntityDto
{

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    //Birthday 
    public DateTime? DOB { get; set; }

    public Gender Gender { get; set; }

    public string? PhoneNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public int? ClassId { get; set; }
}
