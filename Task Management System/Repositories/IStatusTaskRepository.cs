using System.Linq.Expressions;
using Task_Management_System.Core.ModelEntity;
using Task_Management_System.Shared.Interface;

namespace Task_Management_System.Repositories
{
    public interface IStatusTaskRepository : IBaseRepository<StatusTask>
    {
        Task<StatusTask?> GetStatusTaskByUserTaskId(int userTaskId);
        Task AddAsync(StatusTask newStatus);
        void Update(StatusTask currentStatus);
        
    }
}
