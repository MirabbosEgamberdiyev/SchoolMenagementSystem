
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public class TeacherAttendanceRepository : Repository<TeacherAttendance>, ITeacherAttendanceRepository
{
    public TeacherAttendanceRepository(SchoolDbContext dbContext) : base(dbContext)
    {
    }
}
