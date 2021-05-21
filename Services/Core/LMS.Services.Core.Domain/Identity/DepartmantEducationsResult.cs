using System;
using LMS.Common.Model.Entity;

namespace LMS.Services.Core.Domain.Identity
{
    public class DepartmantEducationsResult : IEntity
    {
        public Guid Id { get; set; }

        public Guid DepartmantEducationId { get; set; }
        public Guid UserId { get; set; }

        public DepartmantEducation DepartmantEducation { get; set; }
        public User User { get; set; }

        public decimal Result { get; set; }
    }
}
