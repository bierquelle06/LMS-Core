using System;
using LMS.Common.Model.Constants;
using LMS.Common.Model.Messages;

namespace LMS.Services.Core.Dto.Messages.Events.Identity
{
    public class SubscriptionAdded :IEvent
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public SubscriptionStatus Status { get; set; }
    }
}
