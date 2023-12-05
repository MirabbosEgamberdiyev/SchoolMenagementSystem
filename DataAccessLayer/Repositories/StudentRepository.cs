

using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    public StudentRepository(SchoolDbContext dbContext) : base(dbContext)
    {
    }
}
