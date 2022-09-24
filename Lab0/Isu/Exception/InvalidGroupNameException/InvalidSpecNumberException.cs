namespace Isu.Exception.InvalidGroupNameException;

public class InvalidSpecNumberException : InvalidGroupNameException
{
    public InvalidSpecNumberException(int number)
        : base($"Specialization number {number} is out of range.")
    { }
}