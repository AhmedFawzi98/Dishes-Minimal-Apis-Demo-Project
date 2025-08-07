using DishesAPI.DbContexts;
using DishesMinimalApi.HostedServices;
using DishesMinimalApi.Infrastructure.Constants;
using DishesMinimalApi.Infrastructure.Repositories.Abstractions;
using DishesMinimalApi.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;

namespace DishesMinimalApi.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        //presentation
        services.AddOpenApi();

        //data access
        services.AddDbContext<DishesDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(DatabaseConstants.DishesDBConnectionString));
        });

        services.AddScoped<IDishRepository, DishRepository>();
        services.AddScoped<IDbSeeder, DbSeeder>();
        services.AddHostedService<DbContextInitializer>();
        return services;
    }
}
