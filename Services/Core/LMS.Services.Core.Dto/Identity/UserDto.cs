using System;
using LMS.Common.Model.Constants;

namespace LMS.Services.Core.Dto.Identity
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public Guid SubscriptionId { get; set; }
        
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public UserStatus Status { get; set; }
    }
}
