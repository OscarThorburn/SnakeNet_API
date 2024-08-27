using System.Linq.Expressions;

namespace SnakeNet_API.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
	{
        void Delete(TEntity entityToDelete);
        Task DeleteAsync(object id);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<TEntity> GetByIDAsync(object id);
        Task InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entityToUpdate);
    }
}