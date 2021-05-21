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
    public class DepartmantEducationRepository : EfRepository<DepartmantEducation, CoreContext>, IDepartmantEducationRepository
    {
        private readonly CoreContext _context;
        private readonly ILogger<DepartmantEducationRepository> _logger;
        public DepartmantEducationRepository(CoreContext context,
            ILogger<DepartmantEducationRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
