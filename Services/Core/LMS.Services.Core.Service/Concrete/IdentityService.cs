using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using LMS.Common.Core.Sql;
using LMS.Common.Model;
using LMS.Services.Core.Domain.Identity;
using LMS.Services.Core.Dto.Identity;
using LMS.Services.Core.Repository.Abstract.Identity;
using LMS.Services.Core.Service.Abstract;
using System.Collections.Generic;

namespace LMS.Services.Core.Service.Concrete
{
	public class IdentityService : IIdentityService
	{
		// this service will just be used to pass values to repository 
		// because this data is validated and only come from internal messages


		private readonly IMapper _mapper;
		private readonly ISubscriptionRepository _subscriptionRepository;
		private readonly IDepartmantRepository _departmentRepository;
		private readonly IDepartmantEducationRepository _departmentEducationRepository;
		private readonly IDepartmantEducationsResultRepository _departmantEducationsResultRepository;
		private readonly IUserRepository _userRepository;
		private readonly ILogger<IdentityService> _logger;
		public IdentityService(
			ISubscriptionRepository subscriptionRepository,
			IDepartmantRepository departmentRepository,
			IDepartmantEducationRepository departmentEducationRepository,
			IDepartmantEducationsResultRepository departmantEducationsResultRepository,
			IUserRepository userRepository,
			IMapper mapper,
			ILogger<IdentityService> logger)
		{
			_logger = logger;
			_subscriptionRepository = subscriptionRepository;
			_departmentRepository = departmentRepository;
			_departmentEducationRepository = departmentEducationRepository;
			_departmantEducationsResultRepository = departmantEducationsResultRepository;
			_userRepository = userRepository;
			_mapper = mapper;

		}

