using System;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Data;

public static class Extensions
{
    public static IServiceCollection AddData(this IServiceCollection services) {

        services.AddDbContext<InventoryDataContext>(options => options.UseInMemoryDatabase("db")); 
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
