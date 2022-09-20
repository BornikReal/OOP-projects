namespace Isu.Exception.InvalidGroupNameException;

public class InvalidFormatGroupNameException : InvalidGroupNameException
{
    public InvalidFormatGroupNameException(string message)
        : base($"\"{message}\" is invalid Group Name.")
    { }
}