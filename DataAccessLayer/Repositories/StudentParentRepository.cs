
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public class StudentParentRepository : Repository<StudentParent>, IStudentParentRepository
{
    public StudentParentRepository(SchoolDbContext dbContext) : base(dbContext)
    {
    }
}
