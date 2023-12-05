

using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public class TeacherRepository : Repository<Teacher>, ITeacherRepository
{
    public TeacherRepository(SchoolDbContext dbContext) : base(dbContext)
    {
    }
}
