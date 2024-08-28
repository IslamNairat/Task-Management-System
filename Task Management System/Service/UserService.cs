using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Task_Management_System.Core.Dto;
using Task_Management_System.Core.ModelEntity;
using Task_Management_System.Repository;
using Task_Management_System.Shared.Pagination;
using Task_Management_System.UnitOfWork;

namespace Task_Management_System.Service
{
    public class UserService : IUserService
    {
        private readonly ITMSUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(ITMSUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            return await _unitOfWork.IUserRepo.GetUserByUsernameAndPassword(username, password); 
        }

        public async Task<CreateandUpdateUserDto> CreateUser(CreateandUpdateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);

            user.CreatedOn = DateTime.Now;
            user.CreatedBy = "USER ADMIN";
            await _unitOfWork.IUserRepo.AddAsync(user);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<CreateandUpdateUserDto>(user);
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = _unitOfWork.IUserRepo.GetById(id)
                ?? throw new KeyNotFoundException($"User with Id {id} not found.");

           
            user.IsActive = false; 
            user.UpdatedOn = DateTime.Now;
            user.UpdatedBy = "USER ADMIN"; 
            _unitOfWork.IUserRepo.Update(user);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<List<UserDto>> GetAllUserByUserTaskId(int Id)
        {
            var users = await _unitOfWork.IUserRepo.GetListAsync(u => u.Tasks.Any(x => x.UserTaskId == Id))
            ?? throw new Exception("Id not Found");

            if (users == null || !users.Any())
                return new List<UserDto>();

            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<PaginationDto<UserDto>> GetAllUsers(string? search, int pageIndex, int pageSize)
        {
            var result = (await _unitOfWork.IUserRepo
                .GetPagination(i => i.IsActive && (string.IsNullOrEmpty(search) || i.Username.Contains(search)), pageIndex: pageIndex, pageSize: pageSize, orderBy: o => o.OrderByDescending(i => i.UserId)));

            return new PaginationDto<UserDto>
            {
                Result = _mapper.Map<List<UserDto>>(result.Result),
                Total = result.Total,
            };
        }

        public async Task<GetUserDto> GetUserById(int id)
        {
            var user = await _unitOfWork.IUserRepo.Get(u => u.UserId == id && u.IsActive);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with Id {id} not found or is not active.");
            }
            
            return _mapper.Map<GetUserDto>(user);
        }

        public async Task<CreateandUpdateUserDto> UpdateUser(CreateandUpdateUserDto dto, int Id)
        {
            var user = _unitOfWork.IUserRepo.GetById(Id);
            _mapper.Map(dto, user);
            user.UpdatedOn = DateTime.Now;
            user.UpdatedBy = "USER ADMIN";

            _unitOfWork.IUserRepo.Update(user);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CreateandUpdateUserDto>(dto);
        }
    }
}


