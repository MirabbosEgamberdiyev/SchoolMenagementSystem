
using SchoolApi.Dto.TeacherAttendanceDtos;

namespace BusinessLogicLayer.Interfaces;

public interface ITeacherAttendanceService
{
    Task<List<TeacherAttendanceDto>> GetAllTeacherAttendanceAsync();
    Task<TeacherAttendanceDto> GetTeacherAttendanceByIdAsync(int id);
    Task AddTeacherAttendanceAsync(AddTeacherAttendanceDto newTeacherAttendance);
    Task UpdateTeacherAttendanceAsync(TeacherAttendanceDto teacherAttendanceDto);
    Task DeleteTeacherAttendanceAsync(int id);
}
