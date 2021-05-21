using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LMS.Common.Model;
using LMS.Services.Core.Domain.Identity;
using LMS.Services.Core.Dto.Identity;

namespace LMS.Services.Core.Service.Abstract
{
    public interface IIdentityService
    {

        Task<OperationResult<SubscriptionDto>> AddSubscriptionAsync(Query<SubscriptionDto> query);
        Task<OperationResult<SubscriptionDto>> UpdateSubscriptionAsync(Query<SubscriptionDto> query);
        Task<OperationResult<SubscriptionDto>> GetSubscriptionAsync(Query<SubscriptionDto> query);


        //User
        Task<OperationResult<UserDto>> AddUserAsync(Query<UserDto> query);
        Task<OperationResult<UserDto>> UpdateUserAsync(Query<UserDto> query);
        Task<OperationResult<UserDto>> GetUserAsync(Query<UserDto> query);
        Task<OperationResult<UserDto>> GetUserByUsernameAsync(string username);

        //Departmant
        Task<OperationResult<DepartmantDto>> AddDepartmantAsync(Query<DepartmantDto> query);
        Task<OperationResult<DepartmantDto>> UpdateDepartmantAsync(Query<DepartmantDto> query);
        Task<OperationResult<DepartmantDto>> GetDepartmantAsync(Query<DepartmantDto> query);
        Task<OperationResult<DepartmantDto>> GetDepartmantByCodeAsync(string code);

        //Departmant Education
        Task<OperationResult<DepartmantEducationDto>> AddDepartmantEducationAsync(Query<DepartmantEducationDto> query);
        Task<OperationResult<DepartmantEducationDto>> UpdateDepartmantEducationAsync(Query<DepartmantEducationDto> query);
        Task<OperationResult<DepartmantEducationDto>> GetDepartmantEducationAsync(Query<DepartmantEducationDto> query);

        //Departmant Eductions Result
        Task<OperationResult<DepartmantEducationsResultDto>> AddDepartmantEducationsResultAsync(Query<DepartmantEducationsResultDto> query);
        Task<OperationResult<DepartmantEducationsResultDto>> GetDepartmantEducationsResultByUsernameAsync(string username, string departmantCode, string educationCode);
        Task<OperationResult<List<DepartmantEducationsResultDto>>> GetDepartmantEducationsResultByUserIdAsync(Guid userId);

        Task<OperationResult<List<DepartmantEducationsResultDto>>> GenerateAndStoreAsync();
    }
}