using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using LMS.Common.Core.Logging;

namespace LMS.Services.Core.Webapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>()
                   .UseLogging();
               });
    }
}
