namespace Isu.Exception;

public class UnavailableIdException : IsuException
{
    public UnavailableIdException()
        : base("There are no ID's available for issuing.")
    { }
}