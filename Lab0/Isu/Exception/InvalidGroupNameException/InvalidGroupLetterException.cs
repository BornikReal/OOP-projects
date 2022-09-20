namespace Isu.Exception.InvalidGroupNameException;

public class InvalidGroupLetterException : InvalidGroupNameException
{
    public InvalidGroupLetterException(char letter)
        : base($"Group leter {letter} is not a letter.")
    { }
}