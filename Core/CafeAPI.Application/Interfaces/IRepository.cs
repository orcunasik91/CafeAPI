namespace CafeAPI.Application.Interfaces;
public interface IRepository<TEntity> where TEntity : class, new()
{
    Task<TEntity> GetByIdAsync(int id);
    Task<List<TEntity>> GetAllAsync();
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
}