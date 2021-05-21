using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LMS.Common.Core.DataAccess.EntityFramework;
using LMS.Common.Model;
using LMS.Services.Core.Domain.Identity;
using LMS.Services.Core.Repository.Abstract.Identity;
using LMS.Services.Core.Repository.Concrete.EntityFramework.Context;

namespace LMS.Services.Core.Repository.Concrete.EntityFramework.Identity
{
    public class UserRepository : EfRepository<User, CoreContext>, IUserRepository
    {

        private readonly CoreContext _context;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(CoreContext context,
            ILogger<UserRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
 
    }
}
