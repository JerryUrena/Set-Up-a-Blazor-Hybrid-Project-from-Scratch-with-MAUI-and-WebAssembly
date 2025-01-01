using Microsoft.Extensions.DependencyInjection;
using MyBlazorHybridApp.Services;

namespace MyBlazorHybridApp;

public static class Extensions
{

    public static IServiceCollection MyBlazorHybridAppClient(this IServiceCollection services)
    {
        return services
            .AddSingleton<IMyFirstService, MyFirstService>()
            .AddSingleton<IMySecondService, MySecondService>();
    }
}