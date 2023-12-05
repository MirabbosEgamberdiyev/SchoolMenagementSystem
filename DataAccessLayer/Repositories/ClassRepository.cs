

using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public class ClassRepository : Repository<Class>, IClassRepository
{
    public ClassRepository(SchoolDbContext dbContext) : base(dbContext)
    {
    }
}