		/// <summary>
		/// Add Departmant Async
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public async Task<OperationResult<DepartmantDto>> AddDepartmantAsync(Query<DepartmantDto> query)
		{
			OperationResult<DepartmantDto> result = new OperationResult<DepartmantDto>();
			try
			{
				var repQuery = new Query<Departmant>(_mapper.Map<Departmant>(query.Parameter));

				var subscriptionData = await _subscriptionRepository.GetAsync(x => x.Code == repQuery.Parameter.Code + "_subscription");

				var subscription = new Subscription();
				if (subscriptionData.Response == null)
				{
					var subResult = await _subscriptionRepository.AddAsync(new Subscription()
					{
						Id = Guid.NewGuid(),
						Title = repQuery.Parameter.Title + " Subscription",
						Code = repQuery.Parameter.Code + "_subscription",
						Status = Common.Model.Constants.Status.Active,
						Departmants = null,
						Users = null
					});

					if (subResult.Result)
						repQuery.Parameter.SubscriptionId = subResult.Response.Id;
				}
				else
					repQuery.Parameter.SubscriptionId = subscriptionData.Response.Id;


				var departmantData = await _departmentRepository.GetAsync(x => x.Code == repQuery.Parameter.Code);

				if (departmantData.Response == null)
				{
					var repResult = await _departmentRepository.AddAsync(repQuery.Parameter);

					result.Clone(repResult);
					if (result.Result)
					{
						result.Response = _mapper.Map<DepartmantDto>(repResult.Response);
					}
				}
				else
				{
					result.Clone(departmantData);
					if (result.Result)
					{
						result.Response = _mapper.Map<DepartmantDto>(departmantData.Response);
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exciption AddDepartmantAsync {@Query}", query);
				ex.HandelException(result);
			}
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public async Task<OperationResult<DepartmantEducationsResultDto>> AddDepartmantEducationsResultAsync(Query<DepartmantEducationsResultDto> query)
		{
			OperationResult<DepartmantEducationsResultDto> result = new OperationResult<DepartmantEducationsResultDto>();
			try
			{
				var repQuery = new Query<DepartmantEducationsResult>(_mapper.Map<DepartmantEducationsResult>(query.Parameter));

				var departmantEducationData = await _departmantEducationsResultRepository.GetAsync(x => x.UserId == repQuery.Parameter.UserId && x.DepartmantEducationId == repQuery.Parameter.DepartmantEducationId);

				if (departmantEducationData.Response == null)
				{
					var repResult = await _departmantEducationsResultRepository.AddAsync(repQuery.Parameter);

					result.Clone(repResult);
					if (result.Result)
					{
						result.Response = _mapper.Map<DepartmantEducationsResultDto>(repResult.Response);
					}
				}
				else
				{
					result.Clone(departmantEducationData);
					if (result.Result)
					{
						result.Response = _mapper.Map<DepartmantEducationsResultDto>(departmantEducationData.Response);
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exciption AddDepartmantEducationsResultAsync {@Query}", query);
				ex.HandelException(result);
			}
			return result;
		}

		/// <summary>
		/// Add Departmant Education Async
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public async Task<OperationResult<DepartmantEducationDto>> AddDepartmantEducationAsync(Query<DepartmantEducationDto> query)
		{
			OperationResult<DepartmantEducationDto> result = new OperationResult<DepartmantEducationDto>();
			try
			{
				var repQuery = new Query<DepartmantEducation>(_mapper.Map<DepartmantEducation>(query.Parameter));

				var departmantEducationData = await _departmentEducationRepository.GetAsync(x => x.Code == repQuery.Parameter.Code && x.DepartmantId == repQuery.Parameter.DepartmantId);

				if (departmantEducationData.Response == null)
				{
					var repResult = await _departmentEducationRepository.AddAsync(repQuery.Parameter);

					result.Clone(repResult);
					if (result.Result)
					{
						result.Response = _mapper.Map<DepartmantEducationDto>(repResult.Response);
					}
				}
				else
				{
					result.Clone(departmantEducationData);
					if (result.Result)
					{
						result.Response = _mapper.Map<DepartmantEducationDto>(departmantEducationData.Response);
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exciption AddDepartmantEducationAsync {@Query}", query);
				ex.HandelException(result);
			}
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public async Task<OperationResult<SubscriptionDto>> AddSubscriptionAsync(Query<SubscriptionDto> query)
		{
			OperationResult<SubscriptionDto> result = new OperationResult<SubscriptionDto>();
			try
			{
				var repQuery = new Query<Subscription>(_mapper.Map<Subscription>(query.Parameter));
				var repResult = await _subscriptionRepository.AddAsync(repQuery.Parameter);

				result.Clone(repResult);
				if (result.Result)
				{
					result.Response = _mapper.Map<SubscriptionDto>(repResult.Response);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exciption AddSubscriptionAsync {@Query}", query);
				ex.HandelException(result);
			}
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public async Task<OperationResult<UserDto>> AddUserAsync(Query<UserDto> query)
		{
			OperationResult<UserDto> result = new OperationResult<UserDto>();
			try
			{
				var repQuery = new Query<User>(_mapper.Map<User>(query.Parameter));

				var userData = await _userRepository.GetAsync(x => x.Email == repQuery.Parameter.Email && x.Username == repQuery.Parameter.Username);

				if (userData.Response == null)
				{
					var repResult = await _userRepository.AddAsync(repQuery.Parameter);

					result.Clone(repResult);
					if (result.Result)
					{
						result.Response = _mapper.Map<UserDto>(repResult.Response);
					}
				}
				else
				{
					result.Clone(userData);
					if (result.Result)
					{
						result.Response = _mapper.Map<UserDto>(userData.Response);
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exciption AddUserAsync {@Query}", query);
				ex.HandelException(result);
			}
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		public async Task<OperationResult<UserDto>> GetUserByUsernameAsync(string username)
		{
			OperationResult<UserDto> result = new OperationResult<UserDto>();
			try
			{
				var repResult = await _userRepository.GetAsync(x => x.Username == username);

				result.Clone(repResult);
				if (result.Result)
				{
					result.Response = _mapper.Map<UserDto>(repResult.Response);
				}

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exciption GetUserByUsernameAsync {@Username}", username);
				ex.HandelException(result);
			}
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public async Task<OperationResult<DepartmantDto>> GetDepartmantByCodeAsync(string code)
		{
			OperationResult<DepartmantDto> result = new OperationResult<DepartmantDto>();
			try
			{
				var repResult = await _departmentRepository.GetAsync(x => x.Code == code);

				result.Clone(repResult);
				if (result.Result)
				{
					result.Response = _mapper.Map<DepartmantDto>(repResult.Response);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exciption GetDepartmantByCodeAsync {@Code}", code);
				ex.HandelException(result);
			}
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<OperationResult<List<DepartmantEducationsResultDto>>> GetDepartmantEducationsResultByUserIdAsync(Guid userId)
		{
			OperationResult<List<DepartmantEducationsResultDto>> result = new OperationResult<List<DepartmantEducationsResultDto>>();

			try
			{
				var repResult = await _departmantEducationsResultRepository.GetListAsync(x => x.UserId == userId);

				result.Clone(repResult);
				if (result.Result)
				{
					result.Response = _mapper.Map<List<DepartmantEducationsResultDto>>(repResult.Response);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exciption GetDepartmantEducationsResultByUsernameAsync {@userId}", userId);
				ex.HandelException(result);
			}
			return result;
		}

		/// <summary>
		/// Get Departmant Educations Result By Username Async
		/// </summary>
		/// <param name="username"></param>
		/// <param name="departmantCode"></param>
		/// <param name="educationCode"></param>
		/// <returns></returns>
		public async Task<OperationResult<DepartmantEducationsResultDto>> GetDepartmantEducationsResultByUsernameAsync(string username, string departmantCode, string educationCode)
		{
			OperationResult<DepartmantEducationsResultDto> result = new OperationResult<DepartmantEducationsResultDto>();
			try
			{
				var userResult = await _userRepository.GetAsync(x => x.Username == username);
				if (userResult.Result)
				{
					var departmentResult = await _departmentRepository.GetAsync(x => x.Code == departmantCode);
					var departmantEducation = await _departmentEducationRepository.GetAsync(x => x.DepartmantId == departmentResult.Response.Id && x.Code == educationCode);

					var repResult = await _departmantEducationsResultRepository.GetAsync(x => x.UserId == userResult.Response.Id && x.DepartmantEducationId == departmantEducation.Response.Id);

					result.Clone(repResult);
					if (result.Result)
					{
						result.Response = _mapper.Map<DepartmantEducationsResultDto>(repResult.Response);
					}
				}
				else
				{
					result.Result = false;
					result.Response = null;
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exciption GetDepartmantEducationsResultByUsernameAsync {@Username} {@DepartmantCode} {@EducationCode}", username, departmantCode, educationCode);
				ex.HandelException(result);
			}
			return result;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public async Task<OperationResult<DepartmantDto>> GetDepartmantAsync(Query<DepartmantDto> query)
		{
			OperationResult<DepartmantDto> result = new OperationResult<DepartmantDto>();
			try
			{
				var repQuery = new Query<Departmant>(_mapper.Map<Departmant>(query.Parameter));
				var repResult = await _departmentRepository.GetAsync(c => c.Id == query.Parameter.Id);

				result.Clone(repResult);
				if (result.Result)
				{
					result.Response = _mapper.Map<DepartmantDto>(repResult.Response);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exciption GetDepartmantAsync {@Query}", query);
				ex.HandelException(result);
			}
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public async Task<OperationResult<DepartmantEducationDto>> GetDepartmantEducationAsync(Query<DepartmantEducationDto> query)
		{
			OperationResult<DepartmantEducationDto> result = new OperationResult<DepartmantEducationDto>();
			try
			{
				var repQuery = new Query<DepartmantEducation>(_mapper.Map<DepartmantEducation>(query.Parameter));
				var repResult = await _departmentEducationRepository.GetAsync(c => c.Id == query.Parameter.Id);

				result.Clone(repResult);
				if (result.Result)
				{
					result.Response = _mapper.Map<DepartmantEducationDto>(repResult.Response);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exciption GetDepartmantEducationAsync {@Query}", query);
				ex.HandelException(result);
			}
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public async Task<OperationResult<SubscriptionDto>> GetSubscriptionAsync(Query<SubscriptionDto> query)
		{
			OperationResult<SubscriptionDto> result = new OperationResult<SubscriptionDto>();
			try
			{
				var repQuery = new Query<Subscription>(_mapper.Map<Subscription>(query.Parameter));
				var repResult = await _subscriptionRepository.GetAsync(c => c.Id == query.Parameter.Id);

				result.Clone(repResult);
				if (result.Result)
				{
					result.Response = _mapper.Map<SubscriptionDto>(repResult.Response);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exciption GetSubscriptionAsync {@Query}", query);
				ex.HandelException(result);
			}
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public async Task<OperationResult<UserDto>> GetUserAsync(Query<UserDto> query)
		{
			OperationResult<UserDto> result = new OperationResult<UserDto>();
			try
			{
				var repQuery = new Query<User>(_mapper.Map<User>(query.Parameter));
				var repResult = await _userRepository.GetAsync(c => c.Id == query.Parameter.Id);

				result.Clone(repResult);
				if (result.Result)
				{
					result.Response = _mapper.Map<UserDto>(repResult.Response);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exciption GetUserAsync {@Query}", query);
				ex.HandelException(result);
			}
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public async Task<OperationResult<DepartmantDto>> UpdateDepartmantAsync(Query<DepartmantDto> query)
		{
			OperationResult<DepartmantDto> result = new OperationResult<DepartmantDto>();
			try
			{
				var repQuery = new Query<Departmant>(_mapper.Map<Departmant>(query.Parameter));
				var repResult = await _departmentRepository.UpdateAsync(repQuery.Parameter);

				result.Clone(repResult);
				if (result.Result)
				{
					result.Response = _mapper.Map<DepartmantDto>(repResult.Response);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exciption UpdateDepartmantAsync {@Query}", query);
				ex.HandelException(result);
			}
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public async Task<OperationResult<DepartmantEducationDto>> UpdateDepartmantEducationAsync(Query<DepartmantEducationDto> query)
		{
			OperationResult<DepartmantEducationDto> result = new OperationResult<DepartmantEducationDto>();
			try
			{
				var repQuery = new Query<DepartmantEducation>(_mapper.Map<DepartmantEducation>(query.Parameter));
				var repResult = await _departmentEducationRepository.UpdateAsync(repQuery.Parameter);

				result.Clone(repResult);
				if (result.Result)
				{
					result.Response = _mapper.Map<DepartmantEducationDto>(repResult.Response);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exciption UpdateDepartmantEducationAsync {@Query}", query);
				ex.HandelException(result);
			}
			return result;
		}

		/// <summary>
		/// Update Subscription Async
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public async Task<OperationResult<SubscriptionDto>> UpdateSubscriptionAsync(Query<SubscriptionDto> query)
		{
			OperationResult<SubscriptionDto> result = new OperationResult<SubscriptionDto>();
			try
			{
				var repQuery = new Query<Subscription>(_mapper.Map<Subscription>(query.Parameter));
				var repResult = await _subscriptionRepository.UpdateAsync(repQuery.Parameter);

				result.Clone(repResult);
				if (result.Result)
				{
					result.Response = _mapper.Map<SubscriptionDto>(repResult.Response);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exciption UpdateSubscriptionAsync {@Query}", query);
				ex.HandelException(result);
			}
			return result;
		}

		/// <summary>
		/// Update User Async
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public async Task<OperationResult<UserDto>> UpdateUserAsync(Query<UserDto> query)
		{
			OperationResult<UserDto> result = new OperationResult<UserDto>();
			try
			{
				var repQuery = new Query<User>(_mapper.Map<User>(query.Parameter));
				var repResult = await _userRepository.UpdateAsync(repQuery.Parameter);

				result.Clone(repResult);
				if (result.Result)
				{
					result.Response = _mapper.Map<UserDto>(repResult.Response);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exciption UpdateUserAsync {@Query}", query);
				ex.HandelException(result);
			}
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public async Task<OperationResult<List<DepartmantEducationsResultDto>>> GenerateAndStoreAsync()
		{
			OperationResult<List<DepartmantEducationsResultDto>> result = new OperationResult<List<DepartmantEducationsResultDto>>();

			try
			{
				var userServiceResult = await _userRepository.GetListAsync(x => x.SubscriptionId != null);

				if (userServiceResult.Result)
				{
					for (int i = 0; i < userServiceResult.Response.Count; i++)
					{
						var subscriberId = userServiceResult.Response[i].SubscriptionId;
						var userId = userServiceResult.Response[i].Id;

						var departmant = await _departmentRepository.GetAsync(x => x.SubscriptionId == subscriberId);
						if (departmant.Result)
						{
							OperationResult<List<DepartmantEducation>> departmantEducationList = new OperationResult<List<DepartmantEducation>>();

							var departmantEducationResult = await _departmentEducationRepository.GetListAsync(x => x.DepartmantId == departmant.Response.Id);

							departmantEducationList.Clone(departmantEducationResult);

							if (departmantEducationResult.Result)
							{
								var educationResultList = departmantEducationResult.Response;
								for (int j = 0; j < educationResultList.Count; j++)
								{
									var educationResultItem = educationResultList[j];

									var existData = await _departmantEducationsResultRepository.GetAsync(x => x.DepartmantEducationId == educationResultItem.Id);

									if (existData.Response == null)
									{
										var subResult = await _departmantEducationsResultRepository.AddAsync(new DepartmantEducationsResult()
										{
											Id = Guid.NewGuid(),
											DepartmantEducationId = educationResultItem.Id,
											Result = j * 10,
											UserId = userId,
											DepartmantEducation = null,
											User = null
										});
									}
								}
							}
						}
					}
				}

				var resultAllList = await _departmantEducationsResultRepository.GetListAsync(x => x.Id != null);
				result.Clone(resultAllList);
				if(result.Result)
				{
					result.Response = _mapper.Map<List<DepartmantEducationsResultDto>>(resultAllList.Response);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exciption GenerateAndStoreAsync");
				ex.HandelException(result);
			}

			return result;
		}
	}
}
