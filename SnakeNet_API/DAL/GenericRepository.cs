using Microsoft.EntityFrameworkCore;
using SnakeNet_API.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SnakeNet_API.DAL
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
	{
		private readonly AppDbContext _context;
		private readonly DbSet<TEntity> _dbSet;

		public GenericRepository(AppDbContext context)
		{
			_context = context;
			_dbSet = context.Set<TEntity>();
		}

		public async virtual Task<IEnumerable<TEntity>> GetAsync(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = "")
		{
			IQueryable<TEntity> query = _dbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			foreach (var includeProperty in includeProperties.Split
				(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			if (orderBy != null)
			{
				return await orderBy(query).ToListAsync();
			}
			else
			{
				return await query.ToListAsync();
			}
		}

		public async virtual Task<TEntity> GetByIDAsync(object id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async virtual Task InsertAsync(TEntity entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public async virtual Task DeleteAsync(object id)
		{
			TEntity entityToDelete = await _dbSet.FindAsync(id);
			if (entityToDelete != null)
			{
				Delete(entityToDelete);
			}
		}

		public virtual void Delete(TEntity entityToDelete)
		{
			if (_context.Entry(entityToDelete).State == EntityState.Detached)
			{
				_dbSet.Attach(entityToDelete);
			}
			_dbSet.Remove(entityToDelete);
		}

		public async virtual Task UpdateAsync(TEntity entityToUpdate)
		{
			_dbSet.Attach(entityToUpdate);
			_context.Entry(entityToUpdate).State = EntityState.Modified;
		}
	}
}
