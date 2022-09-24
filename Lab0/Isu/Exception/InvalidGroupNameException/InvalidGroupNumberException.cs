namespace Isu.Exception.InvalidGroupNameException;

public class InvalidGroupNumberException : InvalidGroupNameException
{
    public InvalidGroupNumberException(int number)
        : base($"Group number {number} is out of range.")
    { }
}