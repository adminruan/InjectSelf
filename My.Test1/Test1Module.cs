using Microsoft.Extensions.DependencyInjection;
using My.Common;

namespace My.Test1
{
    public class Test1Module : MyModule
    {
        public override void OnCongigurationService(IServiceCollection services)
        {
            services.AddScoped<ITest1Service, Test1Service>();
        }
    }
}