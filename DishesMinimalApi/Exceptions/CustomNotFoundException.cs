namespace DishesMinimalApi.Exceptions;

public class CustomNotFoundException : CustomException
{
    public CustomNotFoundException(string message) : base(message) 
    {
    }
    public override int StatusCode => StatusCodes.Status404NotFound;
}
