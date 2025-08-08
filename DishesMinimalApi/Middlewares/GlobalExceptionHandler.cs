using DishesMinimalApi.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DishesMinimalApi.Middlewares;
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> _logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        var response = context.Response;
        response.ContentType = "application/problem+json"; //standard for ProblemDetails responses
        _logger.LogError(exception, $"exception message: {exception.Message}\n exception is being handled by global handling middleware");

        var problemDetails = exception switch
        {
            CustomValidationException validationEx => CreateValidationProblemDetails(validationEx, context.Request.Path),
            CustomNotFoundException notFoundEx => CreateNotFoundProblemDetails(notFoundEx, context.Request.Path),
            CustomException customEx => CreateCustomProblemDetails(customEx, context.Request.Path), //just in case i added a custom exception and forgot to add it in middleare switch case
            _ => CreateServerErrorProblemDetails(context.Request.Path) //for general non-custom exceptions
        };

        response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
        await WriteResponseAsync(response, problemDetails, cancellationToken);
        return true;
    }

    private static ValidationProblemDetails CreateValidationProblemDetails(CustomValidationException ex, string path)
    {
        return new ValidationProblemDetails
        {
            Title = "Validation Failed",
            Detail = ex.Message,
            Status = ex.StatusCode,
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            Instance = path,
            Errors = ex.Errors
        };
    }

    private static ProblemDetails CreateNotFoundProblemDetails(CustomNotFoundException ex, string path)
    {
        return new ProblemDetails
        {
            Title = "Not Found",
            Detail = ex.Message,
            Status = ex.StatusCode,
            Type = "https://tools.ietf.org/html/rfc9110#section-15.6.1",
            Instance = path
        };
    }

    private static ProblemDetails CreateCustomProblemDetails(CustomException ex, string path)
    {
        return new ProblemDetails
        {
            Title = "Error",
            Detail = ex.Message,
            Status = ex.StatusCode,
            Instance = path
        };
    }

    private static ProblemDetails CreateServerErrorProblemDetails(string path)
    {
        return new ProblemDetails
        {
            Title = "Internal Server Error",
            Detail = "An unexpected error occurred.",
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://tools.ietf.org/html/rfc9110#section-15.6.1",
            Instance = path
        };
    }

    private static async Task WriteResponseAsync(HttpResponse response, ProblemDetails problemDetails, CancellationToken cancellationToken)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        var json = JsonSerializer.Serialize(problemDetails, problemDetails.GetType(), options);
        await response.WriteAsync(json, cancellationToken);
    }
}