

using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public class TeacherSubjectRepository : Repository<TeacherSubject>, ITeacherSubjectRepository
{
    public TeacherSubjectRepository(SchoolDbContext dbContext) : base(dbContext)
    {
    }
}
