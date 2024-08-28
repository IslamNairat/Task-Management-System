using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Task_Management_System.Core.ModelEntity;
using Task_Management_System.Shared.Pagination;

namespace Task_Management_System.Shared.Interface
{
    public interface IBaseRepository<TEntity>
    {
        Task AddAsync(TEntity objModel);
        void AddRange(IEnumerable<TEntity> objModel);
        TEntity? GetById(int id);
        Task<TEntity?> GetByIdAsync(int id);
        Task<UserTask> GetByIdAsync(int Id, bool includeRelated = false);
        Task<TEntity?> Get(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        int Count();
        Task<int> CountAsync();
        void Update(TEntity objModel);
        void Remove(TEntity objModel);
        Task<PaginationDto<TEntity>> GetPagination(Expression<Func<TEntity, bool>> predicate = null,
        int? pageIndex = 1,
        int? pageSize = 10,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);

    }
}
