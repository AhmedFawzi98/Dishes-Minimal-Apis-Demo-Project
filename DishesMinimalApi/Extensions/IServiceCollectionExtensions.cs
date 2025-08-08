using DishesAPI.DbContexts;
using DishesMinimalApi.HostedServices;
using DishesMinimalApi.Infrastructure.Constants;
using DishesMinimalApi.Infrastructure.Repositories.Abstractions;
using DishesMinimalApi.Infrastructure.Seeders;
using DishesMinimalApi.Middlewares;
using DishesMinimalApi.Shared.Constants;
using FluentValidation;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.RateLimiting;

namespace DishesMinimalApi.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApi();

        services.AddEndpoints();

        services.AddApplicationServices(configuration);

        services.AddRateLimiter(limiterOptions =>
        {
            limiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            limiterOptions.AddPolicy(RateLimitingPolicies.SlidingWindowPolicy, httpContext =>
            {
                var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unkown";
                return RateLimitPartition.GetSlidingWindowLimiter(
                                partitionKey: ipAddress,
                                factory: partition => new SlidingWindowRateLimiterOptions
                                {
                                    PermitLimit = 15,
                                    Window = TimeSpan.FromSeconds(15),
                                    SegmentsPerWindow = 3,
                                    QueueLimit = 3,
                                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                                });
            });
        });

        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }
    private static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Continue;
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Continue;

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
