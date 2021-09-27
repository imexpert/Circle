using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Circle.Core.Aspects.Autofac.Exception;
using Circle.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;

namespace Circle.Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes =
                type.GetMethod(method.Name)?.GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            if (methodAttributes != null)
            {
                classAttributes.AddRange(methodAttributes);
            }

            classAttributes.Add(new ExceptionLogAspect(typeof(MsSqlLogger)));
            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}