

namespace SchoolApi.Dto.TeacherDtos;

public class AddTeacherDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public int SubjectId { get; set; }
    //Birthday
    public DateTime? DOB { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
