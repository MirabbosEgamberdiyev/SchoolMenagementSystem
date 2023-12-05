

using SchoolApi.Dto.StudentAttendanceDtos;

namespace BusinessLogicLayer.Interfaces;

public interface IStudentAttendanceService
{
    Task<List<StudentAttendanceDto>> GetAllStudentAttendancesAsync(); 
    Task<StudentAttendanceDto> GetStudentAttendanceByIdAsync(int id);
    Task AddStudentAttendanceAsync(AddStudentAttendanceDto studentAttendance);
    Task DeleteStudentAttendanceAsync(int id);
    Task UpdateStudentAttendanceAsync(StudentAttendanceDto studentAttendanceDto);
}
