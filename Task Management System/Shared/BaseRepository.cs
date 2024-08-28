using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Task_Management_System.Context;
using Task_Management_System.Shared.Interface;
using Task_Management_System.Core.ModelEntity;
using Task_Management_System.Shared.Pagination;
using Task_Management_System.Shared.Helper;

namespace Task_Management_System.Shared
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<TEntity> DbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();

        }
        public async Task AddAsync(TEntity model)
        {
            _context.Set<TEntity>().Add(model);

        }

        public void AddRange(IEnumerable<TEntity> model)
        {
            _context.Set<TEntity>().AddRange(model);

        }

        public TEntity? GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }
        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<UserTask> GetByIdAsync(int userId, bool includeRelated = false)
        {
            IQueryable<UserTask> query = _context.UserTasks.AsQueryable();

            if (includeRelated)
            {
                query = query.Include(f => f.UserId);
                            
            }

            return await query.SingleOrDefaultAsync(f => f.UserId == userId);
        }


        public async Task<TEntity?> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where<TEntity>(predicate).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Run(() => _context.Set<TEntity>().Where<TEntity>(predicate));
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Task.Run(() => _context.Set<TEntity>());
        }

        public int Count()
        {
            return _context.Set<TEntity>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<TEntity>().CountAsync();
        }

        public void Update(TEntity objModel)
        {
            _context.Entry(objModel).State = EntityState.Modified;
        }

        public void Remove(TEntity objModel)
        {
            _context.Set<TEntity>().Remove(objModel);

        }

        public async Task<PaginationDto<TEntity>> GetPagination(Expression<Func<TEntity, bool>> predicate = null,
        int? pageIndex = 1,
        int? pageSize = 10,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            var results = DbSet.AsQueryable();
            if (includes != null)
                results = includes(results);

            int count = 0;
            if (predicate != null)
                results = results.Where(predicate);

            if (orderBy != null)
            {
                results = orderBy(results);
            }
            if (pageIndex.GetValueOrDefault() > 0 && pageSize.GetValueOrDefault() > 0)
            {
                count = await results.CountAsync();
                results = results.Pagination(pageIndex.GetValueOrDefault(), pageSize.GetValueOrDefault());
            }

            return new PaginationDto<TEntity>()
            {
                Result = await results.ToListAsync(),
                Total = count
            };
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            var results = DbSet.AsQueryable();
            if (includes != null)
                results = includes(results);

            if (predicate != null)
                results = results.Where(predicate);

            return await results.ToListAsync();
        }
    }
}
