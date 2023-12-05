
using SchoolApi.Dto.SubjectDtos;

namespace BusinessLogicLayer.Interfaces;

public interface ISubjectService
{
    Task<List<SubjectDto>> GetAllSubjectAsync();
    Task<SubjectDto> GetSubjectByIdAsync(int id);
    Task AddSubjectAsync(AddSubjectDto newSubjectDto);
    Task DeleteSubjectAsync(int id);
    Task UpdateSubjectAsync(SubjectDto subjectDto);
}
