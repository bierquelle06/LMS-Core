using AutoMapper;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using LMS.Common.Core;
using LMS.Common.Core.Authentication;
using LMS.Common.Core.Mvc;
using LMS.Common.Core.Sql;
using LMS.Common.Core.Swagger;

using LMS.Common.RabbitMq;
using LMS.Common.RabbitMq.Handlers;

using LMS.Services.Core.Dto.Messages.Events.Identity;
using LMS.Services.Core.Webapi.Handlers.Events.Identity;

using LMS.Services.Core.Repository.Abstract.Identity;
using LMS.Services.Core.Repository.Concrete.EntityFramework.Context;
using LMS.Services.Core.Repository.Concrete.EntityFramework.Identity;

using LMS.Services.Core.Service;
using LMS.Services.Core.Service.Abstract;
using LMS.Services.Core.Service.Concrete;

namespace LMS.Services.Core.Webapi
{
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration _configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        public IWebHostEnvironment _webHostEnvironment { get; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(env.ContentRootPath)
             .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables();

            _webHostEnvironment = env;

            _configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Distributed Redis Cache
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = _configuration.GetSection("RedisConnectionSettings")["ConnectionString"];
                option.InstanceName = _configuration.GetSection("RedisConnectionSettings")["InstanceName"];
            });

            //Stack Exchange Redis Cache
            services.AddStackExchangeRedisCache(option =>
            {
                option.Configuration = _configuration.GetSection("RedisConnectionSettings")["ConnectionString"];
                option.InstanceName = _configuration.GetSection("RedisConnectionSettings")["InstanceName"];
            });

            services.AddCustomMvc();
            services.AddControllers();
            services.AddJwtAuth();
            services.AddSwaggerDocs();
            services.AddCors(options => { 
                options.AddPolicy("CorsPolicy", cors => cors.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); 
            });

            services.AddEFSqlServer<CoreContext>("core-db-conn");

            var mapperConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<IIdentityService, IdentityService>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISubscriptionRepository, SubscriptionRepository>();
            services.AddTransient<IDepartmantRepository, DepartmantRepository>();
            services.AddTransient<IDepartmantEducationRepository, DepartmantEducationRepository>();
            services.AddTransient<IDepartmantEducationsResultRepository, DepartmantEducationsResultRepository>();

            services.AddTransient<IEventHandler<DepartmantAdded>, DepartmantAddedHandler>();
            services.AddTransient<IEventHandler<DepartmantEducationAdded>, DepartmantEducationAddedHandler>();
            services.AddTransient<IEventHandler<UserAdded>, UserAddedHandler>();
            services.AddTransient<IEventHandler<SubscriptionAdded>, SubscriptionAddedHandler>();

            services.AddTransient<IEventHandler<DepartmantUpdated>, DepartmantUpdatedHandler>();
            services.AddTransient<IEventHandler<DepartmantEducationUpdated>, DepartmantEducationUpdatedHandler>();
            services.AddTransient<IEventHandler<UserUpdated>, UserUpdatedHandler>();
            services.AddTransient<IEventHandler<SubscriptionUpdated>, SubscriptionUpdatedHandler>();
            
            services.AddRabbit();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStartupInitializer startupInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } 

            app.UseHttpsRedirection();

            app.UseSwaggerDocs();
            app.UseCors();
            app.UseRouting();
            app.UseErrorHandler();
            app.UseAuthentication();
            app.UseAuthorization(); 
            app.UseJwtValidator();
            app.UseServiceId(); 

            app.UseRabbit()
                .SubscribeEvent<DepartmantAdded>("identity")
                .SubscribeEvent<DepartmantEducationAdded>("identity")
                .SubscribeEvent<UserAdded>("identity")
                .SubscribeEvent<SubscriptionAdded>("identity")

                .SubscribeEvent<DepartmantUpdated>("identity")
                .SubscribeEvent<DepartmantEducationUpdated>("identity")
                .SubscribeEvent<UserUpdated>("identity")
                .SubscribeEvent<SubscriptionUpdated>("identity");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            startupInitializer.InitializeAsync();
        }
    }
}
