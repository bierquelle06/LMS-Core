using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using LMS.Common.Model;
using LMS.Common.RabbitMq.Handlers;
using LMS.Services.Core.Dto.Identity;
using LMS.Services.Core.Dto.Messages.Events.Identity;
using LMS.Services.Core.Service;
using LMS.Services.Core.Service.Abstract;

namespace LMS.Services.Core.Webapi.Handlers.Events.Identity
{
    public class UserUpdatedHandler : IEventHandler<UserUpdated>
    {
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly ILogger<UserUpdatedHandler> _logger;
        public UserUpdatedHandler(
            IIdentityService identityService,
            IMapper mapper,
            ILogger<UserUpdatedHandler> logger)
        {
            _logger = logger;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task HandleAsync(UserUpdated @event)
        {
            try
            {
                var query = new Query<UserDto>(_mapper.Map<UserDto>(@event));
                await _identityService.AddUserAsync(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UserAdded {@Event}", @event);
            }
        }
    }
}