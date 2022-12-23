namespace Domain.Common.Exceptions;

public class DomainException : Exception
{
    protected DomainException(string? message)
        : base(message) { }
}