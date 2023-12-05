
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public class FeesRepository : Repository<Fees>, IFeesRepository
{
    public FeesRepository(SchoolDbContext dbContext) : base(dbContext)
    {
    }
}
