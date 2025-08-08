namespace DishesMinimalApi.Exceptions;

public class CustomValidationException : CustomException
{
    public IDictionary<string, string[]> Errors { get; }

    public CustomValidationException(IDictionary<string, string[]> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors;
    }
    public override int StatusCode => StatusCodes.Status400BadRequest;

}