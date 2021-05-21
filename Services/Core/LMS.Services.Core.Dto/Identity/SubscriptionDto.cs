using System;
using LMS.Common.Model.Constants;

namespace LMS.Services.Core.Dto.Identity
{
    public class SubscriptionDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public SubscriptionStatus Status { get; set; } 
    }
}
