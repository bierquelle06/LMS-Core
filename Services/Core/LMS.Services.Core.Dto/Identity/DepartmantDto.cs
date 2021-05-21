using System; 
using LMS.Common.Model.Constants;

namespace LMS.Services.Core.Dto.Identity
{
    public class DepartmantDto
    {
        public Guid Id { get; set; }
        public Guid SubscriptionId { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public Status Status { get; set; } 
    }
}
