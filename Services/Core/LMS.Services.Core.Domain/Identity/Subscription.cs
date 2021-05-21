using System;
using System.Collections.Generic;
using LMS.Common.Model.Constants;
using LMS.Common.Model.Entity;

namespace LMS.Services.Core.Domain.Identity
{
    public class Subscription : IEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public Status Status { get; set; }

        public List<Departmant> Departmants { get; set; }
        public List<User> Users { get; set; }
    }
}
