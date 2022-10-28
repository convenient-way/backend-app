using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using unitofwork_core.Entities;
using unitofwork_core.Model.Collection;

namespace unitofwork_core.Core.IRepository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity? GetById(Guid id);
        TEntity Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);
        void Insert(IList<TEntity> entities);
        bool Delete(Guid id);
        void DeleteRange(IEnumerable<TEntity> entities);
        void DeleteRange(IList<TEntity> entities);
        TEntity Update(TEntity entity);
        void Update(IEnumerable<TEntity> entites);
        Task<IList<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<TEntity?> GetByIdAsync(Guid id, Func<IQueryable<TEntity>,
            IIncludableQueryable<TEntity, object?>>? include = null, bool disableTranking = true);
        Task<TEntity> InsertAsync(TEntity entity);
        Task InsertAsync(IEnumerable<TEntity> entities);
        Task InsertAsync(IList<TEntity> entities);
        Task<bool> DeleteAsync(Guid id);

        PaginatedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
            int pageIndex = 0, int pageSize = 20,
            bool disableTracking = true, bool ignoreQueryFilters = false);

        PaginatedList<TResult> GetPagedList<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
            int pageIndex = 0, int pageSize = 20, bool disableTracking = true, 
            bool ignoreQueryFilters = false) where TResult : class;

        Task<PaginatedList<TEntity>> GetPagedListAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
            int pageIndex = 0, int pageSize = 20, bool disableTracking = true,
            CancellationToken cancellationToken = default(CancellationToken),
            bool ignoreQueryFilters = false);

        Task<PaginatedList<TEntity>> GetPagedListAsync(
            List<Expression<Func<TEntity, bool>>>? predicates = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
            int pageIndex = 0, int pageSize = 20, bool disableTracking = true,
            CancellationToken cancellationToken = default(CancellationToken),
            bool ignoreQueryFilters = false);

        Task<PaginatedList<TResult>> GetPagedListAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
            int pageIndex = 0, int pageSize = 20, bool disableTracking = true,
            CancellationToken cancellationToken = default(CancellationToken),
            bool ignoreQueryFilters = false) where TResult : class;
        Task<PaginatedList<TResult>> GetPagedListAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            List<Expression<Func<TEntity, bool>>>? predicates = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
            int pageIndex = 0, int pageSize = 20, bool disableTracking = true,
            CancellationToken cancellationToken = default(CancellationToken),
            bool ignoreQueryFilters = false) where TResult : class;

        IQueryable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
            bool disableTracking = true,
            bool ignoreQueqyFilter = false);

        Task<IList<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
            bool disableTracking = true,
            bool ignoreQueryFilter = false);

        Task<IList<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);
    }
}
