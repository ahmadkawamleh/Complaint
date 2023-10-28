using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure
{
    public abstract class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity> where TEntity : BaseEntity
    where TContext : DbContext
    {
        protected readonly DbSet<TEntity> _dbSet;
        public GenericRepository(TContext context)
        {
            _dbSet = context.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var entity = await _dbSet.FindAsync(id, cancellationToken);
            _dbSet.Remove(entity);
        }

        public async Task<TEntity> FindById(int id, string userId, List<string> includes, CancellationToken cancellationToken)
        {
            var query = _dbSet
                .Where(x => x.Id == id && x.UserId == userId);

            if (includes != null)
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

            return await query.SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<List<TEntity>> GetAll(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> filter,  List<string> includes, CancellationToken cancellationToken)
        {
            var query = _dbSet
                .Where(filter)
                .OrderByDescending(x => x.CreationDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (includes != null)
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

            return await query.ToListAsync(cancellationToken);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
