using System;
using Api.Features.StockyardFeature;
using Api.Features.StorageFeature;
using FluentValidation;

namespace Api.Features;

public static class Extensions
{
    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(Extensions));

        services.AddStorageFeature();
        services.AddStockyardFeature();

        return services;
    }
}
