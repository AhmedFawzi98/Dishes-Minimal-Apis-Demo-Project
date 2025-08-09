using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using DishesMinimalApi.EndpointFilters;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

namespace DishesMinimalApi.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigureWebApplication(this WebApplication app)
    {
        app.UseExceptionHandler(_ => { });

        app.MapOpenApi();

        var versionsGroupNames = new[] { "v1", "v2" };

        foreach (var versionGroupName in versionsGroupNames)
        {
            app.MapScalarApiReference($"/docs/{versionGroupName}", options =>
            {
                options.WithOpenApiRoutePattern($"/openapi/{versionGroupName}.json")
                       .WithTitle($"API {versionGroupName}");
            });
        }

        app.UseHttpsRedirection();

        app.UseRateLimiter();

        var versionSet = app.NewApiVersionSet()
          .HasApiVersion(new ApiVersion(1))
          .HasApiVersion(new ApiVersion(2))
          .ReportApiVersions()
          .Build();


        var baseGroup = app.MapGroup("/api/v{version:apiversion}")
            .WithApiVersionSet(versionSet)
            .AddEndpointFilter<ApiKeyFilter>()
            .AddEndpointFilter<LoggingFilter>()
            .AddEndpointFilter<RequestTimingFilter>()
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError); 

        app.MapEndpoints(baseGroup);

        return app;
    }
}
