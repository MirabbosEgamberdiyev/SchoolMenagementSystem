

using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public class StudentAttendanceRepository : Repository<StudentAttendance>, IStudentAttendanceRepository
{
    public StudentAttendanceRepository(SchoolDbContext dbContext) : base(dbContext)
    {
    }
}
