using Microsoft.Extensions.DependencyInjection;
using My.Common;

namespace My.Test2;

public class Test2Module : MyModule
{
    public override void OnCongigurationService(IServiceCollection services)
    {
        services.AddScoped<ITest2Service, Test2Service>();
    }
}