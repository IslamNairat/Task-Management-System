using Microsoft.EntityFrameworkCore;
using Task_Management_System.Core.ModelEntity;
using Task_Management_System.Context;
using Task_Management_System.Shared;

namespace Task_Management_System.Repository
{
    public class UserTaskRepository : BaseRepository<UserTask>, IUserTaskRepository
    {
        private readonly ApplicationDbContext _ApplicationDbContext;
        public UserTaskRepository(ApplicationDbContext ApplicationDbContext) : base(ApplicationDbContext)
        {
            _ApplicationDbContext = ApplicationDbContext;
        }
    }
}

