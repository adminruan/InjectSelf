using Microsoft.Extensions.DependencyInjection;
using My.Common;

namespace My.Test3;

public class Test3Module : MyModule
{
    public override void OnCongigurationService(IServiceCollection services)
    {
        services.AddScoped<ITest3Service, Test3Service>();
    }
}