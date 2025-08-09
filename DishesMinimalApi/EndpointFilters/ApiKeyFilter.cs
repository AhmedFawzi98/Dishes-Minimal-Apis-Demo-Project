using DishesMinimalApi.Shared.Constants;
using Microsoft.AspNetCore.Mvc;

namespace DishesMinimalApi.EndpointFilters;

public class ApiKeyFilter(IConfiguration configuration) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context,EndpointFilterDelegate next)
    {
        var expectedApiKey = configuration[HeadersConstants.ApiKeyHeader];

        if (!context.HttpContext.Request.Headers.TryGetValue(HeadersConstants.ApiKeyHeader, out var apiKey) || string.IsNullOrEmpty(apiKey) || apiKey != expectedApiKey)
        {
            var problemDetails = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.2",
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Detail = "The provided API key is missing or invalid.",
                Instance = context.HttpContext.Request.Path
            };
            return Results.Problem(problemDetails);
        }

        return await next(context);
    }
}
