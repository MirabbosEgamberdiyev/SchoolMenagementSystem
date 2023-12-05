
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public class ScholarshipRepository : Repository<Scholarship>, IScholarshipRepository
{
    public ScholarshipRepository(SchoolDbContext dbContext) : base(dbContext)
    {
    }
}
