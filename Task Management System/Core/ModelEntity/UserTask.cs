

namespace Task_Management_System.Core.ModelEntity
{
    public class UserTask : BaseEntity
    {
        public int UserTaskId { get; set; }
        public string NameTask { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
