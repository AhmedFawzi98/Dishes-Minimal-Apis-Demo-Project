using System.Diagnostics;

namespace DishesMinimalApi.EndpointFilters;

public class RequestTimingFilter(ILogger<RequestTimingFilter> logger) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        logger.LogInformation($"start stop watch");
        var stopwatch = Stopwatch.StartNew();

        var result = await next(context); 

        stopwatch.Stop();

        logger.LogInformation("stop stop watch, execution Took {ElapsedTime}ms", stopwatch.ElapsedMilliseconds);

        return result;
    }
}
