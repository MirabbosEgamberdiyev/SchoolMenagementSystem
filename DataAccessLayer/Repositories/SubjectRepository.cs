
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public class SubjectRepository : Repository<Subject>, ISubjectRepository
{
    public SubjectRepository(SchoolDbContext dbContext) : base(dbContext)
    {
    }
}
