using System.Diagnostics;
using Castle.DynamicProxy;
using Circle.Core.Utilities.Interceptors;
using Circle.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Circle.Core.Aspects.Autofac.Performance
{
    /// <summary>
    /// PerformanceAspect
    /// </summary>
    public class PerformanceAspect : MethodInterception
    {
        private readonly int _interval;
        private readonly Stopwatch _stopwatch;

        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                Debug.WriteLine(
                    $"Performance: {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");
            }

            _stopwatch.Reset();
        }
    }
}