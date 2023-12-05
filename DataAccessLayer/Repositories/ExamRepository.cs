

using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public class ExamRepository : Repository<Exam>, IExamRepository
{
    public ExamRepository(SchoolDbContext dbContext) : base(dbContext)
    {
    }
}
