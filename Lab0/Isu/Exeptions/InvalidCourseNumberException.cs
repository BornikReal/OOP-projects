namespace Isu.Exception;

public class InvalidCourseNumberException : IsuException
{
    public InvalidCourseNumberException(int number)
        : base($"Group number {number} is out of range.")
    { }
}