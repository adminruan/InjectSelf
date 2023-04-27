using Microsoft.Extensions.DependencyInjection;

namespace My.Common;

public abstract class MyModule
{
    public abstract void OnCongigurationService(IServiceCollection service);
}