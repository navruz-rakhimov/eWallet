using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Application.Common.Interfaces.Persistence
{
    public interface IBaseRepository<TEntity, TIndex>
        where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity item, CancellationToken cancellationToken = default);
        Task AddManyAsync(IList<TEntity> items, CancellationToken cancellationToken = default);
        IQueryable<TEntity> GetAllAsQueryable();
        Task<TEntity?> GetAsync(TIndex id, IEnumerable<Expression<Func<TEntity, object>>>? includeExpressions = null, CancellationToken cancellationToken = default);
        Task<IList<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
        Task<bool> TryRemoveAsync(TIndex id, bool removeRelated = true, CancellationToken cancellationToken = default);
        Task RemoveAsync(TEntity item, bool removeRelated = true, CancellationToken cancellationToken = default);
        Task<bool> TryRemoveManyAsync(IList<TIndex> ids, bool removeRelated = true, CancellationToken cancellationToken = default);
        Task RemoveManyAsync(IList<TEntity> items, bool removeRelated = true, CancellationToken cancellationToken = default);
        Task<int> RemoveByFilterAsync(Expression<Func<TEntity, bool>> filter, bool removeRelated = true, CancellationToken cancellationToken = default);
        Task<bool> SaveChangesAsync();
    }
}
