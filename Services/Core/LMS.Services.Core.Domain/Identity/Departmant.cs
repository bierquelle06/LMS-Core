using System;
using LMS.Common.Model.Constants;
using LMS.Common.Model.Entity;

namespace LMS.Services.Core.Domain.Identity
{
    public class Departmant : IEntity
    {
        public Guid Id { get; set; }
        public Guid SubscriptionId { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public Status Status { get; set; }
        public Subscription Subscription { get; set; }
    }
}
