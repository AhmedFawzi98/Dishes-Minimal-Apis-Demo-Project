using DishesAPI.DbContexts;
using DishesMinimalApi.HostedServices;
using DishesMinimalApi.Infrastructure.Constants;
using DishesMinimalApi.Infrastructure.Repositories.Abstractions;
using DishesMinimalApi.Infrastructure.Seeders;
using DishesMinimalApi.Shared.Constants;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

namespace DishesMinimalApi.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationServices(configuration);

        services.AddEndpoints();

        services.AddRateLimiter(limiterOptions =>
        {
            limiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            limiterOptions.AddPolicy(RateLimitingConstants.SlidingWindowPolicy, httpContext =>
            {
                var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unkown";
                return RateLimitPartition.GetSlidingWindowLimiter(
                                partitionKey: ipAddress,
                                factory: partition => new SlidingWindowRateLimiterOptions
                                {
                                    PermitLimit = 3,
                                    Window = TimeSpan.FromSeconds(15),
                                    SegmentsPerWindow = 3,
                                    QueueLimit = 0,
                                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                                });
            });
        });

        return services;
    }
    private static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
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
