using System;

namespace Api.Features.StorageFeature;

public static class Extensions
{
    public static IServiceCollection AddStorageFeature(this IServiceCollection services) {
        
        services.AddScoped<CreateStorage>();
        services.AddScoped<ModifyStorage>();
        
        return services;
    }
}
