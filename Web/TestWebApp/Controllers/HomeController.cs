using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.Models;

namespace TestWebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApiService.LMSWebApiService _lmsWebApiService;

		public HomeController(ILogger<HomeController> logger, ApiService.LMSWebApiService lmsWebApiService)
		{
			_logger = logger;
			_lmsWebApiService = lmsWebApiService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		/// <summary>
		/// Create Department
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> CreateDepartment()
		{
			List<string> departmants = new List<string>();

			departmants.Add("Genel Cerrahi");
			departmants.Add("İç Hastalıklar");
			departmants.Add("Kadın Hastalıkları");

			var apiResult = await _lmsWebApiService.CreateDepartment(departmants);

			if (apiResult.Result)
				_logger.LogDebug("Success :: CreateDepartment");
			else
				_logger.LogWarning("Failed :: CreateDepartment");

			return Ok(apiResult);
		}

		/// <summary>
		/// Create User
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> CreateUser()
		{
			var userList = new List<LMS.Services.Core.Dto.Identity.UserDto>();

			var departmantResult1 = await _lmsWebApiService.GetDepartmantByCode("Genel Cerrahi".ToLower().Trim());
			if (departmantResult1.Result)
			{
				userList.Add(new LMS.Services.Core.Dto.Identity.UserDto()
				{
					Email = "aykut.aktas@test.com",
					FullName = "Aykut AKTAŞ",
					Status = LMS.Common.Model.Constants.UserStatus.Active,
					SubscriptionId = departmantResult1.Response.SubscriptionId,
					Username = "aykut.aktas"
				});
			}

			var departmantResult2 = await _lmsWebApiService.GetDepartmantByCode("İç Hastalıklar".ToLower().Trim());
			if (departmantResult2.Result)
			{
				userList.Add(new LMS.Services.Core.Dto.Identity.UserDto()
				{
					Email = "okan.yildiz@test.com",
					FullName = "Okan YILDIZ",
					Status = LMS.Common.Model.Constants.UserStatus.Active,
					SubscriptionId = departmantResult2.Response.SubscriptionId,
					Username = "okan.yildiz"
				});
			}

			var apiResult = await _lmsWebApiService.CreateUser(userList);

			if (apiResult.Result)
				_logger.LogDebug("Success :: CreateUser");
			else
				_logger.LogWarning("Failed :: CreateUser");

			return Ok(apiResult);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> CreateDepartmantEducation()
		{
			var departmantEducationList = new List<LMS.Services.Core.Dto.Identity.DepartmantEducationDto>();

			var departmantResult1 = await _lmsWebApiService.GetDepartmantByCode("Genel Cerrahi".ToLower().Trim());
			if (departmantResult1.Result)
			{
				departmantEducationList.Add(new LMS.Services.Core.Dto.Identity.DepartmantEducationDto()
				{
					Code = "101",
					Title = "A Eğitim",
					DepartmantId = departmantResult1.Response.Id
				});

				departmantEducationList.Add(new LMS.Services.Core.Dto.Identity.DepartmantEducationDto()
				{
					Code = "102",
					Title = "B Eğitim",
					DepartmantId = departmantResult1.Response.Id
				});

				departmantEducationList.Add(new LMS.Services.Core.Dto.Identity.DepartmantEducationDto()
				{
					Code = "103",
					Title = "C Eğitim",
					DepartmantId = departmantResult1.Response.Id
				});
			}

			var departmantResult2 = await _lmsWebApiService.GetDepartmantByCode("İç Hastalıklar".ToLower().Trim());
			if (departmantResult2.Result)
			{
				departmantEducationList.Add(new LMS.Services.Core.Dto.Identity.DepartmantEducationDto()
				{
					Code = "101",
					Title = "A Eğitim",
					DepartmantId = departmantResult2.Response.Id
				});

				departmantEducationList.Add(new LMS.Services.Core.Dto.Identity.DepartmantEducationDto()
				{
					Code = "105",
					Title = "E Eğitim",
					DepartmantId = departmantResult2.Response.Id
				});

				departmantEducationList.Add(new LMS.Services.Core.Dto.Identity.DepartmantEducationDto()
				{
					Code = "106",
					Title = "F Eğitim",
					DepartmantId = departmantResult2.Response.Id
				});
			}

			var apiResult = await _lmsWebApiService.CreateDepartmantEducation(departmantEducationList);

			if (apiResult.Result)
				_logger.LogDebug("Success :: CreateDepartmantEducation");
			else
				_logger.LogWarning("Failed :: CreateDepartmantEducation");

			return Ok(apiResult);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> GetDepartmantEducation()
		{
			LMS.Services.Core.Dto.Identity.DepartmantEducationsResultDto dtoItem = new LMS.Services.Core.Dto.Identity.DepartmantEducationsResultDto();

			dtoItem.UserId = (await _lmsWebApiService.GetUserByUsername("aykut.aktas")).Response.Id;

			var apiResult = await _lmsWebApiService.GetDepartmantEducationResultByUser(dtoItem);

			if (apiResult.Result)
				_logger.LogDebug("Success :: CreateDepartment");
			else
				_logger.LogWarning("Failed :: CreateDepartment");

			return Ok(apiResult);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> GenerateAndStoreDepartmantEducation()
		{
			var apiResult = await _lmsWebApiService.GenerateAndStore("");

			if (apiResult.Result)
				_logger.LogDebug("Success :: GenerateAndStoreDepartmantEducation");
			else
				_logger.LogWarning("Failed :: GenerateAndStoreDepartmantEducation");

			return Ok(apiResult);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> GenerateAndStoreDepartmantEducationClear()
		{
			var apiResult = await _lmsWebApiService.GenerateAndStore("CacheClear");

			if (apiResult.Result)
				_logger.LogDebug("Success :: GenerateAndStoreDepartmantEducationClear");
			else
				_logger.LogWarning("Failed :: GenerateAndStoreDepartmantEducationClear");

			return Ok(apiResult);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
