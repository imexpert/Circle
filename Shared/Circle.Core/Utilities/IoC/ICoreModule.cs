using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Circle.Core.Utilities.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection services, IConfiguration configuration);
    }
}