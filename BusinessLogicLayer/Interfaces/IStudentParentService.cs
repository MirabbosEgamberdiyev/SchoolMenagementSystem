


using SchoolApi.Dto.StudentParentDtos;

namespace BusinessLogicLayer.Interfaces;

public interface IStudentParentService
{
    Task<List<StudentParentDto>> GetAllStudentParentsAsync();
    Task<StudentParentDto> GetStudentParentByIdAsync(int id);
    Task AddStudentParentAsync(AddStudentParentDto studentDto);
    Task DeleteStudentParentAsync(int id);
    Task UpdateStudentParentAsync(StudentParentDto studentDto);
}
