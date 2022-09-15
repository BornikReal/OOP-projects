namespace Isu.Exception;

public class GroupOverflowException : IsuException
{
    public GroupOverflowException(int maxSize)
        : base($"The group has reached the maximum number of students - {maxSize}.")
    { }
}