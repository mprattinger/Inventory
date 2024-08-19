using System;

namespace Api.Features.StorageFeature;

public static class Extensions
{
    public static IServiceCollection AddStockFeature(this IServiceCollection services) {
        
        services.AddScoped<CreateStorage>();
        services.AddScoped<ModifyStorage>();
        
        return services;
    }
}
