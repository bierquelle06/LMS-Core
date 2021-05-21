using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.Core.Dto.Identity
{
    public class DepartmantEducationDto
    {
        public Guid Id { get; set; }
        public Guid DepartmantId { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
    }
}
