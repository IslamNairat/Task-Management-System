﻿namespace Task_Management_System.Core.Dto
{
    public class CreateAndUpdateTaskDto
    {
        public string NameTask { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
    }
}
