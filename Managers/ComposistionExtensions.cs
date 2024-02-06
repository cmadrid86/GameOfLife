using Microsoft.Extensions.DependencyInjection;

namespace Managers;

public static class ComposistionExtensions
{
    public static IServiceCollection RegisterDefaultManagers(this IServiceCollection services)
    {
        services.AddScoped<IGameManager, GameManager>();

        return services;
    }
}
