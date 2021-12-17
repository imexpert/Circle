using Circle.Core.Utilities.IoC;
using Circle.Frontends.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Unicode;

namespace Circle.Frontends.Web
{
    public class Startup
    {
        IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureApplicationServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHttpContextAccessor httpContextAccessor)
        {
            ServiceTool.ServiceProvider = app.ApplicationServices;

            app.ConfigureRequestPipeline(httpContextAccessor);
        }
    }
}
