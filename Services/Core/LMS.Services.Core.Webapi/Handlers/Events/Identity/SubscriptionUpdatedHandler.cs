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
    public class SubscriptionUpdatedHandler : IEventHandler<SubscriptionUpdated>
    {
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly ILogger<SubscriptionUpdatedHandler> _logger;
        public SubscriptionUpdatedHandler(
            IIdentityService identityService,
            IMapper mapper,
            ILogger<SubscriptionUpdatedHandler> logger)
        {
            _logger = logger;
            _identityService = identityService;
            _mapper = mapper;

        }

        public async Task HandleAsync(SubscriptionUpdated @event)
        {
            try
            {
                var query = new Query<SubscriptionDto>(_mapper.Map<SubscriptionDto>(@event));
                await _identityService.UpdateSubscriptionAsync(query);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SubscriptionUpdated {@Event}", @event);
            }
        }
    }
}