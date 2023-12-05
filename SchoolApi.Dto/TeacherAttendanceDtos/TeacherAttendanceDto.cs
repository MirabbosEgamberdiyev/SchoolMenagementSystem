namespace SchoolApi.Dto.TeacherAttendanceDtos;

public class TeacherAttendanceDto:BaseEntityDto
{
    public int TeacherId { get; set; }

    public bool? Status { get; set; }

    public DateTime? Date { get; set; }
}
