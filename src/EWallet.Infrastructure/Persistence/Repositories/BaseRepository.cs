using EWallet.Application.Common.Interfaces.Persistence;
using EWallet.Domain.Common;
using EWallet.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EWallet.Infrastructure.Persistence.Repositories
{
    public abstract class BaseRepository<TEntity, TIndex> : IBaseRepository<TEntity, TIndex>
        where TEntity : BaseEntity<TIndex>
        where TIndex : struct
    {
        protected BaseRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected ApplicationDbContext DbContext { get; }

        protected abstract DbSet<TEntity> DbSet { get; }

        public virtual async Task<TEntity> AddAsync(TEntity item, CancellationToken cancellationToken = default)
        {
            var createdItem = await DbSet.AddAsync(item, cancellationToken);
            return createdItem.Entity;
        }

        public virtual async Task AddManyAsync(IList<TEntity> items, CancellationToken cancellationToken = default)
        {
            await DbSet.AddRangeAsync(items, cancellationToken);
        }

        public virtual IQueryable<TEntity> GetAllAsQueryable() => DbSet;

        public virtual async Task<TEntity?> GetAsync(TIndex id, IEnumerable<Expression<Func<TEntity, object>>>? includeExpressions = null, CancellationToken cancellationToken = default)
        {
            var entitySet = DbSet.AsQueryable();

            if (includeExpressions != null && includeExpressions.Any())
            {
                entitySet = includeExpressions.Aggregate(
                    entitySet,
                    (current, include) => current.Include(include));
            }

            return await entitySet.FirstOrDefaultAsync(org => org.Id.Equals(id), cancellationToken);
        }

        public virtual async Task<IList<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(filter).ToListAsync(cancellationToken);
        }

        public virtual async Task<bool> TryRemoveAsync(TIndex id, bool removeRelated = true, CancellationToken cancellationToken = default)
        {
            var entity = await DbSet.FirstOrDefaultAsync(org => org.Id.Equals(id), cancellationToken);

            if (entity != null)
            {
                await RemoveAsync(entity, removeRelated, cancellationToken);
                return true;
            }

            return false;
        }

        public virtual async Task RemoveAsync(TEntity item, bool removeRelated = true, CancellationToken cancellationToken = default)
        {
            DbSet.Remove(item);

            if (removeRelated)
            {
                await RemoveRelatedItemsAsync(item);
            }
        }

        public virtual async Task<bool> TryRemoveManyAsync(IList<TIndex> ids, bool removeRelated = true, CancellationToken cancellationToken = default)
        {
            var entitiesToDelete = await DbSet.Where(x => ids.Contains(x.Id))
                .ToListAsync(cancellationToken);

            if (entitiesToDelete.Any())
            {
                await RemoveManyAsync(entitiesToDelete, removeRelated, cancellationToken);
                return true;
            }

            return false;
        }

        public virtual async Task RemoveManyAsync(IList<TEntity> items, bool removeRelated = true, CancellationToken cancellationToken = default)
        {
            foreach (var item in items)
            {
                await RemoveAsync(item, removeRelated, cancellationToken);
            }
        }

        public async Task<int> RemoveByFilterAsync(Expression<Func<TEntity, bool>> filter, bool removeRelated = true, CancellationToken cancellationToken = default)
        {
            var entities = await GetByFilterAsync(filter, cancellationToken);

            if (!entities.Any())
            {
                return 0;
            }

            await RemoveManyAsync(entities, removeRelated, cancellationToken);

            return entities.Count;
        }

        public virtual async Task<bool> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync() > 0;
        }

        protected abstract Task RemoveRelatedItemsAsync(TEntity entity);
    }

}
