using AutoMapper;

using LMS.Services.Core.Domain.Identity;
using LMS.Services.Core.Dto.Identity;
using LMS.Services.Core.Dto.Messages.Events.Identity;

namespace LMS.Services.Core.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // IDENTITY
            CreateMap<Departmant, DepartmantDto>();
            CreateMap<DepartmantDto, Departmant>();

            CreateMap<DepartmantEducation, DepartmantEducationDto>();
            CreateMap<DepartmantEducationDto, DepartmantEducation>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Subscription, SubscriptionDto>();
            CreateMap<SubscriptionDto, Subscription>();

            CreateMap<DepartmantEducationsResult, DepartmantEducationsResultDto>();
            CreateMap<DepartmantEducationsResultDto, DepartmantEducationsResult>();


            // MESSAGING 
            CreateMap<DepartmantAdded, DepartmantDto>();
            CreateMap<DepartmantDto, DepartmantAdded>();

            CreateMap<DepartmantEducationAdded, DepartmantEducationDto>();
            CreateMap<DepartmantEducationDto, DepartmantEducationAdded>();

            CreateMap<UserAdded, UserDto>();
            CreateMap<UserDto, UserAdded>();

            CreateMap<SubscriptionAdded, SubscriptionDto>();
            CreateMap<SubscriptionDto, SubscriptionAdded>();


            CreateMap<DepartmantUpdated, DepartmantDto>();
            CreateMap<DepartmantDto, DepartmantUpdated>();

            CreateMap<DepartmantEducationUpdated, DepartmantDto>();
            CreateMap<DepartmantDto, DepartmantEducationUpdated>();

            CreateMap<UserUpdated, UserDto>();
            CreateMap<UserDto, UserUpdated>();

            CreateMap<SubscriptionUpdated, SubscriptionDto>();
            CreateMap<SubscriptionDto, SubscriptionUpdated>();
        }
    }

}
