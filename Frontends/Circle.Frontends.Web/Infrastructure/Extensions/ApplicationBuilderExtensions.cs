using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Net;
using Circle.Frontends.Web.Exceptions;

namespace Circle.Frontends.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure the application HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void ConfigureRequestPipeline(this IApplicationBuilder application,
            IHttpContextAccessor httpContextAccessor)
        {
            HttpClientExtensions.SetContext(httpContextAccessor);

            //get detailed exceptions for developing and testing purposes
            application.UseDeveloperExceptionPage();

            //if (webHostEnvironment.IsDevelopment())
            //{

            //}

            application.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Error/404";
                    await next();
                }
            });

            application.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;

                if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                    response.StatusCode == (int)HttpStatusCode.Forbidden)
                    response.Redirect("/Login");
            });

            application.UseCircleExceptionHandler();

            application.UseCircleStaticFiles();

            application.UsePageNotFound(httpContextAccessor);

            application.UseBadRequestResult();

            application.UseRouting();

            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
            };

            application.UseCookiePolicy(cookiePolicyOptions);

            application.UseAuthentication();

            application.UseAuthorization();

            application.UseEndpoints(s => s.MapControllers());

            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("tr-TR"),
                new CultureInfo("en-US"),
            };
            // SupportedCultures ve SupportedUICultures’a yukarıda oluşturduğumuz dil listesini tanımlıyoruz.
            // DefaultRequestCulture’a varsayılan olarak uygulamamızın hangi dil ile çalışması gerektiğini tanımlıyoruz.
            application.UseRequestLocalization(new RequestLocalizationOptions
            {
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                DefaultRequestCulture = new RequestCulture(supportedCultures[0])
            });

            application.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area}/{controller}/{action}");


                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }

        /// <summary>
        /// Configure middleware for dynamically compressing HTTP responses
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseCircleResponseCompression(this IApplicationBuilder application)
        {
            application.UseResponseCompression();
        }

        /// <summary>
        /// Configure static file serving
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseCircleStaticFiles(this IApplicationBuilder application)
        {
            void staticFileResponse(StaticFileResponseContext context)
            {
                context.Context.Response.Headers.Append(HeaderNames.CacheControl, "public,max-age=31536000");
            }

            //common static files
            application.UseStaticFiles(new StaticFileOptions { OnPrepareResponse = staticFileResponse });
        }

        /// <summary>
        /// Adds a special handler that checks for responses with the 404 status code that do not have a body
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UsePageNotFound(this IApplicationBuilder application, IHttpContextAccessor httpContextAccessor)
        {
            application.UseStatusCodePages(context =>
            {
                //handle 404 Not Found
                if (context.HttpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    if (!IsStaticResource(httpContextAccessor))
                    {
                        //get original path and query
                        var originalPath = context.HttpContext.Request.Path;
                        var originalQueryString = context.HttpContext.Request.QueryString;

                        //var logger = EngineContext.Current.Resolve<ILogger>();
                        //var workContext = EngineContext.Current.Resolve<IWorkContext>();

                        //await logger.ErrorAsync($"Error 404. The requested page ({originalPath}) was not found",
                        //    customer: await workContext.GetCurrentCustomerAsync());

                        try
                        {
                            context.HttpContext.Response.Redirect("/Home/SayfaBulunamadi?path=" + originalPath);
                        }
                        finally
                        {
                            //return original path to request
                            context.HttpContext.Request.QueryString = originalQueryString;
                            context.HttpContext.Request.Path = originalPath;
                        }
                    }
                }

                return Task.CompletedTask;
            });
        }

        /// <summary>
        /// Adds a special handler that checks for responses with the 400 status code (bad request)
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseBadRequestResult(this IApplicationBuilder application)
        {
            application.UseStatusCodePages(async context =>
            {
                //handle 404 (Bad request)
                if (context.HttpContext.Response.StatusCode == StatusCodes.Status400BadRequest)
                {
                    //await logger.ErrorAsync("Error 400. Bad request", null, customer: "");
                }
            });
        }

        /// <summary>
        /// Add exception handling
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseCircleExceptionHandler(this IApplicationBuilder application)
        {
            //log errors
            application.UseExceptionHandler(handler =>
            {
                handler.Run(async context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    if (exception == null)
                        return;

                    try
                    {
                        if (exception.GetType() == typeof(UnAuthorizeException))
                        {
                            foreach (var cookie in context.Request.Cookies.Keys)
                            {
                                context.Response.Cookies.Delete(cookie);
                            }

                            context.Response.Redirect(context.Request.PathBase + "/Login/Index");
                        }

                        context.Response.Redirect(context.Request.PathBase + "/Home/Error");
                    }
                    finally
                    {
                        //rethrow the exception to show the error page
                        //ExceptionDispatchInfo.Throw(exception);
                    }
                });
            });
        }

        /// <summary>
        /// Check whether current HTTP request is available
        /// </summary>
        /// <returns>True if available; otherwise false</returns>
        private static bool IsRequestAvailable(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor?.HttpContext == null)
                return false;

            try
            {
                if (httpContextAccessor.HttpContext.Request == null)
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private static bool IsStaticResource(IHttpContextAccessor httpContextAccessor)
        {
            if (!IsRequestAvailable(httpContextAccessor))
                return false;

            string path = httpContextAccessor.HttpContext.Request.Path;

            //a little workaround. FileExtensionContentTypeProvider contains most of static file extensions. So we can use it
            //source: https://github.com/aspnet/StaticFiles/blob/dev/src/Microsoft.AspNetCore.StaticFiles/FileExtensionContentTypeProvider.cs
            //if it can return content type, then it's a static file
            var contentTypeProvider = new FileExtensionContentTypeProvider();
            return contentTypeProvider.TryGetContentType(path, out var _);
        }
    }
}
