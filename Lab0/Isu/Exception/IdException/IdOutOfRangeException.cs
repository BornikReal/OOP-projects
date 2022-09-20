namespace Isu.Exception.IdException;

public class IdOutOfRangeException : IdException
{
    public IdOutOfRangeException(int id)
        : base($"Id {id} has already used or out of range.")
    { }
}