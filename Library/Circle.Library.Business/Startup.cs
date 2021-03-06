using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using Autofac;

using Circle.Library.Business.DependencyResolvers;
using Circle.Core.CrossCuttingConcerns.Caching;
using Circle.Core.DependencyResolvers;
using Circle.Core.Extensions;
using Circle.Core.Utilities.IoC;
using Circle.Core.Utilities.MessageBrokers.RabbitMq;
using Circle.Core.Utilities.Security.Jwt;
using Circle.Library.DataAccess.Abstract;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Circle.Library.DataAccess.Concrete.EntityFramework;
using Circle.Library.DataAccess.Concrete.EntityFramework.Contexts;
using Circle.Library.Business.Helpers;
using Circle.Core.CrossCuttingConcerns.Caching.Redis;

namespace Circle.Library.Business
{
    public partial class BusinessStartup
    {
        public BusinessStartup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }

        protected IHostEnvironment HostEnvironment { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <remarks>
        /// It is common to all configurations and must be called. Aspnet core does not call this method because there are other methods.
        /// </remarks>
        /// <param name="services"></param>
        public virtual void ConfigureServices(IServiceCollection services)
        {
            Func<IServiceProvider, ClaimsPrincipal> getPrincipal = (sp) =>
                sp.GetService<IHttpContextAccessor>().HttpContext?.User ??
                new ClaimsPrincipal(new ClaimsIdentity(""));

            services.AddScoped<IPrincipal>(getPrincipal);
            services.AddMemoryCache();

            var coreModule = new CoreModule();

            services.AddDependencyResolvers(Configuration, new ICoreModule[] { coreModule });

            services.AddSingleton<ConfigurationManager>();


            services.AddTransient<ITokenHelper, JwtHelper>();

            services.AddTransient<IMessageBrokerHelper, MqQueueHelper>();
            services.AddTransient<IMessageConsumer, MqConsumerHelper>();
            services.AddSingleton<ICacheManager, RedisCacheManager>();

            services.AddAutoMapper(typeof(ConfigurationManager));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(BusinessStartup).GetTypeInfo().Assembly);

            ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) =>
            {
                return memberInfo.GetCustomAttribute<DisplayAttribute>()
                    ?.GetName();
            };

            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserGroupRepository, UserGroupRepository>();
            services.AddTransient<IOperationClaimRepository, OperationClaimRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IGroupClaimRepository, GroupClaimRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IReturnUtility, ReturnUtility>();


            services.AddDbContext<ProjectDbContext, MsDbContext>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacBusinessModule(new ConfigurationManager(Configuration, HostEnvironment)));
        }
    }
}