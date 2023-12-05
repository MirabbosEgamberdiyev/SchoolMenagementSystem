namespace SchoolApi.Dto.TeacherSubjectDtos;

public class TeacherSubjectDto:BaseEntityDto
{
    public int ClassId { get; set; }

    public int SubjectId { get; set; }

    public int TeacherId { get; set; }

}
