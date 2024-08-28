namespace Task_Management_System.Core.ModelEntity
{
    public class StatusTask : BaseEntity
    {
        public int StatusTaskId { get; set; }
        public int UserTaskId { get; set; }
        public UserTask UserTask { get; set; }
        public int TaskStatusTypeId { get; set; }
        public TaskStatusType TaskStatusType { get; set; }
        public bool isCurrent { get; set; }
    }
}
