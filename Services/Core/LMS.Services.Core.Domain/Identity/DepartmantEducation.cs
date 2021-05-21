using System;
using LMS.Common.Model.Entity;

namespace LMS.Services.Core.Domain.Identity
{
    public class DepartmantEducation : IEntity
    {
        public Guid Id { get; set; }
        public Guid DepartmantId { get; set; }

        public Departmant Departmant { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
    }
}
