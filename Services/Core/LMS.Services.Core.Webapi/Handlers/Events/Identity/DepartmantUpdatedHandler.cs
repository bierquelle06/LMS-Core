using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Common.Model;
using LMS.Common.RabbitMq.Handlers;
using LMS.Services.Core.Dto.Identity;
using LMS.Services.Core.Dto.Messages.Events.Identity;
using LMS.Services.Core.Service;
using LMS.Services.Core.Service.Abstract;

namespace LMS.Services.Core.Webapi.Handlers.Events.Identity
{
    public class DepartmantUpdatedHandler : IEventHandler<DepartmantUpdated>
    {
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly ILogger<DepartmantUpdatedHandler> _logger;
        public DepartmantUpdatedHandler(
            IIdentityService identityService,
            IMapper mapper,
            ILogger<DepartmantUpdatedHandler> logger)
        {
            _logger = logger;
            _identityService = identityService;
            _mapper = mapper;

        }

        public async Task HandleAsync(DepartmantUpdated @event)
        {
            try
            {
                var query = new Query<DepartmantDto>(_mapper.Map<DepartmantDto>(@event));
                await _identityService.UpdateDepartmantAsync(query);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DepartmantUpdated {@Event}", @event);
            }
        }
    }
}
