namespace Task_Management_System.Core.Dto
{
    public class StatusTaskDto
    {
        public int UserTaskId { get; set; }
        public string NameTask { get; set; }
        public string Name { get; set; }
        public bool isCurrent { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
    }
}
