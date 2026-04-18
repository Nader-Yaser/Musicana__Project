using Musicana.Api.Repositories;
using Musicana.Api.Services;

namespace Musicana.Api.DependencyInjection;

public static class FavouriteDI
{
    public static IServiceCollection FavouriteServices(this IServiceCollection services)
    {
        services.AddScoped<IFavouriteRepo, FavouriteRepo>();
        services.AddScoped<IFavouriteService, FavouriteService>();
        return services;
    }
}
