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
    public class UserAddedHandler : IEventHandler<UserAdded>
    {
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly ILogger<UserAddedHandler> _logger;
        public UserAddedHandler(
            IIdentityService identityService,
            IMapper mapper,
            ILogger<UserAddedHandler> logger)
        {
            _logger = logger;
            _identityService = identityService;
            _mapper = mapper;

        }

        public async Task HandleAsync(UserAdded @event)
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