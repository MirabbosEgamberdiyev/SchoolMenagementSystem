
using SchoolApi.Dto.StudentDtos;

namespace BusinessLogicLayer.Interfaces;

public interface IStudentService
{
    Task<List<StudentDto>> GetAllStudentsAsync();
    Task<StudentDto> GetStudentByIdAsync(int id);
    Task AddStudentAsync(AddStudentDto studentDto);
    Task DeleteStudentAsync(int id);
    Task UpdateStudentAsync(StudentDto studentDto);
}
