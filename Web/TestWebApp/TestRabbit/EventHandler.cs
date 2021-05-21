using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Common.RabbitMq.Handlers;

namespace LMS.Services.Identity.WebApi.TestRabbit
{
    public class EventHandler : IEventHandler<TestEvent>
    { 
        public async Task HandleAsync(TestEvent @event)
        {
            Console.WriteLine($" 2 recived message : {@event.EventMessage}"); 
        }
    }
}
