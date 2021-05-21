using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Common.Model.Messages;

namespace LMS.Services.Identity.WebApi.TestRabbit
{
    public class TestEvent :IEvent
    {
        public string EventMessage { get; set; }
    }
}
