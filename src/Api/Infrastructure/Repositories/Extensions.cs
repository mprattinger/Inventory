using System;
using Api.Features.StockyardFeature;
using Api.Features.StorageFeature;

namespace Api.Infrastructure.Repositories;

public static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services) {

        services.AddScoped<IStorageRepository, StorageRepository>();
        services.AddScoped<IStockyardRepository, StockyardRepository>();

        return services;
    }
}
