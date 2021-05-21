using System;
using LMS.Common.Model.Messages;

namespace LMS.Services.Core.Dto.Messages.Events.Identity
{
    public class DepartmantEducationUpdated :IEvent
    {
        public Guid Id { get; set; }
        public Guid DepartmantId { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
    }
}
