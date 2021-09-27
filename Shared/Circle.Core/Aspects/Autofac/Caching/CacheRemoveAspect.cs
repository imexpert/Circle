using Castle.DynamicProxy;
using Circle.Core.CrossCuttingConcerns.Caching;
using Circle.Core.Utilities.Interceptors;
using Circle.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Circle.Core.Aspects.Autofac.Caching
{
    /// <summary>
    /// CacheRemoveAspect
    /// </summary>
    public class CacheRemoveAspect : MethodInterception
    {
        private readonly string _pattern;
        private readonly ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}