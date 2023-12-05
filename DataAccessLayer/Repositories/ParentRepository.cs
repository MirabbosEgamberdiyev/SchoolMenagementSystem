
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public class ParentRepository : Repository<Parent>, IParentRepository
{
    public ParentRepository(SchoolDbContext dbContext) : base(dbContext)
    {
    }
}
