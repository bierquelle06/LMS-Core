using LMS.Common.Model;
using LMS.Services.Core.Dto.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestWebApp.ApiService
{
	public class LMSWebApiService
	{
        // <summary>
        ///
        /// </summary>
        private readonly HttpClient _httpClient;
        /// <summary>
        ///
        /// </summary>
        private readonly ILogger<LMSWebApiService> _logger;
        /// <summary>
        ///
        /// </summary>
        /// <param name="httpClient"></param>
        public LMSWebApiService(HttpClient httpClient, ILogger<LMSWebApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        public async Task<OperationResult<bool>> CreateDepartment(List<string> departments)
        {
            string apiControllerName = "Lms";
            string methodName = "CreateDepartment";

            //New List Departmants
            List<DepartmantDto> newListDepartmants = new List<DepartmantDto>();

            for (int i = 0; i < departments.Count; i++)
			{
                DepartmantDto departmantDto = new DepartmantDto();

                departmantDto.Id = Guid.NewGuid();
                departmantDto.Title = departments[i];
                departmantDto.Code = departments[i].ToLower().Trim();
                departmantDto.Status = LMS.Common.Model.Constants.Status.Active;
                departmantDto.SubscriptionId = Guid.NewGuid();

                newListDepartmants.Add(departmantDto);
            }

            var result = (await new LMS.Common.Core.Utils.WebApiHelper<List<DepartmantDto>, OperationResult<bool>>(
                _httpClient.BaseAddress.AbsoluteUri, 
                apiControllerName, 
                _logger).PostListAsync(newListDepartmants, methodName));

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<OperationResult<UserDto>> GetUserByUsername(string username)
        {
            string apiControllerName = "Lms";
            string methodName = "GetUserByUsername";

            var result = (await new LMS.Common.Core.Utils.WebApiHelper<string, OperationResult<UserDto>>
                (_httpClient.BaseAddress.AbsoluteUri,
                apiControllerName,
                _logger).PostListAsync(username, methodName));

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<OperationResult<DepartmantDto>> GetDepartmantByCode(string code)
        {
            string apiControllerName = "Lms";
            string methodName = "GetDepartmantByCode";

            var result = (await new LMS.Common.Core.Utils.WebApiHelper<string, OperationResult<DepartmantDto>>
                (_httpClient.BaseAddress.AbsoluteUri,
                apiControllerName,
                _logger).PostListAsync(code, methodName));

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<OperationResult<List<DepartmantEducationsResultDto>>> GetDepartmantEducationResultByUser(DepartmantEducationsResultDto educationsResultDto)
        {
            string apiControllerName = "Lms";
            string methodName = "GetDepartmantEducationResultByUser";

            var result = (await new LMS.Common.Core.Utils.WebApiHelper<DepartmantEducationsResultDto, OperationResult<List<DepartmantEducationsResultDto>>>
                (_httpClient.BaseAddress.AbsoluteUri,
                apiControllerName,
                _logger).PostListAsync(educationsResultDto, methodName));

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDtos"></param>
        /// <returns></returns>
        public async Task<OperationResult<bool>> CreateUser(List<UserDto> userDtos)
        {
            string apiControllerName = "Lms";
            string methodName = "CreateUser";

            var result = (await new LMS.Common.Core.Utils.WebApiHelper<List<UserDto>, OperationResult<bool>>
                (_httpClient.BaseAddress.AbsoluteUri, 
                apiControllerName, 
                _logger).PostListAsync(userDtos, methodName));

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmantEducationDtos"></param>
        /// <returns></returns>
        public async Task<OperationResult<bool>> CreateDepartmantEducation(List<DepartmantEducationDto> departmantEducationDtos)
        {
            string apiControllerName = "Lms";
            string methodName = "CreateDepartmantEducation";

            var result = (await new LMS.Common.Core.Utils.WebApiHelper<List<DepartmantEducationDto>, OperationResult<bool>>
                (_httpClient.BaseAddress.AbsoluteUri,
                apiControllerName,
                _logger).PostListAsync(departmantEducationDtos, methodName));

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<OperationResult<List<DepartmantEducationsResultDto>>> GenerateAndStore(string parameter)
        {
            string apiControllerName = "Lms";
            string methodName = "GenerateAndStore";

            var result = (await new LMS.Common.Core.Utils.WebApiHelper<string, OperationResult<List<DepartmantEducationsResultDto>>>
                (_httpClient.BaseAddress.AbsoluteUri,
                apiControllerName,
                _logger).PostListAsync(parameter, methodName));

            return result;
        }
    }
}
