using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.Core.Dto.Identity
{
    public class DepartmantEducationsResultDto
    {
        public Guid Id { get; set; }
        public Guid DepartmantEducationId { get; set; }
        public Guid UserId { get; set; }

        public decimal Result { get; set; }
    }
}