using Musicana.Api.Repositories;
using Musicana.Api.Services;

namespace Musicana.Api.DependencyInjection;

public static class PlaylistDI
{
    public static IServiceCollection PlaylistServices(this IServiceCollection services)
    {
        services.AddScoped<IPlaylistRepo, PlaylistRepo>();
        services.AddScoped<IPlaylistService, PlaylistService>();
        return services;
    }
}
