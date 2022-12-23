namespace Application.Exceptions;

internal class AlreadyLogInException : ApplicationException
{
    public AlreadyLogInException()
        : base($"You have alredy logged in") { }
}