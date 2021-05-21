using System;
using LMS.Common.Model.Messages;
using LMS.Common.Model.Constants;

namespace LMS.Services.Core.Dto.Messages.Events.Identity
{
    public class SubscriptionUpdated : IEvent
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public SubscriptionStatus Status { get; set; }
    }
}
