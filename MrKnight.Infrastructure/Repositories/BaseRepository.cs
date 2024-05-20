using MrKnight.Core.Repositories.Interfaces;
using MrKnight.Infrastructure.Data;

namespace MrKnight.Infrastructure.Repositories;

public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
{
    protected readonly KnightPathDbContext _context;

    protected BaseRepository(KnightPathDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity?> GetAsync(TKey id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }
}