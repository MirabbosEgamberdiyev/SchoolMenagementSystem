
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class Repository<TEntity>
    : IRepository<TEntity> where TEntity : BaseModel
{
    private readonly SchoolDbContext _dbContext;

    public Repository(SchoolDbContext dbContext)
    {
            _dbContext = dbContext;
    }


    public async Task AddAsync(TEntity entity)
        => await _dbContext.Set<TEntity>().AddAsync(entity);

    public void Delete(int id)
    {
        var entity = _dbContext.Set<TEntity>()
                               .FirstOrDefault(i =>
                                        i.Id == id);
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var list = _dbContext.Set<TEntity>()
                             .AsNoTracking()
                             .AsEnumerable();
        return Task.FromResult(list);
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        var entity = await _dbContext.Set<TEntity>()
                                     .FirstOrDefaultAsync(i =>
                                        i.Id == id);
        if (entity == null)
        {
            throw new ArgumentNullException();
        }

        return entity;
    }

    public void Update(TEntity entity)
        => _dbContext.Set<TEntity>().Update(entity);
}
