using System.Collections.Generic;
using System.Linq;
using System.Security;
using Castle.DynamicProxy;
using Circle.Core.CrossCuttingConcerns.Caching;
using Circle.Core.Utilities.Interceptors;
using Circle.Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Circle.Library.Business.BusinessAspects
{
    /// <summary>
    /// This Aspect control the user's roles in HttpContext by inject the IHttpContextAccessor.
    /// It is checked by writing as [SecuredOperation] on the handler.
    /// If a valid authorization cannot be found in aspect, it throws an exception.
    /// </summary>
    public class SecuredOperation : MethodInterception
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICacheManager _cacheManager;


        public SecuredOperation()
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var userId = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

            if (userId == null)
            {
                throw new SecurityException(null);
            }

            var oprClaims = _cacheManager.Get($"{CacheKeys.UserIdForClaim}={userId}") as IEnumerable<string>;

            var operationName = invocation.TargetType.ReflectedType.Name;
            if (oprClaims != null && oprClaims.Contains(operationName))
            {
                return;
            }

            string cultureCode = _httpContextAccessor.HttpContext.Request.Path.Value.ToString().Split('/')[2];

            string message = string.Empty;

            switch (cultureCode)
            {
                case "en-EN":
                    message = $"You are not authorized to perform this action. Transaction Name : {operationName}";
                    break;
                case "tr-TR":
                    message = $"Bu işlemi yapmaya yetkiniz yok. İşlem Adı : {operationName}";
                    break;
                default:
                    break;
            }

            throw new SecurityException(message);
        }
    }
}