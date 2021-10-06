using System;
using System.Net;
using System.Security;
using System.Threading.Tasks;
using Circle.Core.Utilities.Results;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Circle.Core.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;


        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }


        private async Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            string cultureCode = httpContext.Request.Path.Value.ToString().Split('/')[2];

            ResponseMessage<NoContent> result = new ResponseMessage<NoContent>();
            result.IsSuccess = false;
            
            if (e.GetType() == typeof(ValidationException))
            {
                result.Message = e.GetFullMessage();
                result.DeveloperMessage = e.GetFullMessage();
                httpContext.Response.StatusCode = result.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (e.GetType() == typeof(SecurityException))
            {
                result.Message = e.GetFullMessage();
                result.DeveloperMessage = e.GetFullMessage();
                httpContext.Response.StatusCode = result.StatusCode = (int)HttpStatusCode.Forbidden;
            }
            else
            {
                string mesaj = e.GetFullMessage();

                switch (cultureCode)
                {
                    case "en-EN":
                        result.Message = "An error occured. Please try again later.";
                        break;
                    case "tr-TR":
                        result.Message = "Hata oluştu. Lütfen tekrar deneyiniz.";
                        break;
                    default:
                        break;
                }
                
                result.DeveloperMessage = mesaj;

                if (mesaj.Contains("UNIQUE") || mesaj.Contains("duplicate key"))
                {
                    switch (cultureCode)
                    {
                        case "en-EN":
                            result.Message = "Record already added.";
                            break;
                        case "tr-TR":
                            result.Message = "Kayıt zaten eklenmiş.";
                            break;
                        default:
                            break;
                    }
                }
                
                httpContext.Response.StatusCode = result.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            string json = JsonConvert.SerializeObject(result);

            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(json);
        }
    }
}