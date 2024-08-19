using System;
using Api.Features.StorageFeature;

namespace Api.Features;

public static class Extensions
{
    public static IServiceCollection AddFeatures(this IServiceCollection services) {
        
        services.AddStorageFeature();

        return services;
    }
}
