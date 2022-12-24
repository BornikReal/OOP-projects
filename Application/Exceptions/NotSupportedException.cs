namespace Application.Exceptions;

public class NotSupportedException : ApplicationException
{
    public NotSupportedException(string? message) : base(message) { }
}