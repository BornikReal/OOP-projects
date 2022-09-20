namespace Isu.Exception.IdException;

public class UnavailableIdException : IdException
{
    public UnavailableIdException()
        : base("There are no ID's available for issuing.")
    { }
}