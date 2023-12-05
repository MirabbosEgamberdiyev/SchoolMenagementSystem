

namespace SchoolApi.Dto.StudentAttendanceDtos;

public class StudentAttendanceDto:BaseEntityDto
{
    public int ClassId { get; set; }

    public int SubjectId { get; set; }

    public int StudentId { get; set; }

    public bool? Status { get; set; }

    public DateTime Date { get; set; }
}
