using System;
using Api.Infrastructure.Data;
using Api.Infrastructure.Repositories;

namespace Api.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) {

        services.AddData();
        services.AddRepositories();

        return services;
    }
}
