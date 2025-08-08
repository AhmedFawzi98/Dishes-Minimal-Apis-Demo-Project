namespace DishesMinimalApi.Exceptions;

public abstract class CustomException : Exception
{
    public CustomException() 
    {
    }

    public CustomException(string message, Exception innerException) : base(message, innerException) 
    {
    }

    public CustomException(string message) : base(message)
    {
    }

    public abstract int StatusCode { get; }
}
