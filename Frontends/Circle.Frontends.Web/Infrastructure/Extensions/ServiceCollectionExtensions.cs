using Circle.Core.Http;
using Circle.Core.Utilities.Security.Encyption;
using Circle.Core.Utilities.Security.Jwt;
using Circle.Frontends.Web.Handlers;
using Circle.Frontends.Web.Resources;
using Circle.Frontends.Web.Services.Abstract;
using Circle.Frontends.Web.Services.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //add accessor to HttpContext
            services.AddHttpContextAccessor();

            services.AddAntiforgery();

            services.AddHttpClients(configuration);

            var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, s =>
            {
                s.LoginPath = "/Login/Index";
                s.ExpireTimeSpan = TimeSpan.FromDays(60);
                s.SlidingExpiration = true;
                s.Cookie.Name = "circlewebcookie";
            });

            services.AddScoped<ResourceOwnerPasswordTokenHandler>();
            services.AddScoped<ClientCredentialTokenHandler>();

            services.AddCircleMvc();
        }

        /// <summary>
        /// Register HttpContextAccessor
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAntiforgery();

            services.AddCircleMvc();
        }

        /// <summary>
        /// Add and configure MVC for the application
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <returns>A builder for configuring MVC services</returns>
        public static void AddCircleMvc(this IServiceCollection services)
        {
            //add basic MVC feature
            var mvcBuilder = services.AddControllersWithViews();
            services.AddSingleton<LocService>();
            //use cookie-based temp data provider
            mvcBuilder.AddCookieTempDataProvider(options =>
            {
                options.Cookie.Name = $"{CircleCookieDefaults.Prefix}{CircleCookieDefaults.TempDataCookie}";
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });

            services.AddRazorPages();

            //MVC now serializes JSON with camel case names by default, use this code to avoid it
            mvcBuilder.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            //set some options
            mvcBuilder.AddMvcOptions(options =>
            {
                options.EnableEndpointRouting = false;
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc(s=>
            {
                s.EnableEndpointRouting = false;
            })
            .AddRazorRuntimeCompilation()
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            //register controllers as services, it'll allow to override them
            mvcBuilder.AddControllersAsServices();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("tr-TR"),
                        new CultureInfo("en-US")
                    };

                options.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        /// <summary>
        /// Adds services required for anti-forgery support
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddAntiForgery(this IServiceCollection services)
        {
            //override cookie name
            services.AddAntiforgery(options =>
            {
                options.Cookie.Name = $"{CircleCookieDefaults.Prefix}{CircleCookieDefaults.AntiforgeryCookie}";
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });
        }

        public static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IAuthService, AuthService>(s =>
            {
                s.BaseAddress = new Uri(configuration.GetValue<string>("ApiUrl"));
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IGroupService, GroupService>(s =>
            {
                s.BaseAddress = new Uri(configuration.GetValue<string>("ApiUrl"));
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IUserService, UserService>(s =>
            {
                s.BaseAddress = new Uri(configuration.GetValue<string>("ApiUrl"));
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IDepartmentService, DepartmentService>(s =>
            {
                s.BaseAddress = new Uri(configuration.GetValue<string>("ApiUrl"));
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        }
    }
}
