namespace Isu.Exception.InvalidGroupNameException;

public class InvalidCourseNumberException : InvalidGroupNameException
{
    public InvalidCourseNumberException(int number)
        : base($"Course number {number} is out of range.")
    { }
}