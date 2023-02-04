namespace Application.Exceptions;

public class RootMasterAlreadyExistsException : ApplicationException
{
    public RootMasterAlreadyExistsException()
        : base($"Root master is alredy exist") { }
}