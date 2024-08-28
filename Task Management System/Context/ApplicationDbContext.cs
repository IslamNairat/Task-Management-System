using Microsoft.EntityFrameworkCore;
using Task_Management_System.Core.ModelEntity;

namespace Task_Management_System.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<TaskStatusType> TaskStatusType { get; set; }
        public DbSet<StatusTask> StatusTasks { get; set; }

    }
}
