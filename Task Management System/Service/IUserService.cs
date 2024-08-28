using Task_Management_System.Core.Dto;
using Task_Management_System.Core.ModelEntity;
using Task_Management_System.Shared.Pagination;

namespace Task_Management_System.Service
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string username, string password);
        Task<PaginationDto<UserDto>> GetAllUsers(string? search, int pageIndex, int pageSize);
        Task<CreateandUpdateUserDto> CreateUser(CreateandUpdateUserDto dto);
        Task<GetUserDto> GetUserById(int Id);
        Task<bool> DeleteUser(int Id);
        Task<CreateandUpdateUserDto> UpdateUser(CreateandUpdateUserDto dto, int Id);
        Task<List<UserDto>> GetAllUserByUserTaskId(int Id);
    }
}


