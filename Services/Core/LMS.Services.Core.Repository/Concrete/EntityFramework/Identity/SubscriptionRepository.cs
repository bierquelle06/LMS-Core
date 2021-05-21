using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using LMS.Common.Core.DataAccess.EntityFramework;
using LMS.Services.Core.Domain.Identity;
using LMS.Services.Core.Repository.Abstract.Identity;
using LMS.Services.Core.Repository.Concrete.EntityFramework.Context;

namespace LMS.Services.Core.Repository.Concrete.EntityFramework.Identity
{
    public class SubscriptionRepository : EfRepository<Subscription, CoreContext>, ISubscriptionRepository
    {
        
        private readonly CoreContext _context;
        private readonly ILogger<SubscriptionRepository> _logger;
        public SubscriptionRepository(CoreContext context,
            ILogger<SubscriptionRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
