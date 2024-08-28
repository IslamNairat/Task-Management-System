using AutoMapper;
using Task_Management_System.Core.Dto;
using Task_Management_System.Core.ModelEntity;

namespace Task_Management_System.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<UserTaskDto, UserTask>().ReverseMap();
            CreateMap<User, GetUserDto>().ReverseMap();
            CreateMap<CreateandUpdateUserDto, User>()
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore()) 
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()); 
            CreateMap<User, CreateandUpdateUserDto>();
            CreateMap<CreateAndUpdateTaskDto, UserTask>()
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
            CreateMap<UserTask, CreateAndUpdateTaskDto>();
            CreateMap<StatusTask, StatusTaskDto>()
            .ForMember(dest => dest.Name,opt => opt.MapFrom(src => src.TaskStatusType.Name))
            .ForMember(dest => dest.NameTask,opt => opt.MapFrom(src => src.UserTask.NameTask));
        }
    }
}


