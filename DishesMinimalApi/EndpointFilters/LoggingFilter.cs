namespace DishesMinimalApi.EndpointFilters;

public class LoggingFilter(ILogger<LoggingFilter> logger) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var endpointPath = context.HttpContext?.Request?.Path ?? "uknown path";

        logger.LogInformation("Starting execution of endpoint at path: {EndPointPath}, arguments: {@Arguments}", endpointPath, context.Arguments);

        var result = await next.Invoke(context);

        logger.LogInformation("Finished execution of endpoint at path: {EndPointPath}.", endpointPath);
        return result;
    }
}
