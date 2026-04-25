using Musicana.Api.Repositories;
using Musicana.Api.Services;

namespace Musicana.Api.DependencyInjection;

public static class AlbumDI
{
    public static IServiceCollection AlbumServices(this IServiceCollection services)
    {
        services.AddScoped<IAlbumRepo, AlbumRepo>();
        services.AddScoped<IAlbumService, AlbumService>();
        return services;
    }
}
