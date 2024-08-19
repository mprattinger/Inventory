using System;
using Api.Features.StorageFeature;

namespace Api.Infrastructure.Repositories;

public static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services) {

        services.AddScoped<IStorageRepository, StorageRepository>();

        return services;
    }
}
