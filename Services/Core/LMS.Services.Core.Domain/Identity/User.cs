using System;
using LMS.Common.Model.Constants;
using LMS.Common.Model.Entity;

namespace LMS.Services.Core.Domain.Identity
{
    public class User:IEntity
    {
        public Guid Id { get; set; }
        public Guid SubscriptionId { get; set; } 
        public Subscription Subscription { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public UserStatus Status { get; set; }
    }
}
