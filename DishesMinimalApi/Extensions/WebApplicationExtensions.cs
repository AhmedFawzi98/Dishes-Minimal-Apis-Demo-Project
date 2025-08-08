using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

namespace DishesMinimalApi.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigureWebApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference("docs", options =>
            {
                options.WithOpenApiRoutePattern("/openapi/v1.json");
            });
        }

        app.UseHttpsRedirection();

        app.UseRateLimiter();

        var baseGroup = app.MapGroup("/api")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError); 

        app.MapEndpoints(baseGroup);

        return app;
    }
}
