using Task_Management_System.Core.Dto;
using Task_Management_System.Core.ModelEntity;
using Task_Management_System.Shared.Pagination;

namespace Task_Management_System.Service
{
    public interface IUserTaskService
    {
        Task<PaginationDto<UserTaskDto>> GetAllTasks(string? search, int pageIndex, int pageSize);
        Task<int> CreateTaskAsync(UserTaskDto Dto);
        Task<List<UserTaskDto>> GetTasksByUserIdAsync(int userId);
        Task<CreateAndUpdateTaskDto> UpdateTask(CreateAndUpdateTaskDto dto, int Id);
        Task<bool> DeleteTask(int id);
        Task<bool> UpdateStatusAndAddNewAsync(int userTaskId, int newTaskStatusTypeId);
        Task<List<StatusTaskDto>> GetHistoryStatusTask(int taskId);
    }
}
