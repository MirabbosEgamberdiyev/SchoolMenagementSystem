

namespace SchoolApi.Dto.TeacherAttendanceDtos;

public class AddTeacherAttendanceDto
{
    public int TeacherId { get; set; }

    public bool? Status { get; set; }

    public DateTime? Date { get; set; }
}
