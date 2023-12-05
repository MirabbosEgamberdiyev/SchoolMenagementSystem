

using SchoolApi.Dto.TeacherDtos;

namespace BusinessLogicLayer.Interfaces;

public interface ITeacherService
{
    Task<List<TeacherDto>> GetAllTeacherAsync();
    Task<TeacherDto> GetTeacherByIdAsync(int id);
    Task AddTeacherAsync(AddTeacherDto newTeacher);
    Task DeleteTeacherAsync(int id);
    Task UpdateTeacherAsync(TeacherDto teacherDto);
}
