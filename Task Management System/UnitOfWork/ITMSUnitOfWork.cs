using System.Threading.Tasks;
using Task_Management_System.Context;
using Task_Management_System.Repositories;
using Task_Management_System.Repository;
using Task_Management_System.Shared.Interface;

namespace Task_Management_System.UnitOfWork
{
    public interface ITMSUnitOfWork : IUOW<ApplicationDbContext>
    {
        public IUserRepository IUserRepo { get; }
        public IUserTaskRepository IUserTasksRepo { get; }
        public IStatusTaskRepository IStatusTasksRepo { get; }
        
    }
}


