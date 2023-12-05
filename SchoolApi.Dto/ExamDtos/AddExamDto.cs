
namespace SchoolApi.Dto.ExamDtos;

public class AddExamDto
{
    public int ClassId { get; set; }

    public int SubjectId { get; set; }

    /// <summary>
    /// Jami ball
    /// </summary>
    public int TotalMarks { get; set; }

    //Belgilardan tashqari
    public int OutOfMarks { get; set; }
}
