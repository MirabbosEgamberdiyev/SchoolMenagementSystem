


using SchoolApi.Dto.ExamDtos;

namespace BusinessLogicLayer.Interfaces;

public interface IExamService
{
    Task<List<ExamDto>> GetExamsAsync();
    Task<ExamDto> GetExamByIdAsync(int id);
    Task AddExamAsync(AddExamDto newExam);
    Task UpdateExamAsync(ExamDto examDto);
    Task DeleteExamAsync(int id);
}
