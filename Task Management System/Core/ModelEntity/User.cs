using System.ComponentModel.DataAnnotations;

namespace Task_Management_System.Core.ModelEntity
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordUser { get; set; }
        public ICollection<UserTask> Tasks { get; set; }
    }
}
