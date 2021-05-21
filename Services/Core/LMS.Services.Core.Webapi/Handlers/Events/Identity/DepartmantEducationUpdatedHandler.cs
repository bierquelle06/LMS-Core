﻿using AutoMapper;
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
    public class DepartmantEducationUpdatedHandler : IEventHandler<DepartmantEducationUpdated>
    {
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly ILogger<DepartmantEducationUpdatedHandler> _logger;

        public DepartmantEducationUpdatedHandler(
            IIdentityService identityService,
            IMapper mapper,
            ILogger<DepartmantEducationUpdatedHandler> logger)
        {
            _logger = logger;
            _identityService = identityService;
            _mapper = mapper;

        }

        public async Task HandleAsync(DepartmantEducationUpdated @event)
        {
            try
            {
                var query = new Query<DepartmantEducationDto>(_mapper.Map<DepartmantEducationDto>(@event));
                await _identityService.UpdateDepartmantEducationAsync(query);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DepartmantEducationUpdated {@Event}", @event);
            }
        }
    }
}
