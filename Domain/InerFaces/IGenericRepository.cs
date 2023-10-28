using Domain.Entities;
using System.Linq.Expressions;

namespace Domain
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        void Add(TEntity entity);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<TEntity> FindById(int id, string UserId, List<string> includes, CancellationToken cancellationToken);
        Task<List<TEntity>> GetAll(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> filter, List<string> includes, CancellationToken cancellationToken);
        void Update(TEntity entity);
    }
}