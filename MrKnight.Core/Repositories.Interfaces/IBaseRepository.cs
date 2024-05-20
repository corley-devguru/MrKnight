namespace MrKnight.Core.Repositories.Interfaces;

public interface IBaseRepository<TEntity, TKey> where TEntity : class
{
    Task AddAsync(TEntity entity);
    Task<TEntity?> GetAsync(TKey id);
}