using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Common.RabbitMq.Handlers;

namespace LMS.Services.Identity.WebApi.TestRabbit
{
    public class TestHandler : ICommandHandler<TestMessage>
    {
        public async Task HandleAsync(TestMessage command)
        {
            Console.WriteLine($"recived message : {command.Message}"); 
        }
    }
}
