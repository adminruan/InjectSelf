using Microsoft.Extensions.DependencyInjection;
using My.Common;

namespace My.Test5;

public class Test5Module : MyModule
{
    public override void OnCongigurationService(IServiceCollection services)
    {
        services.AddScoped<ITest5Service, Test5Service>();
    }
}