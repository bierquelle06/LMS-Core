using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LMS.Common.Core.Mvc;
using LMS.Common.Model;
using LMS.Services.Core.Service;
using LMS.Services.Core.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using LMS.Common.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using LMS.Services.Core.Domain.Identity;
using LMS.Services.Core.Dto.Identity;
using System.Text.Json;

namespace LMS.Services.Core.Webapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LmsController : BaseController
	{
		private readonly ILogger<LmsController> _logger;

		private readonly IRabbitPublisher _rabbitPublisher;
		private readonly IIdentityService _identityService;
		private readonly IConfiguration _configuration;
		private readonly IDistributedCache _distributedCache;

		public LmsController(ILogger<LmsController> logger,
			IRabbitPublisher rabbitPublisher, IIdentityService identityService,
			IConfiguration configuration,
			IDistributedCache distributedCache)
		{
			_logger = logger;
			_rabbitPublisher = rabbitPublisher;
			_identityService = identityService;
			_configuration = configuration;
			_distributedCache = distributedCache;
		}

		[HttpGet("GetEducationByUser")]
		public async Task<IActionResult> GetEducationByUser([FromQuery] string query)
		{
			OperationResult<bool> operationResult = new OperationResult<bool>();

			var cacheKey = query.ToLower();

			var result = await _distributedCache.GetAsync(cacheKey);

			if (result != null)
			{

			}
			else
			{

			}

			return Ok(operationResult);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost("GetDepartmantByCode")]
		public async Task<IActionResult> GetDepartmantByCode([FromBody] string code)
		{
			OperationResult<DepartmantDto> operationResult = new OperationResult<DepartmantDto>();

			operationResult = (await _identityService.GetDepartmantByCodeAsync(code));

			return Ok(operationResult);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost("GetUserByUsername")]
		public async Task<IActionResult> GetUserByUsername([FromBody] string username)
		{
			OperationResult<UserDto> operationResult = new OperationResult<UserDto>();

			operationResult = (await _identityService.GetUserByUsernameAsync(username));

			return Ok(operationResult);
		}
		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="queryList"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost("CreateDepartment")]
		public async Task<IActionResult> CreateDepartment(List<DepartmantDto> queryList)
		{
			OperationResult<bool> operationResult = new OperationResult<bool>();

			// Query
			var UserId = Guid.NewGuid();
			var SubscriptionId = Guid.NewGuid();
			var DepartmantId = Guid.NewGuid();
			var PageIndex = 0;
			var PageSize = 0;
			var Culture = "";
			var OrderBy = "";
			Common.Model.Constants.OrderDir OrderDir = Common.Model.Constants.OrderDir.Asc;

			for (int i = 0; i < queryList.Count; i++)
			{
				var newDepartmant = queryList[i];

				var query = new Query<DepartmantDto>(newDepartmant)
				{
					UserId = UserId,
					SubscriptionId = SubscriptionId,
					DepartmantId = DepartmantId,
					PageIndex = PageIndex,
					PageSize = PageSize,
					Culture = Culture,
					OrderBy = OrderBy,
					OrderDir = OrderDir
				};

				await _identityService.AddDepartmantAsync(query);
			}

			operationResult.Result = true;
			operationResult.Message = "";

			return Ok(operationResult);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userDtos"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost("CreateUser")]
		public async Task<IActionResult> CreateUser(List<UserDto> userDtos)
		{
			OperationResult<bool> operationResult = new OperationResult<bool>();

			// Query
			var UserId = Guid.NewGuid();
			var SubscriptionId = Guid.NewGuid();
			var DepartmantId = Guid.NewGuid();
			var PageIndex = 0;
			var PageSize = 0;
			var Culture = "";
			var OrderBy = "";
			Common.Model.Constants.OrderDir OrderDir = Common.Model.Constants.OrderDir.Asc;

			for (int i = 0; i < userDtos.Count; i++)
			{
				var newUser = userDtos[i];

				var query = new Query<UserDto>(newUser)
				{
					UserId = UserId,
					SubscriptionId = SubscriptionId,
					DepartmantId = DepartmantId,
					PageIndex = PageIndex,
					PageSize = PageSize,
					Culture = Culture,
					OrderBy = OrderBy,
					OrderDir = OrderDir
				};

				await _identityService.AddUserAsync(query);
			}

			operationResult.Result = true;
			operationResult.Message = "";

			return Ok(operationResult);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="departmantEducationDtos"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost("CreateDepartmantEducation")]
		public async Task<IActionResult> CreateDepartmantEducation(List<DepartmantEducationDto> departmantEducationDtos)
		{
			OperationResult<bool> operationResult = new OperationResult<bool>();

			// Query
			var UserId = Guid.NewGuid();
			var SubscriptionId = Guid.NewGuid();
			var DepartmantId = Guid.NewGuid();
			var PageIndex = 0;
			var PageSize = 0;
			var Culture = "";
			var OrderBy = "";
			Common.Model.Constants.OrderDir OrderDir = Common.Model.Constants.OrderDir.Asc;

			for (int i = 0; i < departmantEducationDtos.Count; i++)
			{
				var newDepartmantEducation = departmantEducationDtos[i];

				var query = new Query<DepartmantEducationDto>(newDepartmantEducation)
				{
					UserId = UserId,
					SubscriptionId = SubscriptionId,
					DepartmantId = DepartmantId,
					PageIndex = PageIndex,
					PageSize = PageSize,
					Culture = Culture,
					OrderBy = OrderBy,
					OrderDir = OrderDir
				};

				await _identityService.AddDepartmantEducationAsync(query);
			}

			operationResult.Result = true;
			operationResult.Message = "";

			return Ok(operationResult);
		}

		/// <summary>
		/// GetDepartmantEducationResultByCode
		/// </summary>
		/// <param name="departmantEducationDtos"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost("GetDepartmantEducationResultByUser")]
		public async Task<IActionResult> GetDepartmantEducationResultByUser(DepartmantEducationsResultDto departmantEducationDtos)
		{
			OperationResult<List<DepartmantEducationsResultDto>> operationResult = new OperationResult<List<DepartmantEducationsResultDto>>();

			var cachedResponse = await _distributedCache.GetStringAsync("GenerateAndStore".ToLower());
			if (cachedResponse != null)
			{
				operationResult = JsonSerializer.Deserialize<OperationResult<List<DepartmantEducationsResultDto>>>(cachedResponse);

				if(operationResult.Response.Count > 0)
					operationResult.Response = operationResult.Response.Where(x => x.UserId == departmantEducationDtos.UserId).ToList();
			}
			else
				operationResult = await _identityService.GetDepartmantEducationsResultByUserIdAsync(departmantEducationDtos.UserId);

			operationResult.Result = true;
			operationResult.Message = "";

			return Ok(operationResult);
		}

		[AllowAnonymous]
		[HttpPost("GenerateAndStore")]
		public async Task<IActionResult> GenerateAndStore(string query)
		{
			OperationResult<List<DepartmantEducationsResultDto>> operationResult = new OperationResult<List<DepartmantEducationsResultDto>>();

			operationResult = await _identityService.GenerateAndStoreAsync();

			if(operationResult.Result)
			{
				if((query ?? "").Equals("CacheClear"))
					await _distributedCache.RemoveAsync("GenerateAndStore".ToLower());

				var cachedResponse = await _distributedCache.GetStringAsync("GenerateAndStore".ToLower());
				if(cachedResponse != null)
					operationResult = JsonSerializer.Deserialize<OperationResult<List<DepartmantEducationsResultDto>>>(cachedResponse);
				else
				{
					var response = JsonSerializer.Serialize(operationResult);

					await _distributedCache.SetStringAsync("GenerateAndStore".ToLower(), response);
				}
			}

			return Ok(operationResult);
		}
	}
}