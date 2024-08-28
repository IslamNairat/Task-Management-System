using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Task_Management_System.Context;
using Task_Management_System.Core.ModelEntity;
using Task_Management_System.Shared;

namespace Task_Management_System.Repositories
{
    public class StatusTaskRepository : BaseRepository<StatusTask>, IStatusTaskRepository
    {
        private readonly ApplicationDbContext _ApplicationDbContext;
        public StatusTaskRepository(ApplicationDbContext ApplicationDbContext) : base(ApplicationDbContext)
        {
            _ApplicationDbContext = ApplicationDbContext;
        }

        public async Task<StatusTask?> GetStatusTaskByUserTaskId(int userTaskId)
        {
            return (await _ApplicationDbContext.StatusTasks.FirstOrDefaultAsync(x => x.UserTaskId == userTaskId && x.isCurrent));
        }

        public async Task AddAsync(StatusTask newStatus)
        {
            await _ApplicationDbContext.AddAsync(newStatus);
        }

        public void Update(StatusTask currentStatus)
        {
            var existingStatus = _ApplicationDbContext.Update(currentStatus);
        }

        
        public Task<List<StatusTask>> GetListAsync(
        Expression<Func<StatusTask, bool>> predicate,
        Func<IQueryable<StatusTask>, IQueryable<StatusTask>> include = null)
        {
            IQueryable<StatusTask> query = _ApplicationDbContext.StatusTasks.Where(predicate);

            if (include != null)
            {
                query = include(query);
            }

            return query.ToListAsync();
        }
    }
}

