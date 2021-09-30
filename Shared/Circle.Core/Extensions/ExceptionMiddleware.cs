using System;
using System.Net;
using System.Security;
using System.Threading.Tasks;
using Circle.Core.Utilities.Messages;
using Circle.Core.Utilities.Results;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;

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
            ResponseMessage<NoContent> result = new ResponseMessage<NoContent>();
            result.IsSuccess = false;
            
            if (e.GetType() == typeof(ValidationException))
            {
                result.Message = e.GetBaseException().Message;
                result.DeveloperMessage = e.GetBaseException().Message;
                httpContext.Response.StatusCode = result.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (e.GetType() == typeof(SecurityException))
            {
                result.Message = e.GetBaseException().Message;
                result.DeveloperMessage = e.GetBaseException().Message;
                httpContext.Response.StatusCode = result.StatusCode = (int)HttpStatusCode.Forbidden;
            }
            else
            {
                result.Message = "Hata oluştu. Lütfen tekrar deneyiniz.";
                result.DeveloperMessage = e.GetBaseException().Message;
                httpContext.Response.StatusCode = result.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            string json = JsonConvert.SerializeObject(result);

            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(json);
        }
    }
}