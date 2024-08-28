using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Task_Management_System.Context;
using Task_Management_System.Core.Dto;
using Task_Management_System.Core.ModelEntity;
using Task_Management_System.Repository;
using Task_Management_System.Shared.Pagination;
using Task_Management_System.UnitOfWork;

namespace Task_Management_System.Service
{
    public class UserTaskService : IUserTaskService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITMSUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserTaskService(IMapper mapper, ITMSUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateTaskAsync(UserTaskDto Dto)
        {
            var user = _mapper.Map<UserTask>(Dto);

            user.CreatedOn = DateTime.Now;
            user.CreatedBy = "USER ADMIN";

            await _unitOfWork.IUserTasksRepo.AddAsync(user);
            await _unitOfWork.CommitAsync();

            return user.UserId;
        }

        public async Task<bool> DeleteTask(int id)
        {
            var task = _unitOfWork.IUserTasksRepo.GetById(id)
             ?? throw new Exception("Id not Found");

            task.IsActive = false;
            task.UpdatedOn = DateTime.Now;
            task.UpdatedBy = "USER ADMIN";

            _unitOfWork.IUserTasksRepo.Update(task);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<PaginationDto<UserTaskDto>> GetAllTasks(string? search, int pageIndex, int pageSize)
        {
            
            var result = await _unitOfWork.IUserTasksRepo
                .GetPagination(
                    x => x.IsActive && (string.IsNullOrEmpty(search) || x.NameTask.Contains(search)),
                    pageIndex: pageIndex,
                    pageSize: pageSize,
                    orderBy: o => o.OrderByDescending(x => x.UserTaskId)
                );

            var mappedResult = _mapper.Map<List<UserTaskDto>>(result.Result);

            return new PaginationDto<UserTaskDto>
            {
                Result = mappedResult,
                Total = result.Total,
            };
        }

        public async Task<List<UserTaskDto>> GetTasksByUserIdAsync(int userId)
        {
            var tasks = await _unitOfWork.IUserTasksRepo.GetByIdAsync(userId);
            if (tasks == null)
                return new List<UserTaskDto>();

            return _mapper.Map<List<UserTaskDto>>(tasks);
        }

        public async Task<CreateAndUpdateTaskDto> UpdateTask(CreateAndUpdateTaskDto dto, int id)
        {
            
            var task = await _unitOfWork.IUserTasksRepo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("TaskId not found.");

            _mapper.Map(dto, task);

            task.UpdatedOn = DateTime.Now; 
            task.UpdatedBy = "USER ADMIN";

            _unitOfWork.IUserTasksRepo.Update(task);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<CreateAndUpdateTaskDto>(task);
        }

        public async Task<bool> UpdateStatusAndAddNewAsync(int userTaskId, int TaskStatusTypeId)
        {
            
            var currentStatus = await _unitOfWork.IStatusTasksRepo.GetStatusTaskByUserTaskId(userTaskId);
            
            if (currentStatus != null)
            {
                
                currentStatus.isCurrent = false;
                _unitOfWork.IStatusTasksRepo.Update(currentStatus);
            }

            var newStatus = new StatusTask
            {
                UserTaskId = userTaskId,
                TaskStatusTypeId = TaskStatusTypeId,
                isCurrent = true,
                UpdatedOn = DateTime.Now,
                UpdatedBy = "USER ADMIN"
            };

            await _unitOfWork.IStatusTasksRepo.AddAsync(newStatus);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<List<StatusTaskDto>> GetHistoryStatusTask(int taskId)
        {
            var statusTasks = await _unitOfWork.IStatusTasksRepo.GetListAsync(
                t => t.UserTaskId == taskId,
                query => query.Include(t => t.TaskStatusType)
                .Include(t => t.UserTask));

            if (!statusTasks.Any())
                return new List<StatusTaskDto>();

            var statusTaskDtos = _mapper.Map<List<StatusTaskDto>>(statusTasks);

            
            foreach (var dto in statusTaskDtos)
            {
                dto.CreatedOn = DateTime.Now.ToString(); 
                dto.CreatedBy = "USER ADMIN"; 
            }

            return statusTaskDtos;
        }

    }
}
