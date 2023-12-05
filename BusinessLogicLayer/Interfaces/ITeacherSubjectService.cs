

using SchoolApi.Dto.TeacherSubjectDtos;

namespace BusinessLogicLayer.Interfaces;

public interface ITeacherSubjectService
{
    Task<List<TeacherSubjectDto>> GetAllTeacherSubjectAsync();
    Task<TeacherSubjectDto> GetTeacherSubjectByIdAsync(int id);
    Task AddTeacherSubjectAsync(AddTeacherSubjectDto newTeacherSubject);
    Task UpdateTeacherSubjectAsync(TeacherSubjectDto teacherSubjectDto);
    Task DeleteTeacherSubjectAsync(int id);
}
