using Task_Management_System.Context;
using Task_Management_System.Repositories;
using Task_Management_System.Repository;
using Task_Management_System.Shared;

namespace Task_Management_System.UnitOfWork
{
    public class TMSUnitOfWork : UOW<ApplicationDbContext>, ITMSUnitOfWork
    {
        public TMSUnitOfWork(ApplicationDbContext context) : 
         base(context) 
        {
            _context = context;
            IUserRepo = new UserRepository(context);
            IUserTasksRepo = new UserTaskRepository(context);
            IStatusTasksRepo = new StatusTaskRepository(context);
        }

        public IUserRepository IUserRepo { get; }
        public IUserTaskRepository IUserTasksRepo { get; }
        public IStatusTaskRepository IStatusTasksRepo { get; }
    }
}


