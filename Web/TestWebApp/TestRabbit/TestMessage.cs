using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Common.Model.Messages;

namespace LMS.Services.Identity.WebApi.TestRabbit
{
    public class TestMessage : ICommand
    {
        public string Message { get; set; }
    }
}
